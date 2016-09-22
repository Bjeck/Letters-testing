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

	public TraceryReader tr;

	// Use this for initialization
	void Start () {
		txt.text = s;

	}



	public void AcceptInput(){
		mods.Add("#name#",ipf.text);

		//ChangeString (s, mods);

		tr.ReplaceRuleWith("name",mods.Values.ToArray());

		txt.text = tr.RunGrammar();
	}









	//CUSTOM

	public void ChangeString(string stringToModify,  Dictionary<string, string> modifiers){

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
			print (stringToModify+" "+modifiers["#name#"]+" "+changeRange+" "+i);
			stringToModify = stringToModify.Replace (stringToModify.Substring(hashPoss[0],changeRange), modifiers ["#name#"]);
			print (stringToModify+" "+modifiers["#name#"]+" "+changeRange);
		}

		txt.text = stringToModify;
	}


}
