using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Cannon : MonoBehaviour {

	GameObject barrelRef;
	Main gameRef;
	List<GameObject> cannonProjectiles;
	BaseCannonMovement movementHandler;
	float PROJECTILE_SPEED = 10;

	// Use this for initialization
	void Start () {
		barrelRef = GameObject.Find ("Barrel");
		gameRef = GetComponentInParent<Main> ();
		cannonProjectiles = new List<GameObject> ();
		movementHandler = new BaseCannonMovement (new Transform[] { barrelRef.transform});
	}


	// Update is called once per frame
	void Update () {
		Vector2 pos;
		if (!getTouchPos(out pos))
			return;
		movementHandler.RespondToInput (new Vector2[]{ pos });
		createProjectile (barrelRef.transform.position, barrelRef.transform.localEulerAngles);
	}

	private void createProjectile(Vector2 pos, Vector3 angle){
		GameObject ProjectileObj = GameObject.Instantiate (Resources.Load("Prefabs/Projectile")) as GameObject;
		Projectile projectile = ProjectileObj.GetComponent<Projectile> ();
		ProjectileObj.transform.position = pos;
		ProjectileObj.transform.localEulerAngles = angle;
		Vector2 velocity = new Vector2 (-PROJECTILE_SPEED * Mathf.Sin (angle.z * Mathf.Deg2Rad), PROJECTILE_SPEED * Mathf.Cos (angle.z * Mathf.Deg2Rad));
		Debug.LogFormat ("New projectile: pos:{0} angle {1} velocity: {2}", pos, angle.z, velocity);
		Debug.LogFormat ("cos angle x: {0} sin angle : {1}", Mathf.Cos (angle.z * Mathf.Deg2Rad), Mathf.Sin (angle.z * Mathf.Deg2Rad) );
		projectile.SetVelocity (velocity);
		cannonProjectiles.Add (ProjectileObj);
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
