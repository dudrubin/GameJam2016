using System;
using UnityEngine;


public class BaseCannonMovement 
{
	Transform[] barrelTransforms;

	public BaseCannonMovement ( Transform[] barrelTransforms )
	{
		this.barrelTransforms = barrelTransforms;
	}

	public void RespondToInput(Vector2[] touchPoints){
		if (touchPoints.Length > 0) {
			float deltaX = barrelTransforms[0].position.x - touchPoints[0].x;
			float deltaY = barrelTransforms[0].position.y - touchPoints[0].y;
			float barrelAngle = -(float)(Mathf.Atan (deltaX / deltaY) * 180.0 / 3.14);
			barrelTransforms[0].transform.localEulerAngles = new Vector3 (){ x = 0, y = 0, z = barrelAngle };
		}
	}
}


