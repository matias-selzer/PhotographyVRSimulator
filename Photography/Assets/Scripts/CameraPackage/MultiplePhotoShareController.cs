using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MultiplePhotoShareController : PhotosShareController {

	public void ButtonShareAll(ArrayList photoList){
		if(!isProcessing){
			StartCoroutine( ShareAllPictures(photoList) );
		}
	}


	public IEnumerator ShareAllPictures(ArrayList photoList)
	{
		isProcessing = true;
		yield return new WaitForEndOfFrame();

		if(!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND_MULTIPLE"));
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
			AndroidJavaObject objArrayList = new AndroidJavaObject("java.util.ArrayList");
			for (int i = 0; i < photoList.Count; i++) {
				byte[] dataToSave = ((Photo)photoList [i]).get().EncodeToJPG ();
				string destination = Path.Combine (Application.persistentDataPath, System.DateTime.Now.ToString ("yyyy-MM-dd-HHmmss"+"-"+(i+1)) + ".jpg");
				File.WriteAllBytes (destination, dataToSave);
				AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse","file://" + destination );
				objArrayList.Call<bool>("add", uriObject);
			}

			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), objArrayList);

			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "These are the photos I've taken.");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Matias Selzer Photos Simulator");

			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);

		}
		isProcessing = false;

	}
}
