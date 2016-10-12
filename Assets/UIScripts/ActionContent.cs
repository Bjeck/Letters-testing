using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionContent : MonoBehaviour {
	
	public ActionType acType;
	public Text actionTypeText;
	public string actionString;

	public void ChangeType(ActionType at){
		acType = at;
		actionTypeText.text = at.ToString();
	}

}
