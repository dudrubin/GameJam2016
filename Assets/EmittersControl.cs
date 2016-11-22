using System.Collections.Generic;
using Data;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class EmittersControl : MonoBehaviour {

	Transform leftEmitter;
	Transform rightEmitter;
	Transform timerBAr;
	public static Object enemyBase;
	public static Object shlomi;
	public static Object nezach;
	public static Object ronel;
	Sequence waveSequence;

	Dictionary<EnemyType,Object> prefabMap ;

	void Awake() {
		leftEmitter = transform.FindChild("EmitterLeft");
		rightEmitter = transform.FindChild("EmitterRight");
		timerBAr = transform.FindChild("TimerContainer");

		//enemies
		enemyBase = Resources.Load("Prefabs/Enemy");
		shlomi = Resources.Load("Prefabs/Shlomi");
		nezach = Resources.Load("Prefabs/Nezach");
		ronel = Resources.Load("Prefabs/Ronel");

		prefabMap =  new Dictionary<EnemyType, Object>() {
			{EnemyType.Nezach,nezach},
			{EnemyType.Shlomi,shlomi},
			{EnemyType.Ronel,ronel}
		};
	}

	public void StartEmitting(){
		NewWave();
	}

	public void StopEmitting(){
		if (waveSequence!= null) {
			waveSequence.Kill(false);
		}
		Enemy.KillAll();
	}

	/// <summary>
	///
	/// </summary>
	public void NewWave() {
		float waveDuration = 10;
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
		CreateWave(WaveGenerator.GenerateWave(leftEmitter.localPosition,true));
	}

	public void CreateWave(Wave wave) {
		waveSequence.Kill(false);

		waveSequence = DOTween.Sequence();

		foreach (EnemyType enemy in wave.enemies) {
			EnemyType temp = enemy;

			waveSequence.AppendCallback(()=> {

				Debug.LogFormat("Create {0}",temp);
				GameObject enemyObject = Instantiate(prefabMap[temp]) as GameObject;
				enemyObject.transform.SetParent(transform);
				enemyObject.transform.localPosition = leftEmitter.localPosition;
				enemyObject.GetComponent<Enemy>().StartMotion(wave.path);
			});
			waveSequence.AppendInterval(wave.timeBetweenEmits);
		}
	}
}
