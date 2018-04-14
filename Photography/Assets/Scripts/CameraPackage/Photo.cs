using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : Element {

	public void set(Texture2D theElement){
		element = theElement;
	}

	public Texture2D get(){
		return (Texture2D)element;
	}


}
