using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class textswapper : MonoBehaviour {

	public string s;
	public InputField ipf;
	public Text txt;

	public Dictionary<string, string> mods = new Dictionary<string, string> ();

	// Use this for initialization
	void Start () {
	
	}



	public void AcceptInput(){
		List<string> mods = new List<string> (); mods.Add (ipf.text);

		ChangeString (s, mods);
	}


	public void ChangeString(string stringToModify,  List<string> modifiers){

		List<int> hashPoss = new List<int> ();

		for (int i = 0; i < stringToModify.Length; i++) {
			if (stringToModify [i] == '#') {
				hashPoss.Add (i);
			}
		}
		print (hashPoss.Count);


		for (int i = 0; i < hashPoss.Count; i+=2) {
			int changeRange = 0;
			for (int j = hashPoss[i]; j <= hashPoss[i+1]; j++) {
				changeRange++;
			}
			print (stringToModify+" "+modifiers[0]+" "+changeRange);
			stringToModify = stringToModify.Replace (stringToModify.Substring(hashPoss[0],changeRange), modifiers [0]);
			print (stringToModify+" "+modifiers[0]+" "+changeRange);
		}

		txt.text = stringToModify;
	}


}
