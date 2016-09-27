using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour {

	[SerializeField] Text text;
	[SerializeField] Dropdown drop;
	[SerializeField] InputField nameIpf;
	[SerializeField] Button nameButton;
	[SerializeField] AuthorUIManager authorMan;
	[SerializeField] ToggleButton tglA;
	[SerializeField] ToggleButton tglB;

	public bool a, b;

	// Use this for initialization
	void Start () {
		authorMan = GameObject.Find("Canvas").GetComponent<AuthorUIManager>();
		a = true;
	}

	public void InspectObject(UIOBject obj){
		drop.gameObject.SetActive(false);

		nameButton.gameObject.SetActive (true);
		tglA.gameObject.SetActive(true);
		tglB.gameObject.SetActive(true);


		if(obj.GetType().Name == "TextObject"){
			InspectObject(obj as TextObject);
		}
		else if(obj.GetType().Name == "ActionObject"){
			InspectObject(obj as ActionObject);
		}
	}

	public void InspectObject(TextObject obj){
		text.text = a ? obj.a.textString : obj.b.textString;
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
		tglA.gameObject.SetActive(false);
		tglB.gameObject.SetActive(false);

	}

	public void BeginNaming(){
		nameIpf.gameObject.SetActive (true);
	}

	public void EndNaming(){
		authorMan.objectBeingInspected.text.text = nameIpf.text;
		nameIpf.text = "";
		nameIpf.gameObject.SetActive (false);
	}

	/// <summary>
	/// Toggles a or b for inspection in the UIObject. 
	/// </summary>
	/// <param name="a">If set to <c>true</c> a will be true and b will be false. If set to false, b will be true.</param>
	public void ToggleAB(ToggleButton tgl){

		string aa = tgl.gameObject.name;

		if(aa == "A Toggle"){
			a = true;
			b = false;
			tglB.isOn = false;
			tglB.CheckColor();
		}
		else if(aa == "B Toggle"){
			a = false;
			b = true;
			tglA.isOn = false;
			tglA.CheckColor();
		}

		print(aa+"     "+a+" "+b);


		InspectObject(authorMan.objectBeingInspected);
		//tglA.CheckColor();

	}
}
