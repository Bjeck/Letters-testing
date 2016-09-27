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
		drop.gameObject.SetActive (true);
		print (obj.a.acType + " " + obj.b.acType);
		drop.value = DropdownValueStringTranslation (a ? obj.a.acType : obj.b.acType);
	}

	public void HandleDropdownChange(){
		string s = drop.options[drop.value].text;
		if(s=="Phonecall"){
			authorMan.ChangeActionObjectType(ActionType.Phonecall);
		}
		else if(s=="WebPageError"){
			authorMan.ChangeActionObjectType(ActionType.WebPageError);
		}
		else if(s=="WordSubstitution"){
			authorMan.ChangeActionObjectType(ActionType.WordSubstitution);
		}
	}

	private int DropdownValueStringTranslation(ActionType at){


		print(drop.options [(int)at].text); // THIS IS THE CLUE. I CAN FIGURE THIS OUT. I KNOW I CAN.



		for (int i = 0; i < drop.options.Count; i++) {
			if (at.ToString () == drop.options [i].text) {
				print ("FOUND MATCH " + i + " " + at.ToString () + " " + drop.options [i].text);
				return i;
			}
		}



		Debug.LogError ("REACHED bottom of dropdown list without finding any matches. Check your strings?");
		return 0;
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
		InspectObject(authorMan.objectBeingInspected); //calls inspect to show the new side of the current UIObject.
	}

	/// <summary>
	/// Returns what side (A or B) the current inspected object is currently on.
	/// </summary>
	/// <returns>Returns <c>true</c>, if A, <c>false</c> if B.</returns>
	public bool UIOBjectSide(){
		return a ? a : b;
	}

}
