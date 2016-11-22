using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Enemies {

	public class Shlomi : Enemy {

		int level = 0;
		int[] healthCount = new int[]{100,150,200};
		List<GameObject> images = new List<GameObject>();
		List<GameObject> hurtImages = new List<GameObject>();
		float evolutionTime = 4;

		protected override  void OnAwake() {
			base.OnAwake();
			images.Add(transform.FindChild("ImageLevel1").gameObject);
			images.Add(transform.FindChild("ImageLevel2").gameObject);
			images.Add(transform.FindChild("ImageLevel3").gameObject);
			hurtImages.Add(transform.FindChild("ImageLevel1/Hurt").gameObject);
			hurtImages.Add(transform.FindChild("ImageLevel2/Hurt").gameObject);
			hurtImages.Add(transform.FindChild("ImageLevel3/Hurt").gameObject);
			SetLevel();
			DOVirtual.DelayedCall(evolutionTime,Evolve);
		}

		protected override void OnHit() {
			base.OnHit();

			for (int i = 0; i < images.Count; i++) {
				hurtImages[i].GetComponent<SpriteRenderer>().enabled = true;
				images[i].GetComponent<SpriteRenderer>().enabled = false;
			}
			DOVirtual.DelayedCall(0.5f,()=>{
				for (int i = 0; i < images.Count; i++) {
					hurtImages[i].GetComponent<SpriteRenderer>().enabled = false;
					images[i].GetComponent<SpriteRenderer>().enabled = true;
				}
			});
		}

		private void Evolve() {
			level++;

			if (level >= images.Count) {
				Explode();
			}
			else {
				Debug.LogFormat("evolve to {0}",level);
				SetLevel();
				DOVirtual.DelayedCall(evolutionTime,Evolve);
			}
		}

		public void Explode() {
			Debug.LogFormat("BOOM! ");
			Kill(true);
		}

		public void SetLevel() {

			//set active for current image
			for (int i = 0; i < images.Count; i++) {
				GameObject gameObject = images[i];
				gameObject.SetActive(i == level);
			}

			initEnergy = healthCount[level];
			Health = healthCount[level];
		}

	}
}