using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour
{

    public GameObject pemain;
    public GameObject musuh;
    public Gasing gasingPemain;
    public Gasing gasingMusuh;
    private int jumlahMusuh;        
	private AudioClip audioBattle;

    protected void Awake()
    {
		Debug.Log ("---------------- "+Utilities.chosenMode);
		GameObject menuMusic = GameObject.Find ("Background Music");
		if (menuMusic) {				
			Destroy(menuMusic);			//hancurin background music menu
		}
        if (GamePrefs.isMultiplayer)
        {
            
        }
        else if(Utilities.chosenMode == 0)		//royal mode
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
            if (!musuh)
            {
				if(Utilities.enemy1 != null){
					musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy1+"_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
				}else{
					musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
				}
        
				if(jumlahMusuh > 1){
					if(Utilities.enemy2 != null){
						Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy2+"_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
					}else{
						Instantiate(Resources.Load("Prefab/Prefab Gasing/Craseed_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
					}
				}
				if(jumlahMusuh > 2){
					//karena layar ga muat buat pilih gasing, klo musuh lebih dari 2 , musuh diclone dari objek musuh pertama
                    for (int i = 2; i < jumlahMusuh; i++)
                    {
                        if (i <= 4)
                        {
                            Instantiate(musuh, new Vector3(i * (-5), 1, 10), Quaternion.Euler(270, 0, 0));
                        }
                        else if (i <= 7)
                        {
                            Instantiate(musuh, new Vector3((i - 4) * 5, 1, 10), Quaternion.Euler(270, 0, 0));
                        }
                        else if (i <= 11)
                        {
                            Instantiate(musuh, new Vector3((i - 7) * 5, 1, -10), Quaternion.Euler(270, 0, 0));
                        }
                        else if (i <= 14)
                        {
                            Instantiate(musuh, new Vector3((i - 11) * (-5), 1, -10), Quaternion.Euler(270, 0, 0));
                        }
                        else if (i <= 17)
                        {
                            Instantiate(musuh, new Vector3((i - 14) * 5, 1, -15), Quaternion.Euler(270, 0, 0));
                        }
                        else if (i <= 20)
                        {
                            Instantiate(musuh, new Vector3((i - 17) * (-5), 1, -15), Quaternion.Euler(270, 0, 0));
                        }
                    }
                }				
            }//end if !musuh
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
        if (GamePrefs.isMultiplayer)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
            {
                Debug.Log("all dead");
            }
        }
		else if(Utilities.chosenMode == 1) //team mode
		{
			if ((GameObject.FindGameObjectsWithTag("Player").Length <= 0 && GameObject.FindGameObjectsWithTag("Ally").Length <=0) || GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
			{
				Application.LoadLevel("GameOver");
			}
		}
        else 		//royal mode
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 0 || GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                Application.LoadLevel("GameOver");
            }
        }
    }
}
