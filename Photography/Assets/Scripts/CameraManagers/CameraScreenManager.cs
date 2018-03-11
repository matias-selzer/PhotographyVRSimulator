using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScreenManager : MonoBehaviour {

	public GameObject pantallaVista;
	public RawImage imagenVista;
	public Text capacityLabel,pictureNumber;

	public void actualizarImagenVista(Texture2D t){
		imagenVista.texture = t;
	}
		
	public void activarPantallaVista(){
		pantallaVista.SetActive (true);
	}

	public void desactivarPantallaVista(){
		pantallaVista.SetActive (false);
	}

	public void actualizarPictureNumber(string s){
		pictureNumber.text = s;
	}

	public void actualizarCapacityLabel(string s){
		capacityLabel.text = s;
	}
}
