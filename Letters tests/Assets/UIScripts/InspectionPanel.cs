using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour {

	[SerializeField] Text text;
	[SerializeField] Dropdown drop;
	[SerializeField] InputField nameIpf;
	[SerializeField] Button nameButton;
	[SerializeField] AuthorUIManager authorMan;

	// Use this for initialization
	void Start () {
		authorMan = GameObject.Find("Canvas").GetComponent<AuthorUIManager>();
	}

	public void InspectObject(UIOBject obj){
		drop.gameObject.SetActive(false);
		nameButton.gameObject.SetActive (true);

		if(obj.GetType().Name == "TextObject"){
			InspectObject(obj as TextObject);
		}
		else if(obj.GetType().Name == "ActionObject"){
			InspectObject(obj as ActionObject);
		}
	}

	public void InspectObject(TextObject obj){
		text.text = obj.textString;
	}

	public void InspectObject(ActionObject obj){
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
		nameButton.gameObject.SetActive (false);
	}

	public void BeginNaming(){
		nameIpf.gameObject.SetActive (true);
	}

	public void EndNaming(){
		authorMan.objectBeingInspected.text.text = nameIpf.text;
		nameIpf.text = "";
		nameIpf.gameObject.SetActive (false);

	}
}
