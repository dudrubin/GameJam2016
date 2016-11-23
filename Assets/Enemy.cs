using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour {

	static protected int TOTAL_ENEMIES = 0;
	static private List<GameObject> existingGameObjects = new List<GameObject>();
	static public Action<int> OnEnemiesChange;
	static public Action<Enemy> OnEnemyKilled;
	private static UnityEngine.Object xplosion;

	protected float initHealth = 0;
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
	Sequence healthSequence;

	public bool ExplodeAtDeath = true;

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
			return currentX - lastX < 0;
		}
	}

	public int Reward {
		get {
			return (int)Mathf.Round(10 * (1 + TOTAL_ENEMIES / 5.0f));
		}
	}

	protected virtual void OnAwake() {
	}

	protected virtual void OnHit() {
	}

	protected virtual void BeforeDestroyed() {
	}

	void OnDestroy() {
		BeforeDestroyed();
		TOTAL_ENEMIES--;

		if (ExplodeAtDeath) {

			GameObject s = Instantiate(xplosion) as GameObject;
			s.transform.SetParent(transform.parent);
			s.transform.localPosition = transform.localPosition;
		}
		else {
			Sounds.Play("monster");
		}


		existingGameObjects.Remove(gameObject);
		if (OnEnemiesChange != null) {
			OnEnemiesChange(TOTAL_ENEMIES);
		}
		if (healthSequence != null) {
			healthSequence.Kill(false);
		}
	}


	void Awake() {

		if (xplosion == null) {
			xplosion= Resources.Load("Prefabs/enemyXplostion");
		}

		initHealth = Health;
		healthBar = transform.FindChild("Canvas/HealthBar");
		healthBar.GetComponent<CanvasGroup>().alpha = 0;

		if (targetPosition == Vector3.zero) {
			targetPosition = GameObject.Find("EnemiesTarget").transform.localPosition;
		}
		OnAwake();
		TOTAL_ENEMIES++;
		existingGameObjects.Add(gameObject);
		if (OnEnemiesChange != null) {
			OnEnemiesChange(TOTAL_ENEMIES);
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
	public void StartMotion(List<Vector3> pathList, float duration = 8, List<Vector3> generalPath = null) {
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

	protected void Kill(bool split = false) {
		if (false && split) SplitEnemy();
		currentTween.Kill(false);

		OnEnemyKilled(this);
		Destroy(gameObject);
	}

	private Vector2 GetCurrentPathLocation() {
		float timePassedInSeconds = GetPassedTimeSinceCreated();
		float singlePartDuration = duration / pathList.Count;
		int floorIndex = (int) Math.Floor(timePassedInSeconds / singlePartDuration);
		int ceilIndex = (int) Math.Ceiling(timePassedInSeconds / singlePartDuration);
		return new Vector2(floorIndex, ceilIndex);
	}

	public void Hit(float damage) {
		Health -= damage;
		OnHit();
	}

	private void OnHealthChange(float oldEnergy, float newEnergy) {

		if (oldEnergy > 0) {
			newEnergy = Mathf.Max(0, newEnergy);
			healthSequence.Kill(false);
			healthSequence = DOTween.Sequence();
			CanvasGroup canvasGroup = healthBar.GetComponent<CanvasGroup>();
			canvasGroup.alpha = 1;
			healthSequence.Append(healthBar.DOScaleX(newEnergy / initHealth, 0.2f));
			healthSequence.AppendCallback(() => {
				if (newEnergy <= 0) Kill(true);
			});
			healthSequence.AppendInterval(0.5f);
			healthSequence.Append(canvasGroup.DOFade(0, 0.1f));
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
		left.GetComponent<Enemy>().StartMotion(leftPath, timeLeft, pathList);
		right.GetComponent<Enemy>().StartMotion(rightPath, timeLeft, pathList);
	}

	public void OnCompletedPath() {
		Debug.LogFormat("Complete");
		StartMotion(pathList, duration);
	}

	public float GetPassedTimeSinceCreated() {
		TimeSpan timePassedFromBeginning = DateTime.Now.Subtract(timeCreated);
		return (float)timePassedFromBeginning.TotalMilliseconds / 1000;
	}

	public GameObject CreateSplitInstance() {
		GameObject left = Instantiate(EmittersControl.shlomi) as GameObject;
		left.transform.SetParent(transform.parent);
		left.transform.localPosition = transform.localPosition;
		return left;
	}

	public static void KillAll() {

		Sequence s = DOTween.Sequence();
		for (int i = 0; i < existingGameObjects.Count; i++) {
			GameObject existingGameObject = existingGameObjects[i];
			existingGameObject.GetComponent<Enemy>().ExplodeAtDeath = false;
			s.AppendCallback(()=>GameObject.Destroy(existingGameObject));
			s.AppendInterval(0.001f);
		}
		s.AppendCallback(existingGameObjects.Clear);

	}


}
