using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPicturesManager : MonoBehaviour {

	private int pos = 0;
	private ArrayList listaImagenes;
	//private ArrayList objetosFotografiados;
	private int capacity=32;
	private bool mustSave;

	Texture2D ultima;

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
		RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Default);        
		Texture2D screenShot = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

		//no es necesario todas las camaras, puede ser camaraPosta y listo
		foreach(Camera cam in Camera.allCameras)
		{
			cam.targetTexture = rt;
			cam.Render();
			cam.targetTexture = null;
		}

		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0, false);
		Camera.main.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);
		screenShot.Apply();

		listaImagenes.Add (screenShot);
		csm.actualizarImagenVista (screenShot);
	}

	public void eliminarFoto(){
		listaImagenes.RemoveAt (pos);
		if (hayFotos ()) {
			pos = pos % listaImagenes.Count;
			csm.actualizarImagenVista ((Texture2D)listaImagenes [pos]);
		} else
			transform.GetComponentInParent<CameraManager> ().cambiarModo ();
		actualizarPictureNumber ();
	}



	/*public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.jpg", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}*/

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
		Photo p = new Photo ();
		p.set (Texture2D.blackTexture);
		PhotoRepository pr = new PhotoRepository (3);
		pr.add (p);

		Texture2D t = pr.get (0).get ();

		return listaImagenes.Count > 0;
	}



	public void reiniciar(){
		pos = 0;
		listaImagenes = new ArrayList ();
		actualizarCapacidad ();
		actualizarPictureNumber ();
		csm.actualizarImagenVista (null);
	}

	public Texture2D imagenActual(){
		return ((Texture2D)(listaImagenes [pos]));
	}

	public ArrayList obtenerImagenes(){
		return listaImagenes;
	}





}
