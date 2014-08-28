using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MultiplayerSkillContoller : MonoBehaviour
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

    public string[] availableSkills = new string[4] { "", "", "", "" };

    private bool isActive = false;

    public Texture2D[] skillButtons;

    public MultiplayerInputHandler mpInputHandler;

    private bool isSkillInitialized = false;


    #region untuk GUI
    private float guiRatioX;
    private float guiRatioY;
    private float sWidth;
    private float sHeight;
    private Vector3 GUIsF;
    private int sizegui;
    #endregion


    public bool ultiAvailable = true;
    public bool ultiReady = false;
    public bool countdownStarted = false;
    public float startTime;


    // Use this for initialization
    void Start()
    {
        //get the screen's width
        sWidth = Screen.width;
        sHeight = Screen.height;
        //calculate the rescale ratio
        guiRatioX = sWidth / 1280;
        guiRatioY = sHeight / 720;
        //create a rescale Vector3 with the above ratio
        GUIsF = new Vector3(guiRatioX, guiRatioY, 1);




    }

    // Update is called once per frame
    void Update()
    {
        if (MultiplayerManager.instance.isGameStarted)
        {
            if (Network.isServer)
            {
                if (!isSkillInitialized)
                {
                    initializeClientsSkills();
                    isSkillInitialized = true;
                }

                checkForUlti();
            }

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

            if (countdownStarted)
            {
                if (Time.time - startTime > 2000)
                {
                    ultiReady = false;
                }
            }

            if (ultiReady && !countdownStarted)
            {
                countdownStarted = true;
                startTime = Time.time;
            }

            if (!MultiplayerManager.instance.isGameStarted)
            {
                setActive(false);
            }
        }
    }

    void OnGUI()
    {

        if (isActive)
        {
            GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
            if (MultiplayerManager.instance.isGameStarted)
            {
                if (MultiplayerManager.instance.isDedicatedServer && !Network.isServer && MultiplayerManager.instance.isGameStarted)
                {
                    //health
                    GUI.DrawTexture(new Rect(0, 0, 1280, 720), mpInputHandler.blackScreenTexture);
                    GUIStyle style = new GUIStyle(GUI.skin.box);
                    //		Texture2D texture = new Texture2D(1, 1);
                    //		texture.SetPixel (1, 1, Color.green);
                    //		texture.Apply ();
                    style.normal.background = mpInputHandler.teksturHealth;

                    GUIStyle style2 = new GUIStyle(GUI.skin.box);
                    //		Texture2D texture2 = new Texture2D(1, 1);
                    //		texture2.SetPixel (1, 1, Color.red);
                    //		texture2.Apply ();
                    style2.normal.background = mpInputHandler.teksturHealth2;

                    GUIStyle styleSkill = new GUIStyle(GUI.skin.box);
                    styleSkill.normal.background = mpInputHandler.teksturSkill;

                    GUIStyle styleSkill2 = new GUIStyle(GUI.skin.box);
                    styleSkill2.normal.background = mpInputHandler.teksturSkill2;

                    GUI.Box(new Rect(40, 114, 1200, 60), "", style2);
                    GUI.Box(new Rect(40, 114, mpInputHandler.healthBarLength, 60), "", style);

                    GUI.Box(new Rect(40, 186, 1200, 60), "", styleSkill2);
                    GUI.Box(new Rect(40, 186, mpInputHandler.skillBarLength, 60), "", styleSkill);
                }

                if ((!MultiplayerManager.instance.isDedicatedServer) || (Network.isClient && MultiplayerManager.instance.isDedicatedServer))
                {
                    if (availableSkills[0] != null)
                    {
                        GUIStyle style1 = new GUIStyle(GUI.skin.box);
                        style1.normal.background = getButton(availableSkills[0]);
                        //            if (GUI.Button(new Rect(Screen.width * 4 / 5 - Screen.width / 8 / 2, Screen.height * 7 / 10, Screen.width / 8, Screen.width / 8), skills[0].skillName, style))
                        if (GUI.Button(new Rect(855, 470, 200, 200), "", style1))
                        {
                            if (Network.isServer)
                            {
                                server_doSkill(Network.player, 0);
                            }
                            else
                            {
                                networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 0);
                            }
                        }
                    }

                    if (availableSkills[1] != null)
                    {
                        GUIStyle style1 = new GUIStyle(GUI.skin.box);
                        style1.normal.background = getButton(availableSkills[1]);
                        //            if (GUI.Button(new Rect(Screen.width * 9 / 10 - Screen.width / 8 / 2, Screen.height * 5 / 10, Screen.width / 8, Screen.width / 8), skills[1].skillName, style))
                        if (GUI.Button(new Rect(1027, 303, 200, 200), "", style1))
                        {
                            if (Network.isServer)
                            {
                                server_doSkill(Network.player, 1);
                            }
                            else
                            {
                                networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 1);
                            }
                        }
                    }

                    if (ultiReady)
                    {
                        if (availableSkills[2] != null)
                        {
                            GUIStyle style1 = new GUIStyle(GUI.skin.box);
                            style1.normal.background = getButton(availableSkills[2]);
                            //            if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), skills[2].skillName, style))
                            if (GUI.Button(new Rect(60, 370, 300, 300), "", style1))
                            {
                                if (Network.isServer)
                                {
                                    server_doSkill(Network.player, 2);
                                }
                                else
                                {
                                    networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 2);
                                }
                            }
                        }
                    }

                    if (ultiAvailable && ultiReady)
                    {
                        GUI.Label(new Rect(0, 0, 500, 100), "ULTIMATEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                        UpdateMic();
                    }
                }
            }
        }

    }

    private void UpdateMic()
    {
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

        if (GamePrefs.isVoiceUsed)
        {
            // Deteksi Jurus
            if (AudioWordDetection.ClosestIndex == 1 && ultiAvailable)
            {
                // pake skill
                ultiAvailable = false;
                ultiReady = false;
                if (Network.isServer)
                {
                    server_doSkill(Network.player, 2);
                }
                else
                {
                    networkView.RPC("server_doSkill", RPCMode.Server, Network.player, 2);
                }
                //DoUltimate();
                WordDetails details = AudioWordDetection.Words[0];
                //				Debug.Log(details.Score.ToString());
            }
        }
    }

    [RPC]
    public void client_animateUlti()
    {
        UnityEngine.Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            if (go.GetComponent<HealthBar>())
                go.GetComponent<HealthBar>().isAvailable = false;
        }
        //GetComponent<SkillController>().isAvailable = false;
        Application.LoadLevelAdditive("ArjunaUltimate");
        if (Network.isServer)
        {
            Invoke("InvokeUlti", 12);
        }
    }

    public void InvokeUlti()
    {
        //skills[2].doSkill();
    }

    public void startUltiCountdown()
    {
        ultiReady = true;
        Debug.Log("Ulti readyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
    }

    private void initializeClientsSkills()
    {
        for (int i = 0; i < MultiplayerManager.instance.serverSideGasings.Count; i++)
        {
            if (MultiplayerManager.instance.serverSideGasings[i] != null)
            {
                if (MultiplayerManager.instance.playerList[i].playerNetwork == Network.player)
                {
                    //for server
                    if (MultiplayerManager.instance.serverSideGasings[i] != null)
                    {
                        if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>() != null)
                        {
                            for (int j = 0; j < MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length; j++)
                            {
                                if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j] != null)
                                {
                                    availableSkills[j] = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j].skillName;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (MultiplayerManager.instance.isAllPlayerReady)
                    {
                        //for other clients
                        string[] stringOfSkills = new string[MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length];
                        for (int j = 0; j < MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills.Length; j++)
                        {
                            if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j] != null)
                            {
                                stringOfSkills[j] = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<SkillController>().skills[j].skillName;

                            }
                        }
                        networkView.RPC("client_sendAvailableSkills", MultiplayerManager.instance.playerList[i].playerNetwork, string.Join("-", stringOfSkills));
                    }
                }
            }
        }
    }

    private void checkForUlti()
    {
        for (int i = 0; i < MultiplayerManager.instance.serverSideGasings.Count; i++)
        {
            if (MultiplayerManager.instance.serverSideGasings[i] != null)
            {
                if (MultiplayerManager.instance.playerList[i].playerNetwork == Network.player)
                {
                    //for server
                    if (MultiplayerManager.instance.serverSideGasings[i] != null)
                    {
                        if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<UltiControl>() != null)
                        {
                            ultiReady = MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<UltiControl>().isCanUlti;
                        }
                    }
                }
                else
                {
                    //for others
                    if (MultiplayerManager.instance.isAllPlayerReady)
                    {
                        if (MultiplayerManager.instance.serverSideGasings[i] != null)
                        {
                            if (MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<UltiControl>() != null)
                            {
                                networkView.RPC("client_setUltiReady", MultiplayerManager.instance.playerList[i].playerNetwork, MultiplayerManager.instance.serverSideGasings[i].GetComponentInChildren<UltiControl>().isCanUlti);
                            }
                        }
                    }
                }
            }
        }
    }

    [RPC]
    public void client_sendAvailableSkills(string stringOfSkills)
    {
        //stringOfSkill ( '-' separated)
        availableSkills = stringOfSkills.Split('-');
    }

    [RPC]
    public void server_doSkill(NetworkPlayer player, int skillIndex)
    {
        List<GameObject> asd = MultiplayerManager.instance.getGasingOwnedByPlayer(player).GetComponentInChildren<SkillController>().skills[skillIndex].mp_findAllTarget();
        MultiplayerManager.instance.getGasingOwnedByPlayer(player).GetComponentInChildren<SkillController>().skills[skillIndex].doSkill();
        string jenisGasing = "";
        if (PlayerPrefs.GetInt("Selected Gasing") == 0)
        {
            jenisGasing = "Colonix";
        }
        else if (PlayerPrefs.GetInt("Selected Gasing") ==1)
        {
            jenisGasing = "Craseed";
        }
        else if (PlayerPrefs.GetInt("Selected Gasing") == 2)
        {
            jenisGasing = "Legasic";
        }
        else if (PlayerPrefs.GetInt("Selected Gasing") == 3)
        {
            jenisGasing = "Prototype";
        }
        else if (PlayerPrefs.GetInt("Selected Gasing") == 4)
        {
            jenisGasing = "Skymir";
        }

        networkView.RPC("client_setSiapaYangUlti", RPCMode.All, MultiplayerManager.instance.getGasingOwnedByPlayer(player).transform.position, jenisGasing);
        if (skillIndex == 2)
        {
            networkView.RPC("client_animateUlti", RPCMode.All);
        }
    }

    [RPC]
    public void client_setSiapaYangUlti(Vector3 playerTransform, string jenisGasing)
    {
        MultiplayerManager.instance.siapaYangUlti = playerTransform;
        MultiplayerManager.instance.jenisGasing = jenisGasing;
    }

    public void setActive(bool active)
    {
        isActive = active;
    }

    private Texture2D getButton(string skillName)
    {
        Texture2D hasil = null;
        if (skillName == "Jump")
        {
            hasil = skillButtons[0];
        }
        else
        {
            hasil = skillButtons[1];
        }
        return hasil;
    }

    void OnLevelWasLoaded(int level)
    {
        isActive = true;

        //untuk voice command
        MicG = GameObject.Find("SpectrumMicrophone");
        WordG = GameObject.Find("WordDetection");
        Mic = MicG.GetComponents<SpectrumMicrophone>()[0];
        AudioWordDetection = WordG.GetComponents<WordDetection>()[0];

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

    /*
     * dikirim ke player yang ultinya ready saja
     */
    [RPC]
    public void client_setUltiReady(bool ready)
    {
        ultiReady = ready;
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
        int halfSize = size / 2;

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
