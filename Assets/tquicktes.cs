using UnityEngine;
using System.Collections;

public class tquicktes : MonoBehaviour {

	enum test{hello, one, two, three}

	// Use this for initialization
	void Start () {
		test t = test.one;
		print(t+" "+(int)t);
		t = (test)3;
		print(t+" "+(int)t);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
