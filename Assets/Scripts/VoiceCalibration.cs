using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class VoiceCalibration : MonoBehaviour {

	// menu related
	private float guiRatioX;
	private float guiRatioY;
	private float sWidth;
	private float sHeight;
	private Vector3 GUIsF;
	private int sizegui;
	//public GUIStyle customButton;
	public GUISkin customSkin;

	// voice option
	private string bgmText = "ON";
	private string sfxText = "ON";
	private string voiceText = "ON";

	// frame-rate
	private int m_frames = 0;
	private float m_framesPerSecond = 0f;
	private DateTime m_timerFrames = DateTime.Now;

	// Komponen Voice-Recog
	public SpectrumMicrophone Mic = null;
	public WordDetection AudioWordDetection = null;
	private float[] m_micData = null;
	
	//// Flag to normalize wave samples
	public bool NormalizeWave = false;
	//// Flag to remove spectrum noise
	public bool RemoveSpectrumNoise = false;
	//// Selected button down index
	private int m_buttonIndex = -1;
	//// Start position for recording
	private int m_startPosition = 0;
	//// Hold to talk timer
	private DateTime m_timerStart = DateTime.MinValue;

	// key untuk save profile
	private const string FILE_PROFILES = "GasingEvo.profiles";
	// flag untuk load profile
	private bool haveLoad = false;

	// Ambil word yang sudah direkam
	private WordDetails GetWord(string label)
	{
		foreach (WordDetails details in AudioWordDetection.Words)
		{
			if (null == details)
			{
				continue;
			}
			if (details.Label.Equals(label))
			{
				return details;
			}
		}
		
		return null;
	}

	// Ambil data dari Mic di device
	private void GetMicData()
	{
		m_micData = Mic.GetData(0);
	}

	// Use this for initialization
	void Start () {
		//get the screen's width
		sWidth = Screen.width;
		sHeight = Screen.height;
		//calculate the rescale ratio
		guiRatioX = sWidth / 1280;
		guiRatioY = sHeight / 720;
		//create a rescale Vector3 with the above ratio
		GUIsF = new Vector3(guiRatioX, guiRatioY, 1);

		if (null == AudioWordDetection ||
		    null == Mic)
		{
			Debug.LogError("Missing meta references");
			return;
		}
		
		// prepopulate words
		AudioWordDetection.Words.Add(new WordDetails() { Label = "Noise" });
		AudioWordDetection.Words.Add(new WordDetails() { Label = "Ultimate" });
		
		//subscribe detection event
		AudioWordDetection.WordDetectedEvent += WordDetectedHandler;
	}
	
	// Update is called once per frame
	void Update () {
		// Ambil data suara dari Mic
		try
		{
			GetMicData();
			if (null == m_micData)
			{
				return;
			}
		}
		catch (System.Exception ex)
		{
			Debug.Log(string.Format("Update exception={0}", ex));
		}
	}

	public void VoiceOption() {
		GUI.Label(new Rect(400,100,500,100), "Music");
		GUI.Label(new Rect(400,150,500,100), "Sound Effect");
		GUI.Label(new Rect(400,250,500,100), "Voice Control");
		GUI.Label(new Rect(400,300,500,100), "<size=20>(Used to activate your gasing's ultimate skill)</size>");
		if (GUI.Button(new Rect(800, 100, 100, 50), bgmText))
		{
			if (GamePrefs.isBGM) 
			{
				GamePrefs.isBGM = false;
				bgmText = "OFF";
			} else {
				GamePrefs.isBGM = true;
				bgmText = "ON";
			}
		}
		if (GUI.Button(new Rect(800, 150, 100, 50), sfxText))
		{
			if (GamePrefs.isSFX) 
			{
				GamePrefs.isSFX = false;
				sfxText = "OFF";
			} else {
				GamePrefs.isSFX = true;
				sfxText = "ON";
			}
		}
		if (GUI.Button(new Rect(800, 250, 100, 50), voiceText))
		{
			if (GamePrefs.isVoiceUsed) 
			{
				GamePrefs.isVoiceUsed = false;
				voiceText = "OFF";
			} else {
				GamePrefs.isVoiceUsed = true;
				voiceText = "ON";
			}
		}
	}

	// Update GUI
	// Print FPS ke layar dan Set Microphone
	void OnGUI() {
		GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
		GUI.skin = customSkin;
		try {
			// Hitung jumlah FPS
			++m_frames;
			
			if (m_timerFrames < DateTime.Now)
			{
				if (m_frames == 0)
				{
					m_framesPerSecond = 0f;
				}
				else
				{
					m_framesPerSecond = m_frames;
				}
				m_timerFrames = DateTime.Now + TimeSpan.FromSeconds(1);
				m_frames = 0;
			}

			GUI.Label(new Rect(1100,10,500,100), string.Format("<size=20>FPS: {0:F2}</size>", m_framesPerSecond));
			GUI.Label(new Rect(500,10,800,100), string.Format("<size=20>Detected Mic: " + Mic.DeviceName + "</size>"));

			if (string.IsNullOrEmpty(Mic.DeviceName))
			{	
				foreach (string device in Microphone.devices)
				{
					if (string.IsNullOrEmpty(device))
					{
						continue;
					}
					
					Mic.DeviceName = device;
				}
			}
		}
		catch (System.Exception ex)
		{
			Debug.Log(string.Format("OnGUI exception={0}", ex));
		}

		// Opsi suara
		VoiceOption();

		// Fungsi word detection diluar try-catch
		if (null == AudioWordDetection ||
		    null == Mic ||
		    string.IsNullOrEmpty(Mic.DeviceName))
		{
			return;
		}

		SaveProfile(FILE_PROFILES);

		if (!haveLoad) 
		{
			LoadProfile(FILE_PROFILES);
			haveLoad = true;
		}

//		if (GUILayout.Button("<size=30>Add Jurus</size>", GUILayout.Height(90)))
//		{
//			WordDetails details = new WordDetails();
//			details.Label = "Jurus " + AudioWordDetection.Words.Count.ToString();
//			AudioWordDetection.Words.Add(details);
//		}

		Color backgroundColor = GUI.backgroundColor;
		GUILayout.BeginArea(new Rect(280,380,900,800));

		if (GamePrefs.isVoiceUsed) {
			// Untuk setiap word yang direkam, maka.....
			for (int wordIndex = 0; wordIndex < AudioWordDetection.Words.Count; ++wordIndex)
			{
				// word yang terdeteksi diwarnain
//				if (AudioWordDetection.ClosestIndex == wordIndex)
//				{
//					GUI.backgroundColor = Color.green;
//				}
//				else
//				{
//					GUI.backgroundColor = backgroundColor;
//				}
				// GUI berfungsi jika Noise tidak null
	//			if (wordIndex > 0)
	//			{
	//				GUI.enabled = null != GetWord("Noise").SpectrumReal;
	//			}
				
				// print GUI untuk tiap word
				GUILayout.BeginHorizontal();
				WordDetails details = AudioWordDetection.Words[wordIndex];
				GUILayout.Label(details.Label);
				GUILayout.Button(string.Format("{0}", (null == details.SpectrumReal) ? "not set" : "set"));
				
				// rekam word dan catat labelnya
				Event e = Event.current;
				if (null != e)
				{
					Rect rect = GUILayoutUtility.GetLastRect();
					bool overButton = rect.Contains(e.mousePosition);
					
					if (m_buttonIndex == -1 &&
					    m_timerStart == DateTime.MinValue &&
					    Input.GetMouseButton(0) &&
					    overButton)
					{
						//Debug.Log("Initial button down");
						m_buttonIndex = wordIndex;
						m_startPosition = Mic.GetPosition();
						m_timerStart = DateTime.Now + TimeSpan.FromSeconds(Mic.CaptureTime);
					}
					if (m_buttonIndex == wordIndex)
					{
						bool buttonUp = Input.GetMouseButtonUp(0);
						if (m_timerStart > DateTime.Now &&
						    !buttonUp)
						{
							//Debug.Log("Button still pressed");
						}
						else if (m_timerStart != DateTime.MinValue &&
						         m_timerStart < DateTime.Now)
						{
							//Debug.Log("Button timed out");
							SetupWordProfile(false);
							m_timerStart = DateTime.MinValue;
							m_buttonIndex = -1;
						}
						else if (m_timerStart != DateTime.MinValue &&
						         buttonUp &&
						         m_buttonIndex != -1)
						{
							//Debug.Log("Button is no longer pressed");
							SetupWordProfile(true);
							m_timerStart = DateTime.MinValue;
							m_buttonIndex = -1;
						}
					}
				}
				
				// play audio untuk word yang udah direkam
				GUI.enabled = null != details.Audio;
				if (GUILayout.Button("Play", GUILayout.Height(90)))
				{
					if (null != details.Audio)
					{
						if (NormalizeWave)
						{
							audio.PlayOneShot(details.Audio, 0.1f);
						}
						else
						{
							audio.PlayOneShot(details.Audio);
						}
					}
				}
				
				// GUI untuk menghapus word yang sudah direkam
	//			GUI.enabled = wordIndex > 0;
	//			if (wordIndex > 0 &&
	//			    GUILayout.Button("<size=30>Remove</size>", GUILayout.Height(90)))
	//			{
	//				AudioWordDetection.Words.RemoveAt(wordIndex);
	//				--wordIndex;
	//			}
				
				// tampilkan score
				Color color = GUI.color;
				GUI.color = Color.white;
				GUILayout.Label(details.Score.ToString());
				GUI.color = color;
				GUILayout.EndHorizontal();
			}
		}
		GUILayout.EndArea();
		GUI.backgroundColor = backgroundColor;
	}

	// Handler untuk event word detection
	void WordDetectedHandler(object sender, WordDetection.WordEventArgs args)
	{
		if (string.IsNullOrEmpty(args.Details.Label))
		{
			return;
		}
		
		//Debug.Log(string.Format("Detected: {0}", args.Details.Label));
	}

	private void SetupWordProfile(bool playAudio)
	{
		SetupWordProfile(playAudio, m_buttonIndex == 0, m_buttonIndex);
	}
	
	// Setup profil dari word
	private void SetupWordProfile(bool playAudio, bool isNoise, int wordIndex)
	{
		if (null == AudioWordDetection ||
		    null == Mic ||
		    string.IsNullOrEmpty(Mic.DeviceName))
		{
			return;
		}
		
		if (wordIndex < 0 ||
		    wordIndex >= AudioWordDetection.Words.Count)
		{
			return;
		}
		
		WordDetails details = AudioWordDetection.Words[wordIndex];
		
		float[] wave = Mic.GetLastData();
		if (null != wave)
		{
			//allocate for the wave copy
			int size = wave.Length;
			if (null == details.Wave ||
			    details.Wave.Length != size)
			{
				details.Wave = new float[size];
				if (null != details.Audio)
				{
					UnityEngine.Object.DestroyImmediate(details.Audio, true);
					details.Audio = null;
				}
			}
			
			//trim the wave
			int position = Mic.GetPosition();
			
			//get the trim size
			int trim = 0;
			if (m_startPosition < position)
			{
				trim = position - m_startPosition;
			}
			else
			{
				trim = size - m_startPosition + position;
			}
			
			//zero the existing wave
			for (int index = 0; index < size; ++index)
			{
				details.Wave[index] = 0f;
			}
			
			//shift array
			for (int index = 0, i = m_startPosition; index < trim; ++index, i = (i + 1) % size)
			{
				details.Wave[index] = wave[i];
			}
			
			//clear existing mic data
			for (int index = 0; index < size; ++index)
			{
				wave[index] = 0;
			}
			
			if (NormalizeWave &&
			    !isNoise)
			{
				//normalize the array
				Mic.NormalizeWave(details.Wave);
			}
			
			SetupWordProfile(details, isNoise);
			
			//play the audio
			if (null == details.Audio)
			{
				details.Audio = AudioClip.Create(string.Empty, size, 1, Mic.SampleRate, false, false);
			}
			details.Audio.SetData(details.Wave, 0);
			audio.loop = false;
			audio.mute = false;
			if (playAudio)
			{
				if (NormalizeWave)
				{
					audio.PlayOneShot(details.Audio, 0.1f);
				}
				else
				{
					audio.PlayOneShot(details.Audio);
				}
			}
		}
	}
	
	private void SetupWordProfile(WordDetails details, bool isNoise)
	{
		if (null == AudioWordDetection ||
		    null == Mic ||
		    string.IsNullOrEmpty(Mic.DeviceName))
		{
			return;
		}
		
		int size = details.Wave.Length;
		int halfSize = size/2;
		
		//allocate profile spectrum, real
		if (null == details.SpectrumReal ||
		    details.SpectrumReal.Length != halfSize)
		{
			details.SpectrumReal = new float[halfSize];
		}
		
		//allocate profile spectrum, imaginary
		if (null == details.SpectrumImag ||
		    details.SpectrumImag.Length != halfSize)
		{
			details.SpectrumImag = new float[halfSize];
		}
		
		//get the spectrum for the trimmed word
		if (null != details.Wave &&
		    details.Wave.Length > 0)
		{
			Mic.GetSpectrumData(details.Wave, details.SpectrumReal, details.SpectrumImag, FFTWindow.Rectangular);
		}
		
		//filter noise
		if (RemoveSpectrumNoise)
		{
			if (isNoise)
			{
				Refilter();
			}
			else
			{
				Mic.RemoveSpectrumNoise(GetWord("Noise").SpectrumReal, details.SpectrumReal);
			}
		}
	}

	// print tombol untuk save profile pemain
	private void SaveProfile(string key) 
	{
		if (GamePrefs.isVoiceUsed) {
			if (GUI.Button(new Rect(500, 630, 300, 50), "Save Voice"))
			{
				//AudioWordDetection.SaveProfiles(new FileInfo(key));
				AudioWordDetection.SaveProfilesPrefs(key);
			}
		}
	}

	// Load Profile pemain jika ada
	private void LoadProfile(string key) 
	{
		if (//AudioWordDetection.LoadProfiles(new FileInfo(key)) //||
		    AudioWordDetection.LoadProfilesPrefs(key))
			//)
		{
			for (int wordIndex = 0; wordIndex < AudioWordDetection.Words.Count; ++wordIndex)
			{
				WordDetails details = AudioWordDetection.Words[wordIndex];
				
				if (null != details.Wave &&
				    details.Wave.Length > 0)
				{
					if (null == details.Audio)
					{
						details.Audio = AudioClip.Create(string.Empty, details.Wave.Length, 1, Mic.SampleRate, false,
						                                 false);
					}
					details.Audio.SetData(details.Wave, 0);
					audio.loop = false;
					audio.mute = false;
				}
				
				SetupWordProfile(details, false);
			}
		}
	}

	// Hilangkan noise di semua word yang sudah direkam
	private void Refilter()
	{
		if (null == AudioWordDetection)
		{
			return;
		}
		for (int index = 1; index < AudioWordDetection.Words.Count; ++index)
		{
			WordDetails details = AudioWordDetection.Words[index];
			Mic.RemoveSpectrumNoise(GetWord("Noise").SpectrumReal, details.SpectrumReal);
		}
	}
}
