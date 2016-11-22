using DG.Tweening;
using UnityEngine;

public class Wiggle : MonoBehaviour {

	Vector3 initPos;
	Tween currentTween;
	float amp = 0.1f;
	int directionX = 1;
	int directionY = 1;

	// Use this for initialization
	void Awake() {
		initPos = transform.localPosition;
		if (Random.Range(0,100) > 50) directionX = -1;
		if (Random.Range(0,100) > 50) directionY = -1;
		MoveToRandomPos();
	}

	void OnDestroy() {
		if (currentTween != null) {
			currentTween.Kill(false);
		}
	}

	public void MoveToRandomPos() {
		directionX *= -1;
		directionY *= -1;
		Vector3 target = new Vector3(directionX * Random.Range(0, amp), directionY * Random.Range(0, amp));
		currentTween = transform.DOLocalMove(target + initPos, 0.5f)
		.SetEase(Ease.InOutSine)
		.OnComplete(MoveToRandomPos);
	}

}
