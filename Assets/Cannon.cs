using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Cannon : MonoBehaviour {

	protected BaseCannonMovement movementHandler;
	protected BaseCannonFire fireHandler;
	protected CannonProperties cannonProperties;

	// Use this for initialization
	void Start () {
		Init ();
	}

	// Update is called once per frame
	void Update () {
		RespondToInput ();
	}

	protected virtual void Init(){
		GameObject barrelRef = (GameObject)transform.Find("Base/BarrelBase/Barrel").gameObject;
		Vector3 pivot = transform.FindChild ("Base/BarrelBase").position;

		cannonProperties = Cannons.BASIC_CANNON;
		movementHandler = new BaseCannonMovement (new Transform[] { barrelRef.transform},pivot, cannonProperties);
		fireHandler = new BaseCannonFire (new Transform[] { barrelRef.transform }, cannonProperties);
	}

	protected virtual void RespondToInput(){
		Vector2 pos;
		if (!getTouchPos(out pos))
			return;
		movementHandler.RespondToInput (new Vector2[]{ pos }, () => {
			fireHandler.RespondToInput (new Vector2[]{ pos });
		});
	}

	protected bool getTouchPos(out Vector2 touchPos){
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
