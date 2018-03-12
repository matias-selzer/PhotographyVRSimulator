using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePictureManager : MonoBehaviour {

	public GameObject boton,panel;

	public void activarPanel(){
		boton.SetActive (false);
		panel.SetActive (true);
	}

	public void desactivarPanel(){
		boton.SetActive (true);
		panel.SetActive (false);
	}

	public void deleteCurrentPicture(){
		transform.GetComponentInChildren<CameraManager> ().eliminarFoto ();
		desactivarPanel ();
	}
}
