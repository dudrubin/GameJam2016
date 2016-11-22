using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Cannon : MonoBehaviour {

	GameObject barrelRef;
	BaseCannonMovement movementHandler;
	BaseCannonFire fireHandler;
	CannonProperties cannonProperties;

	// Use this for initialization
	void Start () {
		barrelRef = GameObject.Find ("Barrel");
		cannonProperties = Cannons.BASIC_CANNON;
		movementHandler = new BaseCannonMovement (new Transform[] { barrelRef.transform}, cannonProperties);
		fireHandler = new BaseCannonFire (new Transform[] { barrelRef.transform }, cannonProperties);
	}


	// Update is called once per frame
	void Update () {
		Vector2 pos;
		if (!getTouchPos(out pos))
			return;
		movementHandler.RespondToInput (new Vector2[]{ pos }, () => {
			fireHandler.RespondToInput (new Vector2[]{ pos });
		});

	}



	private bool getTouchPos(out Vector2 touchPos){
		touchPos = new Vector2();
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Ended) {
				touchPos = Input.GetTouch (i).position;
				Debug.LogFormat ("touch detected {0},{1}", touchPos.x, touchPos.y);
				return true;
			}
		}
		if (Input.GetMouseButtonDown(0)){
			touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
			Debug.LogFormat ("click detected {0},{1}", touchPos.x, touchPos.y);
			return true;
		}
		return false;
	}
}
