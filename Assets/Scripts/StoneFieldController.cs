using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour
{

    public GameObject pemain;
    public GameObject musuh;
    public Gasing gasingPemain;
    public Gasing gasingMusuh;
    private int jumlahMusuh;        
	public GameObject[] listObstacle;

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
				Object objekGasing;
				switch (Utilities.playerGasing){
					case "arjuna":
						objekGasing =  Resources.Load("Prefab/Prefab Gasing/Arjuna");
						break;
					case "srikandi":
						objekGasing =  Resources.Load("Prefab/Prefab Gasing/Srikandi");
						break;
					case "prototype":
						objekGasing =  Resources.Load("Prefab/Prefab Gasing/Prototype");
						break;
					case "dipati":
						objekGasing =  Resources.Load("Prefab/Prefab Gasing/Dipati");
						break;
					case "jalaprang":
						objekGasing =  Resources.Load("Prefab/Prefab Gasing/Jalaprang");
						break;
				default:
						objekGasing =  Resources.Load("Prefab/Prefab Gasing/Arjuna");
						break;
				}

				pemain = (GameObject)Instantiate(objekGasing, new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
           		pemain.name = "Pemain";
                
                if (!gasingPemain)
                {
                    gasingPemain = pemain.GetComponent<Gasing>();
                }
            }
            if (!musuh)
            {                
				Object objekGasing2;
				switch (Utilities.enemy1){
				case "arjuna":
					objekGasing2 =  Resources.Load("Prefab/Prefab Gasing/Arjuna_Musuh");
					break;
				case "srikandi":
					objekGasing2 =  Resources.Load("Prefab/Prefab Gasing/Srikandi_Musuh");
					break;
				case "prototype":
					objekGasing2 =  Resources.Load("Prefab/Prefab Gasing/Prototype_Musuh");
					break;
				case "dipati":
					objekGasing2 =  Resources.Load("Prefab/Prefab Gasing/Dipati_Musuh");
					break;
				case "jalaprang":
					objekGasing2 =  Resources.Load("Prefab/Prefab Gasing/Jalaprang_Musuh");
					break;
				default:
					objekGasing2 =  Resources.Load("Prefab/Prefab Gasing/Arjuna_Musuh");
					break;
				}
				musuh = (GameObject) Instantiate(objekGasing2, new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
			    
        
				if(jumlahMusuh > 1){
					Object objekGasing3;
					switch (Utilities.enemy2){
					case "arjuna":
						objekGasing3 =  Resources.Load("Prefab/Prefab Gasing/Arjuna_Musuh");
						break;
					case "srikandi":
						objekGasing3 =  Resources.Load("Prefab/Prefab Gasing/Srikandi_Musuh");
						break;
					case "prototype":
						objekGasing3 =  Resources.Load("Prefab/Prefab Gasing/Prototype_Musuh");
						break;
					case "dipati":
						objekGasing3 =  Resources.Load("Prefab/Prefab Gasing/Dipati_Musuh");
						break;
					case "jalaprang":
						objekGasing3 =  Resources.Load("Prefab/Prefab Gasing/Jalaprang_Musuh");
						break;
					default:
						objekGasing3 =  Resources.Load("Prefab/Prefab Gasing/Arjuna_Musuh");
						break;
					}
					Instantiate(objekGasing3, new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
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
