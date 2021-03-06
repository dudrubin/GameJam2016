﻿using System.Collections.Generic;
using Data;
using DG.Tweening;
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
	Sequence mainSequence;
	SpriteMask mask;

	Dictionary<EnemyType,Object> prefabMap ;

	void Awake() {
		leftEmitter = transform.FindChild("EmitterLeft");
		rightEmitter = transform.FindChild("EmitterRight");
		timerBAr = transform.FindChild("TimerContainer");
		mask = GetComponent<SpriteMask>();

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
		if (mainSequence!= null) {
			mainSequence.Kill(false);
		}
	}

	/// <summary>
	///
	/// </summary>
	public void NewWave() {
		float waveDuration = 10;
		mainSequence = DOTween.Sequence();
		mainSequence.Append(timerBAr.DOScaleX(1, 0.05f));
		mainSequence.AppendCallback(Emit);
		mainSequence.Append(timerBAr.DOScaleX(0, waveDuration).SetEase(Ease.Linear));
		mainSequence.AppendCallback(NewWave);
	}

	/// <summary>
	///
	/// </summary>
	private void Emit() {
		bool right = Random.Range(0,100) >50;
		Vector3 position = right? rightEmitter.localPosition:leftEmitter.localPosition;
		CreateWave(WaveGenerator.GenerateWave(position,right));
	}

	public void CreateWave(Wave wave) {

		waveSequence.Kill(false);

		waveSequence = DOTween.Sequence();

		foreach (EnemyType enemy in wave.enemies) {
			EnemyType temp = enemy;
			//mask.forceDefaultMaterialOnChilds = true;
			waveSequence.AppendCallback(()=> {

				Debug.LogFormat("Create {0}",temp);
				GameObject enemyObject = Instantiate(prefabMap[temp]) as GameObject;
				enemyObject.transform.SetParent(transform);
				enemyObject.transform.localPosition = leftEmitter.localPosition;
				enemyObject.GetComponent<Enemy>().StartMotion(wave.path);
//				mask.update();
				foreach (SpriteRenderer renderer in enemyObject.GetComponentsInChildren<SpriteRenderer>()) {
					mask.updateSprites(renderer.transform);
				}
			});
			waveSequence.AppendInterval(wave.timeBetweenEmits);
		}
	}
}
