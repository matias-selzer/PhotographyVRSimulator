using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhotoRenderer {

	public static Photo renderPhoto(){
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

		screenShot.Apply();
		Photo newPhoto = new Photo ();
		newPhoto.set (screenShot);

		return newPhoto;
	}

}
