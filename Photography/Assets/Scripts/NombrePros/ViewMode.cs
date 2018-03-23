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

	public void delete(){

	}
}
