using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

	private CameraManagerDos cameraManager;

	public GameObject viewModeScreenComponent;
	public RawImage viewModeScreeImage;
	public Text picturesTaken,pictureNumber;

	void Start () {
	}
	void Update () {
	}

	public ViewModeDisplay createViewModeDisplay(){
		return new ViewModeDisplay ();
	}

	public void updatePicturesTaken(int picTaken){
		picturesTaken.text = picTaken + "";
	}

	public void updatePictureNumber(int picNumber){
		pictureNumber.text = picNumber + "";
	}





}
