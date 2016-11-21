using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


	Vector3 direction = Vector3.zero;
	private Renderer rend;

	void Awake() {
		rend = GetComponent<Renderer>();
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
		if (GameBorders.IsOutVerticaly(transform.localPosition)) {
			direction.y *= -1;
		}
	}


}
