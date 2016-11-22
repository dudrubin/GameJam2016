using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EmittersControl : MonoBehaviour {

	Transform leftEmitter;
	Transform rightEmitter;
	Transform timerBAr;
	public static Object enemyBase;

	void Awake() {
		leftEmitter = transform.FindChild("EmitterLeft");
		rightEmitter = transform.FindChild("EmitterRight");
		timerBAr = transform.FindChild("TimerContainer");
		enemyBase = Resources.Load("Prefabs/Enemy");
		NewWave();
	}

	/// <summary>
	///
	/// </summary>
	public void NewWave() {
		float waveDuration = 7;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(timerBAr.DOScaleX(1, 0.05f));
		sequence.AppendCallback(Emit);
		sequence.Append(timerBAr.DOScaleX(0, waveDuration).SetEase(Ease.Linear));
		sequence.AppendCallback(NewWave);
	}

	/// <summary>
	///
	/// </summary>
	private void Emit() {
		GameObject left = Instantiate(enemyBase) as GameObject;
		GameObject right = Instantiate(enemyBase) as GameObject;
		right.transform.SetParent(transform);
		right.transform.localPosition = rightEmitter.localPosition;
		left.transform.SetParent(transform);
		left.transform.localPosition = leftEmitter.localPosition;
		Vector3 originalLeftPos = left.transform.localPosition;
		Vector3 originalRightPos = right.transform.localPosition;
		Vector2 width = new Vector2(-1.8f,1.8f);

		List<Vector3> pathLeft = MovementPaths.CreateSnakePath(originalLeftPos,width, rightToLeft: false);
		List<Vector3> pathRight = MovementPaths.CreateSnakePath(originalRightPos,width, rightToLeft: true);
		left.GetComponent<Enemy>().StartMotion(pathLeft);
		right.GetComponent<Enemy>().StartMotion(pathRight);
	}
}
