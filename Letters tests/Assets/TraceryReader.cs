using UnityEngine;
using System.Collections;
using Tracery;

public class TraceryReader : MonoBehaviour {

	Grammar grammar;
	// Use this for initialization
	void Start () {
	
		TextAsset jsonFile = Resources.Load("grammar") as TextAsset; // assuming the file is at Assets/Resources/grammar.json
		grammar = Grammar.LoadFromJSON(jsonFile);


		print(grammar.Flatten("#origin#"));
	//	grammar.SelectRule("mood") = new string[]{"MOOD"};
	//	print(grammar.SelectRule("mood").Raw);
	//	grammar.PushRules("origin", new string[]{"Hello, #name#!"});
		grammar.PushRules("name", new string[]{"MOOD", "WORLD"});
		print(grammar.Flatten("#origin#"));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			RunGrammar();
		}
	}


	public void ReplaceRuleWith(string rule, string[] strings){
		grammar.PushRules(rule, strings);
	}



	public string RunGrammar(){

		return grammar.Flatten("#origin#") + "  "  + grammar.Flatten("#secondorigin#");

	}

}
