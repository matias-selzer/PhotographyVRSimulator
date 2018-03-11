using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBotoncitosManager : MonoBehaviour {

	public GameObject btnPlayOff,btnFlashOff;


	public void presionarBotonFlash(){
		btnFlashOff.SetActive (false);
	}

	public void presionarBotonPlay(){
		btnPlayOff.SetActive (false);
	}

	public void restaurarBotonFlash(){
		btnFlashOff.SetActive (true);
	}

	public void restaurarBotonPlay(){
		btnPlayOff.SetActive (true);
	}
}
