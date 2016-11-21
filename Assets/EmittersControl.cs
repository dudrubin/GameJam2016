using DG.Tweening;
using UnityEngine;

public class EmittersControl : MonoBehaviour {

	Transform leftEmitter;
	Transform rightEmitter;
	Transform timerBAr;
	private Object enemyBase;

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
		float waveDuration = 10;
		Sequence sequence = DOTween.Sequence();
		sequence.Append(timerBAr.DOScaleX(1, 0.05f));
		sequence.AppendCallback(Emit);
		sequence.Append(timerBAr.DOScaleX(0, waveDuration));
		sequence.AppendCallback(NewWave);

	}

	/// <summary>
	///
	/// </summary>
	private void Emit() {
		GameObject left = Instantiate(enemyBase) as GameObject;
		GameObject right = Instantiate(enemyBase) as GameObject;
		left.transform.SetParent(transform);
		right.transform.SetParent(transform);
		left.transform.localPosition = leftEmitter.localPosition;
		right.transform.localPosition = rightEmitter.localPosition;
	}
}
