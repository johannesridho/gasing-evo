using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour
{

    public GameObject pemain;
    public GameObject musuh;
    public Gasing gasingPemain;
    public Gasing gasingMusuh;
    private int jumlahMusuh;
    public GameObject[] listPemain;
    public GameObject[] listMusuh;
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
                if (listPemain.Length != 0)
                {
					int nomorGasing = 0;
					switch (Utilities.playerGasing){
						case "arjuna":
							nomorGasing = 0;
							break;
						case "srikandi":
							nomorGasing = 1;
							break;
						case "prototype":
							nomorGasing = 2;
							break;
						default:
							nomorGasing = 0;
							break;
					}

                    pemain = (GameObject)Instantiate(listPemain[nomorGasing], new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
                    pemain.name = "Pemain";
                }
                if (!gasingPemain)
                {
                    gasingPemain = pemain.GetComponent<Gasing>();
                }
            }
            if (!musuh)
            {
                if (listMusuh.Length != 0)
                {
					int nomorGasing = 0;
					switch (Utilities.enemy1){
					case "arjuna":
						nomorGasing = 0;
						break;
					case "srikandi":
						nomorGasing = 1;
						break;
					case "prototype":
						nomorGasing = 2;
						break;
					default:
						nomorGasing = 0;
						break;
					}
                    Instantiate(listMusuh[nomorGasing], new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
                    musuh = listMusuh[nomorGasing];
                
					if(jumlahMusuh > 1){
						int nomorGasingMsh2 = 0;
						switch (Utilities.enemy2){
						case "arjuna":
							nomorGasingMsh2 = 0;
							break;
						case "srikandi":
							nomorGasingMsh2 = 1;
							break;
						case "prototype":
							nomorGasingMsh2 = 2;
							break;
						default:
							nomorGasingMsh2 = 0;
							break;
						}
						Instantiate(listMusuh[nomorGasingMsh2], new Vector3(-5, 1, 10), Quaternion.Euler(270, 0, 0));
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
				}//end if listmusuh tidak kosong 
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
