using UnityEngine;
using System.Collections;

public class SnakeMovement :  IMovement{
	Vector3 direction;
	Vector3 position;

	public void Init(Vector3 position, Vector3 direction){
		
		this.direction = direction;
		this.position = position;

	}

	// Update is called once per frame
	public Vector3 UpdatePosition () {
		// ???
		return new Vector3();
	}
}
