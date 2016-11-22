using DG.Tweening;
using UnityEngine;

public class Splatter : MonoBehaviour {

	Sequence s;
	// Use this for initialization
	void Start() {
		s = DOTween.Sequence();
		s.AppendInterval(2);
		s.Append(GetComponent<SpriteRenderer>().DOFade(0, 5));
		s.AppendCallback(() => {
			Destroy(gameObject);
		});
	}

	void OnDestroy() {
		if (s != null) {
			s.Kill(false);
		}
	}

}
