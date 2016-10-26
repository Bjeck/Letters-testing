using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class StoryManager : MonoBehaviour {

	public Dictionary<string,List<UIOBject>> stories = new Dictionary<string, List<UIOBject>>();
	public AuthorUIManager authorMan;
	[SerializeField] StoryParser sp;
	[SerializeField] InputField newStoryipf;
	[SerializeField] Transform loadList;
	[SerializeField] GameObject storyButtonPrefab;
	[SerializeField] Canvas menuCanvas;



	// Use this for initialization
	void Start () {
		LoadStoriesIntoDictionary ();
	}

	public void AccessMenu(){
		menuCanvas.gameObject.SetActive (true);
		LoadStoriesIntoDictionary ();
	}

	public void InputNewStory(){
		CreateNewStory (newStoryipf.text);
	}

	private void CreateNewStory(string nam){
		stories.Add (nam, new List<UIOBject> ());
		authorMan.StartStoryEditing (nam);
		ExitMenu ();
	}

	/// <summary>
	/// Loads the story. For Debugging. DOESN'T WORK ANYMORE.
	/// </summary>
	public void LoadStory(){
		authorMan.StartStoryEditing ("DEBUG");
		sp.LoadStory ("Manager_16-10-03_12-41-10.txt");
		authorMan.FinalStartStoryEditing ();
		ExitMenu ();
	}

	/// <summary>
	///Call to load a story.
	/// </summary>
	/// <param name="s">Name of the story you want to load.</param>
	public void LoadStory(string s){
		authorMan.StartStoryEditing (s);
		sp.LoadStory (s);
		authorMan.FinalStartStoryEditing ();
		ExitMenu ();
	}

	public void LoadStory(string n, string full){
		authorMan.StartStoryEditing (n);
		sp.LoadStoryFullString(n,full);
		authorMan.FinalStartStoryEditing ();
		ExitMenu ();
	}


	//DUMB TEST
	//public void LoadStoryFromURL(string s){
	//	authorMan.StartStoryEditing (s);
		//sp.LoadStoryFile (s);
	//	ExitMenu ();
	//}

	public void ExitMenu(){
		menuCanvas.gameObject.SetActive (false);
	}

	public void SaveStory(string nam, List<UIOBject> list){
		stories [nam] = list;
		print (nam + " saved. Count: " + stories [nam].Count);

		string story = "";

		foreach (UIOBject obj in list) {		//Parsing all the objects into a single string.
			if (obj.GetType ().Equals (typeof(TextObject))) {
				TextObject t = obj as TextObject;

				story += "TEXT." + obj.text.text + "|" + obj.id; 
				story += "|" + t.a.letterString + "|" + t.b.letterString;

				//NEED TO ADD LINKS HERE.

			} 
			else if (obj.GetType ().Equals (typeof(ActionObject))) {
				ActionObject t = obj as ActionObject;

				story += "ACTION." + obj.text.text + "|" + obj.id;
				story += "|" + (int)t.a.acType + "|" + (int)t.b.acType;
				story += "|" + t.a.actionString + "|" + t.b.actionString;


				story += "|";

				for (int i = 0; i < 2; i++) {
					for (int j = 0; j < 2; j++) {
						if(t.links[i,j] != null){
							story += t.links[i,j].id;
						}
						story += "#";
					}
				}
			}

			print(story);

			story += "\n"; //new line to indicate new object

		}

		sp.SaveStoryToFile (nam, story); 

	}


	/// <summary>
	/// Loads the stories present in the local folder into dictionary. atm. should access the database and load those files instead.
	/// </summary>
	public void LoadStoriesIntoDictionary(){
		DirectoryInfo levelDirectoryPath = new DirectoryInfo(Application.dataPath);
		FileInfo[] fileInfo = levelDirectoryPath.GetFiles("*.*", SearchOption.AllDirectories);

		foreach (FileInfo file in fileInfo) { //Finding all files in unity directory with .txt.
			if (file.Extension == ".txt") {
			//	print ("TEXT "+file.Name);

				StreamReader sr = new StreamReader (file.FullName);
				if (sr.ReadLine () == "LETTERS STORY --------------- ") { //sorting so only .txt files with the correct format are taken into account.
					if(!stories.ContainsKey(file.Name)){
						stories.Add (file.Name, new List<UIOBject> ());
						AddStoryButtonToList (file.Name);
					}
				}
				sr.Close ();
			}
		}
	}

	/// <summary>
	/// Adds the story button to the list of stories (for loading).
	/// </summary>
	/// <param name="name">Name of the story.</param>
	public void AddStoryButtonToList(string nam){
		GameObject g = (GameObject)Instantiate (storyButtonPrefab, loadList);
		g.transform.localScale = Vector3.one;
		g.GetComponentInChildren<Text> ().text = nam;
	}


}
