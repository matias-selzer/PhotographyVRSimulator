using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMode {

	private ViewModeGUI myGUI;
	private int posActual = 0;
	private Photo photoActual;
	private PhotoRepository repo;

	public ViewMode( ViewModeGUI vmg){
		myGUI = vmg;
		photoActual = new Photo ();
		photoActual.set (Texture2D.blackTexture);
		//repo = new PhotoRepository (32);
		repo =(PhotoRepository) PhotoRepository.theRepository;
	}

	public void moveLeft(){
		if (posActual == 0)
			posActual = repo.getCant()-1;
		else 
			posActual = (posActual - 1) % repo.getCant();

		photoActual = repo.get (posActual);
		updatePhoto ();
		updateGUI ();
	}

	public void moveRight(){
		posActual = (posActual + 1) % repo.getCant();
		photoActual = repo.get (posActual);
		updatePhoto ();
		updateGUI ();
	}

	public void updatePhoto(){
		photoActual = repo.get (posActual);
		myGUI.updatePhoto (photoActual);
	}

	public void updateGUI(){
		myGUI.updateGUI (posActual+1, repo.getCapacity ());
	}

	public void delete(ViewModeGUI vmg){
		Debug.Log ("pos actual " + posActual);
		repo.delete (posActual);
		if (posActual!=0 && posActual == repo.getCant ())
			posActual--;

		if (repo.isEmpty ())
			vmg.changeModeEvenet ();
		else {
			updatePhoto ();
			updateGUI ();
		}
	}

	public bool noPhotos(){
		return repo.isEmpty ();
	}

	public void shareActualPhoto(SinglePhotoShareController ssc){
		ssc.share (photoActual);
	}

	public void shareAllPhotos(MultiplePhotoShareController msc){
		msc.ButtonShareAll (repo.getFullRepository ());
	}
}
