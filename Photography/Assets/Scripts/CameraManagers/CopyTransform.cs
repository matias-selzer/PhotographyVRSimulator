using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour {

	public Transform other;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Slerp(transform.position, other.position,10*Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, other.rotation,10*Time.deltaTime) ;
	}
}
