using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ShareImageCanvas : MonoBehaviour {

	private bool isProcessing = false;

	//function called from a button
	public void ButtonShareOne ()
	{
		Texture2D imagenActual =transform.GetComponentInChildren<CameraPicturesManager> ().imagenActual ();
		if(!isProcessing){
			StartCoroutine( ShareOnePicture(imagenActual) );
		}
	}

	public void ButtonShareAll(){
		ArrayList imagenes = transform.GetComponentInChildren<CameraPicturesManager> ().obtenerImagenes ();
		if(!isProcessing){
			StartCoroutine( ShareAllPictures(imagenes) );
		}
	}

	public IEnumerator ShareOnePicture(Texture2D img)
	{
		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();

		byte[] dataToSave = img.EncodeToPNG();
		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
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
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "hola2");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

			intentObject.Call<AndroidJavaObject>("setType", "*/*");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);
		}
		isProcessing = false;
	}

	public IEnumerator ShareAllPictures(ArrayList imagenes)
	{
		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();


		if(!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
			intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND_MULTIPLE"));
			AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
			AndroidJavaObject objArrayList = new AndroidJavaObject("java.util.ArrayList");
			for (int i = 0; i < imagenes.Count; i++) {
				byte[] dataToSave = ((Texture2D)imagenes [i]).EncodeToPNG ();
				string destination = Path.Combine (Application.persistentDataPath, System.DateTime.Now.ToString ("yyyy-MM-dd-HHmmss"+"-"+(i+1)) + ".png");
				File.WriteAllBytes (destination, dataToSave);
				AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse","file://" + destination );
				objArrayList.Call<bool>("add", uriObject);
			}

			intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), objArrayList);

			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

			currentActivity.Call("startActivity", intentObject);
		}
		isProcessing = false;

	}
}