using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModeGUI : MonoBehaviour {

	public GameObject photoModeContainer, viewModeContainer,deleteScreen;
	public Text posAndCapacity;
	public RawImage screen;
	public SinglePhotoShareController singleShareC;
	public MultiplePhotoShareController multiShareC;

	private ViewMode myViewMode;


	// Use this for initialization
	void Awake () {
		myViewMode = new ViewMode (this);
		//changeModeEvenet ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void leftEvent(){
		myViewMode.moveLeft ();
	}

	public void rightEvent(){
		myViewMode.moveRight ();
	}

	public void deleteEvent(){
		myViewMode.delete (this);
		hideDeleteScreen ();
	}

	public void showDeleteScreen(){
		deleteScreen.SetActive (true);
	}

	public void hideDeleteScreen(){
		deleteScreen.SetActive (false);
	}

	public void changeModeEvenet(){
		photoModeContainer.SetActive (true);
		viewModeContainer.SetActive (false);
	}

	public void updatePhoto(Photo p){
		screen.texture = p.get ();
	}

	public void updateGUI(int pos, int capacity){
		posAndCapacity.text = pos + " - " + capacity;
		//Debug.Log (pos);
	}

	void OnEnable(){
		if (myViewMode.noPhotos ())
			changeModeEvenet ();
		else {
			myViewMode.updatePhoto ();
		}
		myViewMode.updateGUI ();
	}

	public void shareActualPhotoEvent(){
		myViewMode.shareActualPhoto (singleShareC);
	}

	public void shareAllPhotosEvent(){
		myViewMode.shareAllPhotos (multiShareC);
	}


}
