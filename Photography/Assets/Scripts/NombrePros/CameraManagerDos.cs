using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerDos : MonoBehaviour {

	private PhotoRepository repo;
	private GuiManager gui;
	private ViewModeDisplay viewModeDisplay;

	public int maxCapacity;

	// Use this for initialization
	void Start () {
		repo = new PhotoRepository (maxCapacity);
		gui = GetComponent<GuiManager> ();
		viewModeDisplay = gui.createViewModeDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void takePhoto(){
		if (!repo.isFull ()) {
			//reproducir sonido
			repo.add (PhotoRenderer.renderPhoto ());
			gui.updatePicturesTaken (repo.getCant ());
		}
	}

	public void deletePhoto(){
		repo.delete (viewModeDisplay.getPosPhotoActual ());
	}


}
