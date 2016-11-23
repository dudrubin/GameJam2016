using System;
using UnityEngine;
using DG.Tweening;

public class BaseCannonMovement 
{
	Transform[] barrelTransforms;
	public CannonProperties CannonProperties;
	Vector3 pivot;
	float originalY;
	public BaseCannonMovement ( Transform[] barrelTransforms , Vector3 pivot, CannonProperties properties )
	{
		this.barrelTransforms = barrelTransforms;
		this.CannonProperties = properties;
		originalY = barrelTransforms [0].localPosition.y;
	}

	public virtual void RespondToInput(Vector2[] touchPoints, Action doneCallback){
		if (touchPoints.Length > 0) {
			Sequence firingSequence = DOTween.Sequence ();

			foreach (Transform barrelTransform in this.barrelTransforms) {
				float deltaX = barrelTransform.parent.position.x - touchPoints[0].x;
				float deltaY = barrelTransform.parent.position.y - touchPoints[0].y;
				float barrelAngle = -(float)(Mathf.Atan (deltaX / deltaY) * 180.0 / 3.14);
				firingSequence.Insert (0, barrelTransform.parent.DOLocalRotate (new Vector3 (){ x = 0, y = 0, z = barrelAngle }, 
					CannonProperties.baseTurnSpeed));
			}

			foreach (Transform barrelTransform in this.barrelTransforms) {
				firingSequence.Insert (CannonProperties.baseTurnSpeed, barrelTransform.DOLocalMoveY (originalY - 0.15f, 0.03f));
			}
				
			firingSequence.InsertCallback (CannonProperties.baseTurnSpeed+ 0.04f, () => {
				if (doneCallback != null)
					doneCallback ();
			});

			foreach (Transform barrelTransform in this.barrelTransforms) {
				firingSequence.Insert(CannonProperties.baseTurnSpeed + 0.04f + 0.03f, barrelTransform.DOLocalMoveY(originalY, 0.3f) );
			}
//			barrelTransforms[0].transform.localEulerAngles =;
		}
	}
}


