using UnityEngine;
using System.Collections;

public class RotateToPath : MonoBehaviour {
	Vector3 lastPos;
	Vector3 currentPos;
	public float offset = 90;
	public Transform targetTransform;

	// Use this for initialization
	void Start () {
		lastPos = currentPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		lastPos = currentPos;
		currentPos = transform.localPosition;
		Vector3 delta = currentPos - lastPos;
		float angle = Mathf.Atan2(delta.y,delta.x) * Mathf.Rad2Deg;
		if (targetTransform != null) {
			targetTransform.eulerAngles = new Vector3(0,0,angle + offset);
		}
		else {
			transform.eulerAngles = new Vector3(0,0,angle + offset);
		}
	}
}
