using DG.Tweening;
using UnityEngine;

namespace Enemies {
	public class Ronel : Enemy {

		private static Object wallPrefab;
		private const float MIN_SPIT_DURATION = 2;
		private const float MAX_SPIT_DURATION = 8;

		GameObject images ;
		GameObject hurtImages ;
		Tween hitTween;
		Tween wallSpitCounter;

		protected override  void OnAwake() {
			base.OnAwake();
			if (wallPrefab == null) {
				wallPrefab= Resources.Load("Prefabs/Wall");
			}

			images = transform.FindChild("Image").gameObject;
			hurtImages = transform.FindChild("Image/Hurt").gameObject;
			float nextDuration = Random.Range(MIN_SPIT_DURATION,MAX_SPIT_DURATION);
			Debug.LogFormat("nextDuration {0}", nextDuration);
			wallSpitCounter = DOVirtual.DelayedCall(nextDuration,SpitWall);
		}

		protected override void BeforeDestroyed() {
			if (hitTween != null) {
				hitTween.Kill(false);
			}
			if (wallSpitCounter != null) {
				wallSpitCounter.Kill(false);
			}
		}

		protected override void OnHit() {
			base.OnHit();
			hitTween.Kill(false);

			hurtImages.GetComponent<SpriteRenderer>().enabled = true;
			images.GetComponent<SpriteRenderer>().enabled = false;

			hitTween = DOVirtual.DelayedCall(0.5f,()=>{
				hurtImages.GetComponent<SpriteRenderer>().enabled = false;
				images.GetComponent<SpriteRenderer>().enabled = true;
			});
		}

		public void SpitWall() {
			//spit wall
			GameObject s = Instantiate(wallPrefab) as GameObject;
			s.transform.SetParent(transform.parent);
			s.transform.localPosition = transform.localPosition;
			s.transform.localScale = new Vector3(Random.Range(2,3),Random.Range(2,3),1);

			float nextDuration = Random.Range(MIN_SPIT_DURATION,MAX_SPIT_DURATION);
			Debug.LogFormat("nextDuration {0}", nextDuration);
			wallSpitCounter = DOVirtual.DelayedCall(nextDuration,SpitWall);
		}
	}
}