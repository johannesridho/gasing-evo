using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//Define player preferences management methods
public static class Utilities {

	public static int chosenGasingSingle{
		get{return PlayerPrefs.GetInt("chosenGasingSingle");}
		set{PlayerPrefs.SetInt ("chosenGasingSingle", value);}
	}

	public static int chosenEnemySingle{
		get{return PlayerPrefs.GetInt("chosenEnemySingle");}
		set{PlayerPrefs.SetInt("chosenEnemySingle", value);}
	}

	public static int chosenArenaSingle{
		get{return PlayerPrefs.GetInt("chosenArenaSingle");}
		set{PlayerPrefs.SetInt("chosenArenaSingle", value);}
	}
}
