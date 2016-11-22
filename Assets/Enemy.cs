using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private float initEnergy = 0;
	private float health = 100;
	private Vector3 direction = Vector3.zero;
	private Transform healthBar;
	protected static Vector3 targetPosition = Vector3.zero;
	private float duration = 8;
	private DateTime timeCreated;
	List<Vector3> pathList;
	Tween currentTween;
	float lastX = 0;
	float currentX = 0;

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

	private bool RightToLeft {
		get {
			return currentX - lastX  < 0;
		}
	}


	void Awake() {

		initEnergy = Health;
		healthBar = transform.FindChild("Canvas/HealthBar");

		if (targetPosition == Vector3.zero) {
			targetPosition = GameObject.Find("EnemiesTarget").transform.localPosition;
		}

	}

	void Update() {
		lastX = currentX;
		currentX = transform.localPosition.x;
	}

	/// <summary>
	///
	/// </summary>
	/// <param name="pathList"></param>
	public void StartMotion(List<Vector3> pathList, float duration = 8,List<Vector3> generalPath = null) {
		this.duration = duration;
		if (generalPath != null) {
			this.pathList = generalPath;
			this.duration = 8;
		}
		else {
			this.pathList = pathList;
		}
		timeCreated = DateTime.Now;
		currentTween = transform.DOLocalPath(pathList.ToArray(), duration, PathType.CatmullRom, PathMode.TopDown2D,
				10).SetEase(Ease.Linear).OnComplete(OnCompletedPath);
	}

	private void Kill(bool split = false) {
		if (false && split) SplitEnemy();
		currentTween.Kill(false);
		Destroy(gameObject);
	}

	private Vector2 GetCurrentPathLocation() {
		float timePassedInSeconds = GetPassedTimeSinceCreated();
		float singlePartDuration = duration / pathList.Count;
		int floorIndex = (int) Math.Floor(timePassedInSeconds / singlePartDuration);
		int ceilIndex = (int) Math.Ceiling(timePassedInSeconds / singlePartDuration);
		return new Vector2(floorIndex, ceilIndex);
	}

	private void OnHealthChange(float oldEnergy, float newEnergy) {

		if (oldEnergy > 0) {
			newEnergy = Mathf.Max(0, newEnergy);
			healthBar.DOScaleX(newEnergy / initEnergy, 0.2f).OnComplete(() => {
				if (newEnergy <= 0) Kill(true);
			});
		}
	}

	public void SplitEnemy() {

		GameObject left = CreateSplitInstance();
		GameObject right = CreateSplitInstance();
		//get number of levels till end
		Vector2 indices = GetCurrentPathLocation();
		float points = pathList.Count - 2 - indices.x; // number of points
		points = (float) Math.Ceiling(points / 4);

		List<Vector3> leftPath = MovementPaths.CreateSnakePath(transform.localPosition,
				new Vector2(-1.8f, 1.8f),
				levels: (int)points,
				rightToLeft: RightToLeft);
		List<Vector3> rightPath = MovementPaths.CreateSnakePath(transform.localPosition,
				new Vector2(-1.8f, 1.8f),
				levels: (int)points,
				rightToLeft: !RightToLeft);

		float timeLeft = duration - GetPassedTimeSinceCreated();
		left.GetComponent<Enemy>().StartMotion(leftPath, timeLeft,pathList);
		right.GetComponent<Enemy>().StartMotion(rightPath, timeLeft,pathList);
	}

	public void OnCompletedPath() {
		Debug.LogFormat("Complete");
		StartMotion(pathList,duration);
	}

	public float GetPassedTimeSinceCreated() {
		TimeSpan timePassedFromBeginning = DateTime.Now.Subtract(timeCreated);
		return (float)timePassedFromBeginning.TotalMilliseconds / 1000;
	}

	public GameObject CreateSplitInstance() {
		GameObject left = Instantiate(EmittersControl.enemyBase) as GameObject;
		left.transform.SetParent(transform.parent);
		left.transform.localPosition = transform.localPosition;
		return left;
	}


}
