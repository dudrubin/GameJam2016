using DG.Tweening;
using UnityEngine;

namespace Enemies {
	public class Wall : Enemy {

		private const int WALL_HEALTH = 30;
		private const float MINIMUM_Y = -2.5f;
		private const float FALL_DISTANCE = 5;
		private const float DURATION = 7;

		GameObject images;
		GameObject hurtImages;
		Tween hitTween;
		Tween motionTween;

		protected override void OnAwake() {
			base.OnAwake();
			images = transform.FindChild("Image").gameObject;
			hurtImages = transform.FindChild("Image/Hurt").gameObject;
			initHealth = WALL_HEALTH;
			Health = WALL_HEALTH;
			TOTAL_ENEMIES--; //make this ignored by enemies counter

			//move down slowly  but don't pass min point
			float targetPosition = Mathf.Max(transform.localPosition.y - FALL_DISTANCE, MINIMUM_Y);
			Debug.LogFormat("Current ypos {0} target yPos {1}", transform.localPosition.y, targetPosition);
			motionTween = transform.DOLocalMoveY(targetPosition, 4);
		}

		protected override void BeforeDestroyed() {
			if (hitTween != null) {
				hitTween.Kill(false);
			}
			if (motionTween != null) {
				motionTween.Kill(false);
			}
			TOTAL_ENEMIES++; //make this ignored by enemies counter
		}

		protected override void OnHit() {
			base.OnHit();
			hitTween.Kill(false);

			hurtImages.GetComponent<SpriteRenderer>().enabled = true;
			images.GetComponent<SpriteRenderer>().enabled = false;

			hitTween = DOVirtual.DelayedCall(0.5f, () => {
				hurtImages.GetComponent<SpriteRenderer>().enabled = false;
				images.GetComponent<SpriteRenderer>().enabled = true;
			});
		}
	}
}