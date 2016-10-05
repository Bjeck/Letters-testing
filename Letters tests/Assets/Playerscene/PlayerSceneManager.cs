using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;

public class PlayerSceneManager : MonoBehaviour {

	string urlinfo;

	[SerializeField] Text t;
	[SerializeField] InputField ipf;
	public string url;

	// Use this for initialization
	void Start () {
		//GetSceneFunction ("");
		StartURLReadManual();
	}


	public void StartURLReadFromIPF(){
		url = ipf.text;
		StartCoroutine(StartURLRead());
	}


	public void StartURLReadManual(){
		url = "https://dl.dropboxusercontent.com/u/2716073/temp_16-10-04_05-22-07.txt";
		StartCoroutine(StartURLRead());
	}

	IEnumerator StartURLRead() {
		WWW www = new WWW(url);
		yield return www;
		print(www.text);
		t.text = www.text;
		GetSceneFunction (www.text);
	}




	public void StartDetection(){
		Application.ExternalCall("urlFunction"); //first, call javascript
	}

/* ADD the following on the index.html

<script>
function urlFunction(  )
  {
    SendMessage("PlayerSceneManager", "GetSceneFunction", window.location.href);
  }
</script>
*/

	public void GetSceneFunction(string s){ // Javascript then calls this function.
		print(s);

		urlinfo = s;


		//get scenefunction from that

		//load story, player and story part from URL
		//URL: STORY _ a/b _ int signifying line. ? Maybe should be encrypted somehow or something, so it's not just "i'm gonna punch in 5 in the int!"

		List<string> lines = new List<string>(s.Split(new string[] { "\r","\n" },System.StringSplitOptions.RemoveEmptyEntries) );

		string[] sides;

		try{
			foreach(string line in lines)
			{
				if(!String.IsNullOrEmpty(line)){
					if(line.Contains(" --------------- ")){
						continue;
					}

					//print (line);
					string l = line;

					if(l.Substring(0,7) == "ACTION."){

						//ACTION
						l = l.Remove(0,7);
						print (l);
						sides = l.Split('|');

						//CHECK to see if that story is legal yet. NOT IMPLEMENTED
						print("executing");
						ExecuteSceneFunction (int.Parse(sides[0]),sides[2]); //side a
					}
				}
			}
		}
		catch(Exception e){
			print ("LOAD STORY FAILED " + e.Message);
		}



	}



	void ExecuteSceneFunction(int id, string s){

		print ("SCENE FUNCTION "+id + " " + s);

		if (id == 1) {
			ExecuteWebpageError (s);
		}
	}



	public void ExecuteWebpageError(string s){
		print("ERROR TIME");
		Application.ExternalCall("WebpageError",s);

	}
/* ADD the following on the index.html

<script>
function WebpageError( s )
  {
    alert(s);
  }
</script>


*/


}

// TEST IF WebGL can read urls with www. then rebuild a reader that can read that raw string. should be easy.