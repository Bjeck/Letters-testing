using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour {

	public Text text;
	public Dropdown drop;
	public AuthorUIManager authorMan;

	// Use this for initialization
	void Start () {
		authorMan = GameObject.Find("Canvas").GetComponent<AuthorUIManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewObject(UIOBject obj){
		drop.gameObject.SetActive(false);
		if(obj.GetType().Name == "TextObject"){
			NewObject(obj as TextObject);
		}
		else if(obj.GetType().Name == "ActionObject"){
			NewObject(obj as ActionObject);
		}

	}

	public void NewObject(TextObject obj){
		text.text = obj.textString;
	}

	public void NewObject(ActionObject obj){
		print("ActionObject");
		text.text = "Action. Choose action in the dropdown menu";
		drop.gameObject.SetActive(true);
	}


	public void HandleDropdownChange(){
		string s = drop.options[drop.value].text;
		if(s=="Phonecall"){
			authorMan.ChangeActionObjectType(ActionType.Phonecall);
		}
		else if(s=="Webpage Error"){
			authorMan.ChangeActionObjectType(ActionType.webPageError);
		}
		else if(s=="Word Substitution"){
			authorMan.ChangeActionObjectType(ActionType.wordSubstitution);
		}
	}

	public void ClearInspectionPanel(){
		text.text = "";
		drop.gameObject.SetActive(false);


	}



}
