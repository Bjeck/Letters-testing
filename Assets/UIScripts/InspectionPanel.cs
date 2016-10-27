using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour {

	[SerializeField] InspectionPanelActionHelper ipah;
	[SerializeField] public AuthorUIManager authorMan;
	public Text text;
	public Dropdown drop;
	public InputField nameIpf;
	public Button nameButton;
	public ToggleButton tglA;
	public ToggleButton tglB;
	public ToggleButton tglC;
	public Button editButton;
	public InputField textInputField;
	public Transform actionParent;
	public List<GameObject> actionTypes = new List<GameObject>();
	public Button actionSaveButton;
	public InputField textLinkInput;
	public GameObject linkSetupAction;
	public GameObject linkSetupText;


	public bool a, b, c;
	public bool inspectingAction = false;

	public InputField webpageerrorIPF;
	public InputField QuestionniareQIPF;
	public InputField QuestionniareA1IPF;
	public InputField QuestionniareA2IPF;

	// Use this for initialization
	void Start () {
	//	a = true;
	}

	public void InspectObject(UIOBject obj){
		if (obj == null) {
			Debug.LogError ("You're trying to inspect an object without having actually clicked on one. That won't work!");
			return;
		}

		drop.gameObject.SetActive(false);
		nameButton.gameObject.SetActive (true);
		tglA.gameObject.SetActive(true);
		tglB.gameObject.SetActive(true);
		tglC.gameObject.SetActive(true);
		actionSaveButton.gameObject.SetActive (false);
		textLinkInput.gameObject.SetActive (false);
		linkSetupAction.SetActive (false);
		linkSetupText.SetActive (false);
		editButton.gameObject.SetActive(false);
		textInputField.gameObject.SetActive(false);

		foreach (Transform t in actionParent) {
			t.gameObject.SetActive (false);
		}

		if(!inspectingAction){
			InspectObject(obj as TextObject);
		}
		else{
			InspectObject(obj as ActionObject);
		}

	}

	public void InspectObject(TextObject obj){

		if (IsInspectingLinks ()) {
			text.text = "Text. Choose link here.";
			linkSetupText.SetActive (true);
			ipah.LinkFillIn(obj);
		} else {
			text.text = a ? obj.a.letterString : obj.b.letterString;
			textLinkInput.gameObject.SetActive (true);
			editButton.gameObject.SetActive(true);
		}
	}

	public void InspectObject(ActionObject obj){
		print("inspecting "+obj.text.text);

		actionSaveButton.gameObject.SetActive (true);
		//CHECK IF C, IF NOT GO AHEAD WITH THAT
		if (IsInspectingLinks ()) {
			text.text = "Action. Choose links here.";
			linkSetupAction.SetActive (true);
			ipah.LinkFillIn(obj);
		} 
		else {
			
			text.text = "Action. Choose action in the dropdown menu";
			drop.gameObject.SetActive (true);
			int acval = a ? (int)obj.a.acType : (int)obj.b.acType;
			drop.value = (acval);
			actionParent.GetChild (acval).gameObject.SetActive (true);
			ipah.ActionFillIn((ActionType)acval,obj);
			print ("INSPECTING ACTIOn "+c);
		}
	}

	public void HandleDropdownChange(){
		authorMan.ChangeActionObjectType((ActionType)drop.value);
		InspectObject (authorMan.objectBeingInspected);
	}

	public void HandleTextLink(){
		StartCoroutine (StartURLRead ());
	}

	IEnumerator StartURLRead() {
		WWW www = new WWW(textLinkInput.text);
		yield return www;
		print(www.text);

		TextObject t = authorMan.objectBeingInspected as TextObject;

		if (a) {
			t.a.letterString = www.text;
		} else {
			t.b.letterString = www.text;
		}

		text.text = a ? t.a.letterString : t.b.letterString;
	}



	public void BeginNaming(){
		nameIpf.gameObject.SetActive (true);
	}

	public void EndNaming(){
		authorMan.objectBeingInspected.text.text = nameIpf.text;
		nameIpf.text = "";
		nameIpf.gameObject.SetActive (false);
	}

	public void EditText(){
		if(!textInputField.gameObject.activeInHierarchy){
			textInputField.gameObject.gameObject.SetActive(true);
			textInputField.text = text.text;
		}
		else {
			EndEditText();
		}
	}

	public void EndEditText(){
		textInputField.gameObject.gameObject.SetActive(false);
		text.text = textInputField.text;
		TextObject t = authorMan.objectBeingInspected as TextObject;
		if(a){
			t.a.letterString = text.text;
		}
		else { 
			t.b.letterString = text.text;
		}
	}

	/// <summary>
	/// Toggles a or b for inspection in the UIObject. 
	/// </summary>
	/// <param name="a">If set to <c>true</c> a will be true and b will be false. If set to false, b will be true.</param>
	public void ToggleAB(ToggleButton tgl){

		print("TOGGLING "+inspectingAction+" "+authorMan.objectBeingInspected);
		SaveObjectInfo();

		if (tgl == tglA) {
			a = true;
			b = false;
			c = false;
			tglB.isOn = false;
			tglB.CheckColor ();

			tglC.isOn = false;
			tglC.CheckColor ();
			authorMan.ExitShadowMode ();
		} else if (tgl == tglB) {
			a = false;
			b = true;
			c = false;
			tglA.isOn = false;
			tglA.CheckColor ();

			tglC.isOn = false;
			tglC.CheckColor ();
			authorMan.ExitShadowMode ();
		} else if (tgl == tglC) {
			a = false;
			b = false;
			c = true;
			tglA.isOn = false;
			tglB.isOn = false;
			tglA.CheckColor ();
			tglB.CheckColor ();
			authorMan.ShadowMode ();

		}
		InspectObject(authorMan.objectBeingInspected); //calls inspect to show the new side of the current UIObject.
	}


	public void SaveObjectInfo(){
		if(inspectingAction){
			ipah.SaveAction (); //saves the current action, as an autosave. Makes sure we don't lose anything when switching sides.
		}
		else {
			ipah.SaveText ();
		}
	}

	public void ClearInspectionPanel(){
		text.text = "";
		drop.gameObject.SetActive(false);
		nameButton.gameObject.SetActive (false);
		tglA.gameObject.SetActive(false);
		tglB.gameObject.SetActive(false);

	}


	/// <summary>
	/// Returns if Inspection Panel is on sides or C (Links). True is c
	/// </summary>
	/// <returns>Returns <c>true</c>, if A, <c>false</c> if B.</returns>
	public bool IsInspectingLinks(){
		if(c) return true; else return false;
	}

}
