using UnityEngine;
using System;
using System.Collections;

public class SinglePlayer : MonoBehaviour {

	// Frame-Rate
	private int m_frames = 0;
	private float m_framesPerSecond = 0f;
	private DateTime m_timerFrames = DateTime.Now;
	
	// Komponen VR
	public SpectrumMicrophone Mic = null;
	public WordDetection AudioWordDetection = null;
	private float[] m_micData = null;

	// Ambil data suara dari Mic
	private void GetMicData()
	{
		m_micData = Mic.GetData(0);
	}

	// Use this for initialization
	void Start () {
	
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

	// Print FPS ke layar dan Set Microphone
	void OnGUI() {
		try {
			// Hitung FPS
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

			// Print informasi Mic dan FPS ke Layar
			GUILayout.Label(string.Format("FPS: {0:F2}          Detected Mic: " + Mic.DeviceName, m_framesPerSecond));

			// Cari device microphone
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
	}
}
