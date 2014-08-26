using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class SkillController : MonoBehaviour
{
	// Komponen Voice-Recog
	public GameObject MicG = null;
	public GameObject WordG = null;
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
	
	// Ambil data dari Mic di device
	private void GetMicData()
	{
		m_micData = Mic.GetData(0);
	}

    public Skill[] skills = new Skill[3];

    #region untuk GUI
    private float guiRatioX;
    private float guiRatioY;
    private float sWidth;
    private float sHeight;
    private Vector3 GUIsF;
    private int sizegui;
    #endregion

    public bool isAvailable = true;

    public bool ultiAvailable = true;
    public bool ultiReady = false;
    public bool countdownStarted = false;
    public float startTime;

    // Use this for initialization
    void Start()
    {
		MicG = GameObject.Find("SpectrumMicrophone");
		WordG = GameObject.Find("WordDetection");
		Mic = MicG.GetComponents<SpectrumMicrophone>()[0];
		AudioWordDetection = WordG.GetComponents<WordDetection>()[0];

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
    void Update()
    {
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

		if (countdownStarted) {
			if (Time.time  - startTime > 2000) {
				ultiReady = false;
			}
		}

		if (ultiReady && !countdownStarted) {
			countdownStarted = true;
			startTime = Time.time;
		}
    }

    void OnGUI()
    {
        if(isAvailable) {
            GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
            if (GamePrefs.isMultiplayer)
            {
                OnGUI_MultiPlayer();
            }
            else
            {
                OnGUI_SinglePlayer();
            }

            if (ultiAvailable && ultiReady) {
            	GUI.Label(new Rect(0,0,500,100), "ULTIMATEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            	UpdateMic();
            }
        }
    }

	private void UpdateMic() {
		// set device mic
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
		
		if (null == AudioWordDetection ||
		    null == Mic ||
		    string.IsNullOrEmpty(Mic.DeviceName))
		{
			return;
		}
		
		// load profile suara pemain
		if (!haveLoad) 
		{
			LoadProfile(FILE_PROFILES);
			haveLoad = true;
		}
		
		if (GamePrefs.isVoiceUsed) {
			// Deteksi Jurus
			if (AudioWordDetection.ClosestIndex == 1 && ultiAvailable) {
				// pake skill
				ultiAvailable = false;
				ultiReady = false;
				DoUltimate();
				WordDetails details = AudioWordDetection.Words[0];
//				Debug.Log(details.Score.ToString());
			}
		}
	}

	public void startUltiCountdown() {
		ultiReady = true;
		Debug.Log("Ulti readyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
	}

	public void DoUltimate() {
		skills[1].doSkill();
	}

    private void OnGUI_SinglePlayer()
    {
        if (skills[0] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[0].buttonSkill1;
//            if (GUI.Button(new Rect(Screen.width * 4 / 5 - Screen.width / 8 / 2, Screen.height * 7 / 10, Screen.width / 8, Screen.width / 8), skills[0].skillName, style))
            if (GUI.Button(new Rect(855, 470, 200, 200), "", style))
            {
                skills[0].doSkill();
            }
        }

        if (skills[1] != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = skills[1].buttonSkill1;
//            if (GUI.Button(new Rect(Screen.width * 9 / 10 - Screen.width / 8 / 2, Screen.height * 5 / 10, Screen.width / 8, Screen.width / 8), skills[1].skillName, style))
            if (GUI.Button(new Rect(1027, 303, 200, 200), "", style))
            {
                skills[1].doSkill();
            }
        }

        //ULTI
		if (!GamePrefs.isVoiceUsed) {
	        if (skills[2] != null)
	        {
	            GUIStyle style = new GUIStyle(GUI.skin.box);
	            style.normal.background = skills[2].buttonSkill1;
	//            if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skills[2].skillName, style))
	            if (GUI.Button(new Rect(60, 370, 300, 300), "", style))
	            {
					if (!GamePrefs.isVoiceUsed)
	                	skills[2].doSkill();
	            }
	        }
		}
    }

    private void OnGUI_MultiPlayer()
    {

    }

	// FUNGSI-FUNGSI ECKY

	// Handler untuk event word detection
	void WordDetectedHandler(object sender, WordDetection.WordEventArgs args)
	{
		if (string.IsNullOrEmpty(args.Details.Label))
		{
			return;
		}
		Debug.Log(string.Format("Detected: {0}", args.Details.Label));
	}
	
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
}
