using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoRepository: Repository{

	public PhotoRepository(int capacity):base(capacity){}

	public Photo get(int pos){
		return ((Photo)(elements [pos]));
	}

}
