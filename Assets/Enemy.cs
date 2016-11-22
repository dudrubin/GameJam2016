using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private float initEnergy = 0;
	private float health = 100;
	private Vector3 direction = Vector3.zero;
	private Transform healthBar;
	protected static Vector3 targetPosition = Vector3.zero;

	public float Health {
		set {
			float old = health;
			health = value;
			OnHealthChange(old, health);
		}
		get {
			return health;
		}
	}


	void Awake() {

		initEnergy = Health;
		healthBar = transform.FindChild("Canvas/HealthBar");

		if (targetPosition == Vector3.zero) {
			targetPosition = GameObject.Find("EnemiesTarget").transform.localPosition;
		}

	}

	/// <summary>
	///
	/// </summary>
	/// <param name="mirror"></param>
	public void Create(bool mirror) {
		Vector3 originalPos = transform.localPosition;
		int duration = 8;
		List<Vector3> pathList = MovementPaths.CreateSnakePath(originalPos,rightToLeft: mirror);
		pathList.Add(targetPosition);
		transform.DOLocalPath(pathList.ToArray(), duration, PathType.CatmullRom, PathMode.TopDown2D, 10).SetEase(Ease.Linear);
	}

	private void OnHealthChange(float oldEnergy, float newEnergy) {

		if (oldEnergy > 0) {
			newEnergy = Mathf.Max(0, newEnergy);
			healthBar.DOScaleX(newEnergy / initEnergy, 0.2f).OnComplete(() => {
				if (newEnergy <= 0) Destroy(gameObject);
			});
		}
	}



}
