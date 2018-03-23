using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoModeGUI : MonoBehaviour {

	public Text photosLeft;
	public GameObject photoModeContainer, viewModeContainer;

	private PhotoMode myPhotoMode;

	// Use this for initialization
	void Start () {
		myPhotoMode = new PhotoMode (this);
	}
	
	public void takePhotoEvent(){
		myPhotoMode.takePhoto ();
	}

	public void changeModeEvent(){
		photoModeContainer.SetActive (false);
		viewModeContainer.SetActive (true);
	}

	public void updateGUI(int cant, int capacity){
		string s = cant + " - " + capacity;
		photosLeft.text = s;
	}

}
