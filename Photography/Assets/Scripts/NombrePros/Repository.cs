using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Repository {

	protected ArrayList elements;
	protected int capacity;
	protected int cant;
	public static Repository theRepository;

	public Repository(int capacity){
		if (theRepository == null) {
			theRepository = this;
			elements = new ArrayList ();
			this.capacity = capacity;
			cant = 0;
		}
	}

	public bool isFull(){
		return cant == capacity;
	}

	public bool isEmpty(){
		return cant == 0;
	}

	public void add(Element newElement){
		elements.Add (newElement);
		cant++;
	}

	public void delete(int pos){
		elements.RemoveAt (pos);
		cant--;
	}

	public void delete(Element elementToDelete){
		elements.Remove (elementToDelete);
		cant--;
	}

	public Element get(int pos){
		return ((Element)(elements [pos]));
	}

	public ArrayList getFullRepository(){
		return elements;
	}

	public int getCant(){
		return cant;
	}

	public int getCapacity(){
		return capacity;
	}

}
