using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SinglePhotoShareController : PhotosShareController {

	public void share(Photo photo)
	{
		if(!isProcessing){
			StartCoroutine( ShareOnePicture(photo.get()) );
		}
	}


	public IEnumerator ShareOnePicture(Texture2D img)
	{
		isProcessing = true;
		yield return new WaitForEndOfFrame();

		byte[] dataToSave = img.EncodeToJPG();
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".jpg");
		File.WriteAllBytes(destination, dataToSave);
		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND_MULTIPLE"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);

			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "This is the photo I've taken.");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Matias Selzer Photos Simulator");

			intentObject.Call<AndroidJavaObject>("setType", "*/*");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);
		}
		isProcessing = false;
	}

}
