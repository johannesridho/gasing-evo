using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

//Define player preferences management methods
public static class Utilities {

	public static string chosenArena{
		get{return PlayerPrefs.GetString("chosenArenaSingle");}
		set{PlayerPrefs.SetString("chosenArenaSingle", value);}
	}

	public static int chosenMode{
		get{return PlayerPrefs.GetInt("chosenMode");}
		set{PlayerPrefs.SetInt("chosenMode", value);}
	}

	public static int howManyGasingRoyal{
		get{return PlayerPrefs.GetInt("howManyGasingRoyal");}
		set{PlayerPrefs.SetInt("howManyGasingRoyal", value);}
	}

	public static int howManyGasingTeam{
		get{return PlayerPrefs.GetInt("howManyGasingTeam");}
		set{PlayerPrefs.SetInt("howManyGasingTeam", value);}
	}

	public static string playerGasing{
		get{return PlayerPrefs.GetString("playerGasingRoyal");}
		set{PlayerPrefs.SetString("playerGasingRoyal", value);}
	}

	public static string ally1{
		get{return PlayerPrefs.GetString("ally1");}
		set{PlayerPrefs.SetString("ally1", value);}
	}
	
	public static string ally2{
		get{return PlayerPrefs.GetString("ally2");}
		set{PlayerPrefs.SetString("ally2", value);}
	}

	public static string enemy1{
		get{return PlayerPrefs.GetString("enemy1");}
		set{PlayerPrefs.SetString("enemy1", value);}
	}

	public static string enemy2{
		get{return PlayerPrefs.GetString("enemy2");}
		set{PlayerPrefs.SetString("enemy2", value);}
	}

	public static string enemy3{
		get{return PlayerPrefs.GetString("enemy3");}
		set{PlayerPrefs.SetString("enemy3", value);}
	}
	public static string ultiTarget{
		get{return PlayerPrefs.GetString("ultiTarget");}
		set{PlayerPrefs.SetString("ultiTarget", value);}	public static bool victory{
		get{return PlayerPrefs.GetBool("victory");}
		set{PlayerPrefs.SetBool("victory", value);}}
