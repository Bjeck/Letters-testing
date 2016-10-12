﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour {

	[SerializeField] InspectionPanelActionHelper ipah;
	[SerializeField] public Text text;
	[SerializeField] public Dropdown drop;
	[SerializeField] public InputField nameIpf;
	[SerializeField] public Button nameButton;
	[SerializeField] public Transform actionParent;
	[SerializeField] public List<GameObject> actionTypes = new List<GameObject>();
	[SerializeField] public Button actionSaveButton;
	public InputField textLinkInput;
	[SerializeField] public AuthorUIManager authorMan;
	[SerializeField] public InputField webpageerrorIPF;
	public ToggleButton tglA;
	public ToggleButton tglB;

	public bool a, b;

	// Use this for initialization
	void Start () {
		a = true;
	}

	public void InspectObject(UIOBject obj){
		drop.gameObject.SetActive(false);
		nameButton.gameObject.SetActive (true);
		tglA.gameObject.SetActive(true);
		tglB.gameObject.SetActive(true);
		actionSaveButton.gameObject.SetActive (false);
		textLinkInput.gameObject.SetActive (false);
		foreach (Transform t in actionParent) {
			t.gameObject.SetActive (false);
		}

		if(obj.GetType().Name == "TextObject"){
			InspectObject(obj as TextObject);
		}
		else if(obj.GetType().Name == "ActionObject"){
			InspectObject(obj as ActionObject);
		}
	}

	public void InspectObject(TextObject obj){
		textLinkInput.gameObject.SetActive (true);

		text.text = a ? obj.a.textString : obj.b.textString;
	}

	public void InspectObject(ActionObject obj){
		text.text = "Action. Choose action in the dropdown menu";
		drop.gameObject.SetActive (true);
		actionSaveButton.gameObject.SetActive (true);
		int acval = a ? (int)obj.a.acType : (int)obj.b.acType;
		drop.value = (acval);
		actionParent.GetChild (acval).gameObject.SetActive (true);
		ipah.ActionFillIn((ActionType)acval,obj);
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
			t.a.textString = www.text;
		} else {
			t.b.textString = www.text;
		}

		text.text = a ? t.a.textString : t.b.textString;
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
		InspectObject(authorMan.objectBeingInspected); //calls inspect to show the new side of the current UIObject.
	}


	public void ClearInspectionPanel(){
		text.text = "";
		drop.gameObject.SetActive(false);
		nameButton.gameObject.SetActive (false);
		tglA.gameObject.SetActive(false);
		tglB.gameObject.SetActive(false);

	}


	/// <summary>
	/// Returns what side (A or B) the current inspected object is currently on.
	/// </summary>
	/// <returns>Returns <c>true</c>, if A, <c>false</c> if B.</returns>
	public bool UIObjectSide(){
		if(a) return true; else return false;
	}

}