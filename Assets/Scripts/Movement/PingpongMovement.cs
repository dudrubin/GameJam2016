using System;
using UnityEngine;


public class PingpongMovement: IMovement
{
	Vector3 direction;
	Vector3 position;

	public void Init(Vector3 position, Vector3 direction){

		this.direction = direction;
		this.position = position;
	}

	// Update is called once per frame
	public Vector3 UpdatePosition () {
		position += direction;
		Vector3 updatedPos = position;
		if (GameBorders.IsOutHorizontal(updatedPos)) {
			direction.x *= -1;
		}
		if (GameBorders.IsOutVertical(updatedPos)) {
			direction.y *= -1;
		}
		return updatedPos;
	}
}


