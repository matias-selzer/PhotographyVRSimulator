using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPicturesManager : MonoBehaviour {

	private int pos = 0;
	private ArrayList listaImagenes;
	//private ArrayList objetosFotografiados;
	private int capacity=32;


	private CameraScreenManager csm;

	// Use this for initialization
	void Start () {
		listaImagenes = new ArrayList ();

		csm = GetComponent<CameraScreenManager> ();

		actualizarCapacidad ();
		actualizarPictureNumber ();
	}
		

	public bool memoriaEstaLLena(){
		return listaImagenes.Count == capacity;
	}

	public void guardarImagen(Camera camaraPosta){
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = camaraPosta.targetTexture;
		camaraPosta.Render ();
		Texture2D image = new Texture2D (camaraPosta.targetTexture.width, camaraPosta.targetTexture.height);
		image.ReadPixels (new Rect (0, 0, camaraPosta.targetTexture.width, camaraPosta.targetTexture.height), 0, 0);
		image.Apply ();
		RenderTexture.active = currentRT;
		listaImagenes.Add (image);
		csm.actualizarImagenVista (image);
	}



	public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}

	public void actualizarCapacidad(){
		string salida = "";
		if (listaImagenes.Count < 10)
			salida = "0";
		csm.actualizarCapacityLabel(salida+listaImagenes.Count + " - " + capacity);
	}

	public 	void actualizarPictureNumber(){
		string salida = "";
		if ((pos + 1) < 10)
			salida = "0";
		salida = salida + (pos + 1) + " - ";
		if (listaImagenes.Count < 10)
			salida += "0";
		salida += listaImagenes.Count;
		csm.actualizarPictureNumber( salida );
	}

	public bool hayCapacidad(){
		return listaImagenes.Count < capacity;
	}

	public void cambiarFotoEnModoVistaRight(){
		pos = (pos + 1) % listaImagenes.Count;
		csm.actualizarImagenVista( (Texture2D)(listaImagenes [pos]));
		actualizarPictureNumber();
	}

	public void cambiarFotoEnModoVistaLeft(){
		if (pos == 0)
			pos = listaImagenes.Count - 1;
		else 
			pos = (pos - 1) % listaImagenes.Count;
		csm.actualizarImagenVista( (Texture2D)(listaImagenes [pos]));
		actualizarPictureNumber();
	}

	public bool hayFotos(){
		return listaImagenes.Count > 0;
	}



	public void reiniciar(){
		pos = 0;
		listaImagenes = new ArrayList ();
		actualizarCapacidad ();
		actualizarPictureNumber ();
		csm.actualizarImagenVista (null);
	}

}
