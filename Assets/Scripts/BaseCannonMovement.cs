using System;
using UnityEngine;
using DG.Tweening;

public class BaseCannonMovement 
{
	protected bool moving = false;
	protected Transform[] barrelTransforms;
	protected Vector3 pivot;
	protected float originalY;
	protected float currAngle;
	public CannonProperties CannonProperties;


	public BaseCannonMovement ( Transform[] barrelTransforms , Vector3 pivot, CannonProperties properties )
	{
		this.barrelTransforms = barrelTransforms;
		this.CannonProperties = properties;
		originalY = barrelTransforms [0].localPosition.y;
	}

	public virtual void RespondToInput(Vector2[] touchPoints, Action doneCallback){
		if (moving) 
			return;
		float fastPhaseDuration = 0.002f;
		float slowPhaseDuration = 0.02f;

		moving = true;

		if (touchPoints.Length > 0) {
			Sequence firingSequence = DOTween.Sequence ();
			float turnDuration = 0;

			foreach (Transform barrelTransform in this.barrelTransforms) {
				float deltaX = barrelTransform.parent.position.x - touchPoints[0].x;
				float deltaY = barrelTransform.parent.position.y - touchPoints[0].y;
				float barrelAngle = -(float)(Mathf.Atan (deltaX / deltaY) * 180.0 / 3.14);
				float angleDelta = Math.Abs (currAngle - barrelAngle);
				turnDuration = ( angleDelta / 180.0f) * CannonProperties.baseTurnSpeed;

				firingSequence.Insert (0, barrelTransform.parent.DOLocalRotate (new Vector3 (){ x = 0, y = 0, z = barrelAngle }, 
					turnDuration));
			}

			foreach (Transform barrelTransform in this.barrelTransforms) {
				firingSequence.Insert (turnDuration, barrelTransform.DOLocalMoveY (originalY - 0.25f, fastPhaseDuration));
			}
				
			firingSequence.InsertCallback (turnDuration + fastPhaseDuration/2, () => {
				if (doneCallback != null)
					doneCallback ();
			});

			foreach (Transform barrelTransform in this.barrelTransforms) {
				firingSequence.Insert(turnDuration + fastPhaseDuration , barrelTransform.DOLocalMoveY(originalY, slowPhaseDuration) );
			}
			firingSequence.OnComplete (() => {
				moving = false;
			});
//			barrelTransforms[0].transform.localEulerAngles =;
		}
	}
}


