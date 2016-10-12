using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class urldetector : MonoBehaviour {

	public Text txt;
	public string curUrl;
	// Use this for initialization

	//https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting.html
	void Start () {
		Application.ExternalCall("nameFunction");
//		Application.ExternalEval("history.back()");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NamerS(string s){
		print(s);

		txt.text = s;
		curUrl = s;
	}



}
