using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element  {

	protected Object element;

	public void set(Object theElement){
		element = theElement;
	}

	public Object get(){
		return element;
	}

}
