using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModeDisplay : CameraDisplay {

	private int posActual = 0;
	private Photo photoActual;

	public ViewModeDisplay(){
		photoActual = new Photo ();
		photoActual.set (Texture2D.blackTexture);
	}

	public override void update(){

	}

	public int getPosPhotoActual(){
		return posActual;
	}


}
