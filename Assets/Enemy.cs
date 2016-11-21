using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private float initEnergy = 0;
	private float health = 100;
	private Vector3 direction = Vector3.zero;
	private Transform healthBar;

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
	}

	/// <summary>
	///
	/// </summary>
	/// <param name="v"></param>
	public void SetDirection(Vector3 v) {
		direction = v;
	}

	void Update() {
		Vector3 v = transform.localPosition;
		v += direction;
		transform.localPosition = v;
		if (GameBorders.IsOutHorizontal(transform.localPosition)) {
			direction.x *= -1;
		}
		if (GameBorders.IsOutVertical(transform.localPosition)) {
			direction.y *= -1;
		}
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
