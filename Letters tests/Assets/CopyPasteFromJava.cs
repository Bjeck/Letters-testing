using UnityEngine;
using System.Collections;

public class CopyPasteFromJava : MonoBehaviour {

	string text = "text that will come from outside";
	bool updated = false;

	// Use this for initialization
	void Start () {
	
	}


	public void GetTextFromJava(){
		updated = false;
		CopyPasteButton ();
		StartCoroutine (HoldForJava ());

	}

	IEnumerator HoldForJava(){
		while(!updated){
			yield return new WaitForEndOfFrame ();
		}
		yield return text;
	}


	public void CopyPasteButton(){
		Application.ExternalCall("CopyPaste");
	}


	public void ReceivePaste(string s){

		text = s;
		updated = true;
	}

}

/*
<script>
function CopyPaste() {
    var link = prompt("Please enter link", "link");
    if (link != null) {
		SendMessage("PlayerSceneManager", "ReceivePaste", link);
    }
}
</script>
*/