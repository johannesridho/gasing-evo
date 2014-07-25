using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour
{

    public GameObject pemain;
    public GameObject musuh;
    public Gasing gasingPemain;
    public Gasing gasingMusuh;
    private int jumlahMusuh;        

    protected void Awake()
    {
        if (GamePrefs.isMultiplayer)
        {
            
        }
        else
        {
    		jumlahMusuh = Utilities.howManyGasingRoyal-1;
            if (!pemain)
            {   
				if(Utilities.playerGasing != null){
					pemain = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.playerGasing), new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
				}else{
					pemain = (GameObject)Instantiate(Resources.Load("Prefab/Prefab Gasing/Arjuna"), new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
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
					musuh = (GameObject) Instantiate(Resources.Load("Prefab/Prefab Gasing/Arjuna_Musuh"), new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
				}
        
				if(jumlahMusuh > 1){
					if(Utilities.enemy2 != null){
						Instantiate(Resources.Load("Prefab/Prefab Gasing/"+Utilities.enemy2+"_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
					}else{
						Instantiate(Resources.Load("Prefab/Prefab Gasing/Arjuna_Musuh"), new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
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
        }//end if single player


    }

    // Use this for initialization
    protected void Start()
    {

    }

    // Update is called once per frame
	protected void Update()
    {
        if (GamePrefs.isMultiplayer)
        {

        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 0 || GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
            {
                Application.LoadLevel("GameOver");
            }
        }
    }
}
