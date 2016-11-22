using DG.Tweening;
using UnityEngine;

namespace Enemies {

	public class Nezach : Enemy {


		private static Object splat;

		GameObject images ;
		GameObject hurtImages ;
		Tween hitTween;

		protected override  void OnAwake() {
			base.OnAwake();
			if (splat == null) {
				splat= Resources.Load("Prefabs/Splatter");
			}

			images = transform.FindChild("Image").gameObject;
			hurtImages = transform.FindChild("Image/Hurt").gameObject;
		}

		protected override void BeforeDestroyed() {
			if (hitTween != null) {
				hitTween.Kill(false);
			}

			GameObject s = Instantiate(splat) as GameObject;
			s.transform.SetParent(transform.parent);
			s.transform.localPosition = transform.localPosition;
			s.transform.eulerAngles = new Vector3(0,0,Random.Range(0,360));

			base.OnHit();
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

	}
}