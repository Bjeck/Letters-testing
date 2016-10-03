using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

	Dictionary<string,List<UIOBject>> stories = new Dictionary<string, List<UIOBject>>();
	public AuthorUIManager authorMan;
	[SerializeField] StoryParser sp;
	[SerializeField] InputField newStoryipf;
	[SerializeField] Canvas menuCanvas;

	// Use this for initialization
	void Start () {
	
	}

	public void AccessMenu(){
		menuCanvas.gameObject.SetActive (true);
	}

	public void InputNewStory(){
		CreateNewStory (newStoryipf.text);
	}

	private void CreateNewStory(string name){
		stories.Add (name, new List<UIOBject> ());
		authorMan.StartStoryEditing (name);
		menuCanvas.gameObject.SetActive (false);
	}

	public void LoadStory(){
		//UUUUUUUUHHH!!!!
	}

	public void SaveStory(string name, List<UIOBject> list){
		stories [name] = list;
		print (name + " saved. Count: " + stories [name].Count);

		string story = "";
		foreach (UIOBject obj in list) {		//Parsing all the objects into a single string.
			if (obj.GetType ().Equals (typeof(TextObject))) {
				TextObject t = obj as TextObject;

				story += "TEXT." + t.a.textString + "|" + t.b.textString;
			} else if (obj.GetType ().Equals (typeof(ActionObject))) {
				ActionObject t = obj as ActionObject;

				story += "ACTION." + t.a.acType.ToString () + "|" + t.b.acType;
			}

			story += "\n";
		}

		sp.SaveStoryToFile (name, story); 

	}

}
