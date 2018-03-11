using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLightsManager : MonoBehaviour {

	public Light luzRoja,luzVerde,flashlight;
	private bool flashActivado;

	public void encenderLuzVerde(){
		luzVerde.intensity = 8;
	}

	public void apagarLuzVerde(){
		luzVerde.intensity = 0;
	}

	public void encenderLuzRoja(){
		luzRoja.intensity = 8;
	}

	public void apagarLuzRoja(){
		luzRoja.intensity = 0;
	}

	public void encenderFlash(){
		flashlight.intensity = 1;
	}

	public void apagarFlash(){
		flashlight.intensity = 0;
	}

	public void toogleFlash(CameraBotoncitosManager cbm){
		flashActivado = !flashActivado;
		if (flashActivado) {
			cbm.presionarBotonFlash ();
			encenderFlash ();
		} else {
			cbm.restaurarBotonFlash ();
			apagarFlash ();
		}
	}


}
