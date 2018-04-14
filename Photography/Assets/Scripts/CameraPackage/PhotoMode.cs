using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoMode {

	private PhotoModeGUI myGUI;
	private PhotoRepository repo;

	public PhotoMode(PhotoModeGUI pmg){
		myGUI = pmg;
		repo = new PhotoRepository (32);
		repo =(PhotoRepository) PhotoRepository.theRepository;
		updateGUI ();
	}

	public void takePhoto(){
		Debug.Log (repo.getCant ());
		if (!repo.isFull ()) {
			//reproducir sonido
			repo.add (PhotoRenderer.renderPhoto ());
			updateGUI ();
		}
	}

	public void updateGUI(){
		myGUI.updateGUI (repo.getCant (), repo.getCapacity ());
	}

	public void updateZoom(float value, Camera cam){
		cam.fieldOfView = 60 + value * -1 * 60;
	}




}
