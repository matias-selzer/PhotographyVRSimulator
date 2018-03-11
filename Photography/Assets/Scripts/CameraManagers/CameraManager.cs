using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

	private Camera camaraPosta;
	private bool modoTiempoReal=true;

	public Slider slider;

	private CameraLightsManager clm;
	private CameraBotoncitosManager cbm;
	private CameraScreenManager csm;
	private CameraPicturesManager cpm;
	//private CameraRaycastManager crm;


	// Use this for initialization
	void Start () {
		camaraPosta = gameObject.GetComponent<Camera>();

		clm = GetComponent<CameraLightsManager> ();
		cbm = GetComponent<CameraBotoncitosManager> ();
		csm = GetComponent<CameraScreenManager> ();
		//crm = GetComponent<CameraRaycastManager> ();
		cpm = GetComponent<CameraPicturesManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		/*isGrabbed=(GetComponent<VRTK_InteractableObject>().enabled);
		if (crm.estaApuntandoObjetoFotografiable(camaraPosta)) {
			clm.encenderLuzVerde ();
		} else {
			clm.apagarLuzVerde();
		}*/
		if (Input.GetKeyDown (KeyCode.Q)) {
			cambiarModo ();
		}
	}

	public void sacarFoto(){
		if (modoTiempoReal && cpm.hayCapacidad()) {
			GetComponent<AudioSource> ().Play ();
			//string nombre = crm.chequearColision (camaraPosta);

			//cpm.agregarNombre (nombre);
			cpm.guardarImagen (camaraPosta);
			cpm.actualizarCapacidad ();
			if (cpm.memoriaEstaLLena()) {
				clm.encenderLuzRoja ();
			}
		}
	}

	public void toogleFlash(){
		if (modoTiempoReal) {
			clm.toogleFlash (cbm);
		}
	}
		

	public void ajustarZoom(){
		if (modoTiempoReal) {
			float valor = slider.value;
			camaraPosta.fieldOfView = 60 + valor * -1 * 60;
		}
	}

	public void cambiarFoto(float x){
		if (cpm.hayFotos() && !modoTiempoReal) {
			if (x > 0) {
				cpm.cambiarFotoEnModoVistaRight ();
			} else {
				cpm.cambiarFotoEnModoVistaLeft ();
			}
		}
	}



	public void cambiarModo(){
		modoTiempoReal =!modoTiempoReal;
		if (!modoTiempoReal) {
			csm.activarPantallaVista ();
			cpm.actualizarCapacidad ();
			//cbm.presionarBotonPlay ();
		} else {
			//cbm.restaurarBotonPlay ();
			csm.desactivarPantallaVista ();
			cpm.actualizarPictureNumber ();
		}
	}



	public void reiniciar(){
		clm.apagarLuzRoja ();
		cpm.reiniciar ();
	}


}
