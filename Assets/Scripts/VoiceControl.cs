using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class VoiceControl : MonoBehaviour {

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

	// Ambil data dari Mic di device
	private void GetMicData()
	{
		m_micData = Mic.GetData(0);
	}

	// Use this for initialization
	void Start () {
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

	void OnGUI() {
		UpdateMic();
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

//		if (GamePrefs.isVoiceUsed) {
//			// Untuk setiap word yang direkam, maka.....
//			for (int wordIndex = 0; wordIndex < AudioWordDetection.Words.Count; ++wordIndex)
//			{
//				WordDetails details = AudioWordDetection.Words[wordIndex];
//				Debug.Log(details.Score.ToString());
//			}
//		}
	}

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
