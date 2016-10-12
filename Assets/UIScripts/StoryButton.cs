using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryButton : MonoBehaviour {

	[SerializeField] StoryManager sm;

	public void LoadStory(Button b){

		if(sm == null)
			sm = GameObject.Find ("Manager").GetComponent<StoryManager> ();

		sm.LoadStory (b.GetComponentInChildren<Text> ().text);
		
	}

}
