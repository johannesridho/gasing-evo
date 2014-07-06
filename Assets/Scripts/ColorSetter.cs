using UnityEngine;
using System.Collections;

public class ColorSetter : MonoBehaviour {
	public Color objectColor;

	// Use this for initialization
	void Start () {
		objectColor = Color.black;
		renderer.material.color = objectColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
