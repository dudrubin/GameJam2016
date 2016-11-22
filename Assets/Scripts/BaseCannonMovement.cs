using System;
using UnityEngine;
using DG.Tweening;

public class BaseCannonMovement 
{
	Transform[] barrelTransforms;
	CannonProperties cannonProperties;
	public BaseCannonMovement ( Transform[] barrelTransforms , CannonProperties properties )
	{
		this.barrelTransforms = barrelTransforms;
		this.cannonProperties = properties;
	}

	public virtual void RespondToInput(Vector2[] touchPoints, Action doneCallback){
		if (touchPoints.Length > 0) {
			float deltaX = barrelTransforms[0].position.x - touchPoints[0].x;
			float deltaY = barrelTransforms[0].position.y - touchPoints[0].y;
			float barrelAngle = -(float)(Mathf.Atan (deltaX / deltaY) * 180.0 / 3.14);
			this.barrelTransforms [0].DOLocalRotate (new Vector3 (){ x = 0, y = 0, z = barrelAngle }, 
				cannonProperties.baseTurnSpeed).OnComplete(()=>{ 
					if (doneCallback != null)
						doneCallback ();
				});;
//			barrelTransforms[0].transform.localEulerAngles =;

		}
	}
}


