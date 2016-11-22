using System;
using UnityEngine;
using System.Collections.Generic;

public class BaseCannonFire
{
	Transform[] barrelTransforms;
	float projectileSpeed = 10;
	List<GameObject> cannonProjectiles;
	public BaseCannonFire (Transform[] barrelTransforms)
	{
		this.barrelTransforms = barrelTransforms;
		this.cannonProjectiles = new List<GameObject> (); 
	}

	public virtual void RespondToInput(Vector2[] touchPoints){
		if (touchPoints.Length > 0) {
			Vector3 angle = barrelTransforms[0].localEulerAngles;
			GameObject ProjectileObj = GameObject.Instantiate (Resources.Load("Prefabs/Projectile")) as GameObject;
			Projectile projectile = ProjectileObj.GetComponent<Projectile> ();
			ProjectileObj.transform.position = barrelTransforms[0].position;
			ProjectileObj.transform.localEulerAngles = angle;
			Vector2 velocity = new Vector2 (-projectileSpeed * Mathf.Sin (angle.z * Mathf.Deg2Rad), projectileSpeed * Mathf.Cos (angle.z * Mathf.Deg2Rad));
			projectile.SetVelocity (velocity);
			cannonProjectiles.Add (ProjectileObj);
		}
	}
}


