﻿using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour
{

    public GameObject pemain;
    public GameObject musuh;
    public Gasing gasingPemain;
    public Gasing gasingMusuh;
    private int jumlahMusuh;        
	private AudioClip audioBattle;
	protected bool paused = false;

    protected void Awake()
    {
		GameObject menuMusic = GameObject.Find ("Background Music");
		if (menuMusic) {				
			Destroy(menuMusic);			//hancurin background music menu
		}
        if (GamePrefs.isMultiplayer)
        {
            
        }
		else if(Utilities.chosenMode == 0 || Utilities.chosenMode == 2)		//royal mode atau arcade mode
        {
    		jumlahMusuh = Utilities.howManyGasingRoyal-1;
            if (!pemain)
            {   
				if(Utilities.playerGasing != null){
					pemain = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.playerGasing), new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
				}else{
					pemain = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed"), new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
				}
           		pemain.name = "Pemain";
                
                if (!gasingPemain)
                {
                    gasingPemain = pemain.GetComponent<Gasing>();
                }
            }

			if(Utilities.enemy1 != null){
				musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy1+"_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
			}else{
				musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
			}

			if(Utilities.enemy2 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy2+"_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy3 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy3+"_Musuh"), new Vector3(5, 1, 10), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy4 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy4+"_Musuh"), new Vector3(-10, 1, 10), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy5 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy5+"_Musuh"), new Vector3(10, 1, 10), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy6 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy6+"_Musuh"), new Vector3(-15, 1, 10), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy7 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy7+"_Musuh"), new Vector3(15, 1, 10), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy8 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy8+"_Musuh"), new Vector3(-5, 1, 15), Quaternion.Euler(270, 0, 0));
			}
			if(Utilities.enemy9 != null){
				Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy9+"_Musuh"), new Vector3(5, 1, 15), Quaternion.Euler(270, 0, 0));
			}

//            if (!musuh)
//            {
//				if(Utilities.enemy1 != null){
//					musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy1+"_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
//				}else{
//					musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
//				}
//        
//				if(jumlahMusuh > 1){
//					if(Utilities.enemy2 != null){
//						Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy2+"_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
//					}else{
//						Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
//					}
//				}
//				if(jumlahMusuh > 2){
					//karena layar ga muat buat pilih gasing, klo musuh lebih dari 2 , musuh diclone dari objek musuh pertama
//                    for (int i = 2; i < jumlahMusuh; i++)
//                    {
//                        if (i <= 4)
//                        {
//                            Instantiate(musuh, new Vector3(i * (-5), 1, 10), Quaternion.Euler(270, 0, 0));
//                        }
//                        else if (i <= 7)
//                        {
//                            Instantiate(musuh, new Vector3((i - 4) * 5, 1, 10), Quaternion.Euler(270, 0, 0));
//                        }
//                        else if (i <= 11)
//                        {
//                            Instantiate(musuh, new Vector3((i - 7) * 5, 1, -10), Quaternion.Euler(270, 0, 0));
//                        }
//                        else if (i <= 14)
//                        {
//                            Instantiate(musuh, new Vector3((i - 11) * (-5), 1, -10), Quaternion.Euler(270, 0, 0));
//                        }
//                        else if (i <= 17)
//                        {
//                            Instantiate(musuh, new Vector3((i - 14) * 5, 1, -15), Quaternion.Euler(270, 0, 0));
//                        }
//                        else if (i <= 20)
//                        {
//                            Instantiate(musuh, new Vector3((i - 17) * (-5), 1, -15), Quaternion.Euler(270, 0, 0));
//                        }
//                    }
//                }				
//            }//end if !musuh
        } else {				//team mode
			jumlahMusuh = Utilities.howManyGasingTeam * 2 - 1;
			if (!pemain)
			{   
				if(Utilities.playerGasing != null){
					pemain = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.playerGasing), new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
				}else{
					pemain = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed"), new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
				}
				pemain.name = "Pemain";
				
				if (!gasingPemain)
				{
					gasingPemain = pemain.GetComponent<Gasing>();
				}
			}
			if (!musuh)
			{
				if(Utilities.enemy1 != null){
					musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy1+"_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
				}

				if(Utilities.enemy2 != null){
					Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy2+"_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
				}

				if(Utilities.enemy3 != null){
					Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy3+"_Musuh"), new Vector3(5, 1, 10), Quaternion.Euler(270, 0, 0));
				}
				Debug.Log("++++++++++++++++ "+Utilities.ally1);
				if(Utilities.ally1 != null){
					GameObject teman1 = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.ally1+"_Musuh"), new Vector3(5, 1, -15), Quaternion.Euler(270, 0, 0));
					teman1.tag = "Ally";
				}

				if(Utilities.ally2 != null){
					GameObject teman2 = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.ally2+"_Musuh"), new Vector3(-5, 1, -15), Quaternion.Euler(270, 0, 0));
					teman2.tag = "Ally";
				}
													
			}//end if !musuh

		}//end if team mode

		


    }

    protected void OnPauseGame ()
	{
		paused = true;
		Debug.Log("paused");
	}
	 
	protected void OnResumeGame ()
	{
		paused = false;
	}

    // Use this for initialization
    protected void Start()
    {
		audioBattle = (AudioClip) Resources.Load("Audio/Battle");
		if(audioBattle){
			audio.loop = true;
			audio.clip = audioBattle;
			audio.panLevel = 0;
			audio.volume = 0.1f;
			audio.Play();
		}
    }

    // Update is called once per frame
	protected void Update()
    {
		if(GamePrefs.isBGM == false){
			AudioListener.pause = true;
			AudioListener.volume = 0;
		}else{
			AudioListener.pause = false;
			AudioListener.volume = 1;
		}

        if (GamePrefs.isMultiplayer)
        {
            if (MultiplayerManager.instance.isGameStarted)
            {
                if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
                {
                    Debug.Log("1 player left");
                    if (Network.isServer)
                    {
                        MultiplayerManager.instance.decideWinner();
                    }
                }
            }
        }
		else if(Utilities.chosenMode == 1) //team mode
		{
			if ((GameObject.FindGameObjectsWithTag("Player").Length <= 0 && GameObject.FindGameObjectsWithTag("Ally").Length <=0))
			{
				//losing condition
				Utilities.victory = false;
				Application.LoadLevel("GameOver");
			}
			else if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0){
				//winning condition
				Utilities.victory = true;
				Application.LoadLevel("GameOver");
			}
		}
		else if(Utilities.chosenMode == 0)		//royal mode
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
            {
				//losing condition
				Utilities.victory = false;
				Application.LoadLevel("GameOver");
            }
			else if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0){
				//winning condition
				Utilities.victory = true;
				Application.LoadLevel("GameOver");
			}
        }
		else 	//story mode
		{
			if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
			{
				//losing condition
				Utilities.victory = false;
				Application.LoadLevel("GameOver");
			}
			else if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0){
				//winning condition
				Utilities.victory = true;
				if(Utilities.storyModeLevel == 7){			//klo udah menang level 7 lgsg masuk credit
					Application.LoadLevel("Credit");
				}else{
					Application.LoadLevel("GameOver");
				}
			}
		}
    }

	void OnGUI(){
		if(Time.timeScale == 0){		//if paused 
			GUIStyle style = new GUIStyle(GUI.skin.box);
			float lebar = Screen.width / 2;
			float tinggi = Screen.height / 4;
			//		style.normal.background = skills.buttonSkill1;
			//		    if (GUI.Button(new Rect(Screen.width * 1 / 5, Screen.height * 7 / 10, Screen.width / 7, Screen.height / 8), "tes", style))
			if (GUI.Button(new Rect(Screen.width * 1/2 - lebar / 2, tinggi / 2, lebar, tinggi), "Restart", style))
			{
				Application.LoadLevel(Utilities.chosenArena);
				Time.timeScale = 1;
			}
			if (GUI.Button(new Rect(Screen.width * 1/2 - lebar / 2, tinggi * 3 / 2, lebar, tinggi), "Main Menu", style))
			{
				Application.LoadLevel("Main Menu");
				Time.timeScale = 1;
			}
			if (GUI.Button(new Rect(Screen.width * 1/2 - lebar / 2, tinggi * 5 / 2, lebar, tinggi), "Quit Game", style))
			{
				Application.Quit();
				Time.timeScale = 1;
			}
			
		}
	}
}
