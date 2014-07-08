using UnityEngine;
using System.Collections;

public class StoneFieldController : MonoBehaviour
{

    public GameObject pemain;
    public GameObject musuh;
    public Gasing gasingPemain;
    public Gasing gasingMusuh;
    public int jumlahMusuh;
    public GameObject[] listPemain;
    public GameObject[] listMusuh;

    public bool isMultiplayer = true;

    void Awake()
    {
        if (!isMultiplayer)
        {
            //		jumlahMusuh = 3;
            if (!pemain)
            {
                //			pemain = GameObject.Find ("Pemain");
                if (listPemain.Length != 0)
                {
                    //				listPemain[0].name = "tes";
                    //				Instantiate(listPemain[0], new Vector3(0, 1, -15), Quaternion.Euler(270,0,0));
                    //				pemain = listPemain[0];
                    pemain = (GameObject)Instantiate(listPemain[0], new Vector3(0, 1, -15), Quaternion.Euler(270, 0, 0));		//hidupin gasing, pilih prefab
                    pemain.name = "Pemain";
                }
                if (!gasingPemain)
                {
                    gasingPemain = pemain.GetComponent<Gasing>();

                    //set atribut gasing sesuai jenisnya
                    //				gasingPemain.setEPMax(100);
                    //				gasingPemain.setSPMax(100);
                    //				gasingPemain.setSpeed(1);
                    //				gasingPemain.setPower(1);
                    //				gasingPemain.setMass(100);
                }
            }
        }
        if (!musuh)
        {
            //			musuh = GameObject.Find ("Musuh");
            if (listMusuh.Length != 0)
            {
                Instantiate(listMusuh[0], new Vector3(0, 1, 10), Quaternion.Euler(270, 0, 0));
                musuh = listMusuh[0];
            }
            if (!gasingMusuh)
            {
                gasingMusuh = musuh.GetComponent<Gasing>();
                //				gasingMusuh.setEPMax(100);
                //				gasingMusuh.setSPMax(100);
                //				gasingMusuh.setSpeed(1);
                //				gasingMusuh.setPower(1);
                //				gasingMusuh.setMass(100);			

                for (int i = 1; i < jumlahMusuh; i++)
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
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gasingPemain.getEP() <= 0 || GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            Application.LoadLevel("GameOver");
        }
    }
}
