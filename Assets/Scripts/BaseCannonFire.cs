using System;
using UnityEngine;
using System.Collections.Generic;

public class BaseCannonFire
{
	
	Transform[] barrelTransforms;
	CannonProperties cannonProperties;
	List<GameObject> cannonProjectiles;
	public BaseCannonFire (Transform[] barrelTransforms, CannonProperties properties)
	{
		this.barrelTransforms = barrelTransforms;
		this.cannonProjectiles = new List<GameObject> (); 
		this.cannonProperties = properties;
		Debug.LogFormat ("Barrel position:{0}", barrelTransforms[0].position);
	}

	public virtual void RespondToInput(Vector2[] touchPoints){
		if (touchPoints.Length > 0) {
			foreach (Transform barrelTransform in barrelTransforms) {
				Vector3 angle = barrelTransform.parent.localEulerAngles;
				GameObject ProjectileObj = GameObject.Instantiate (Resources.Load ("Prefabs/Projectile")) as GameObject;
				Projectile projectile = ProjectileObj.GetComponent<Projectile> ();
				Debug.LogFormat("2 barrelTransform.x = {0}" ,barrelTransform.position.x);
				ProjectileObj.transform.position = barrelTransform.position;
				ProjectileObj.transform.localEulerAngles = angle;
				Vector2 velocity = new Vector2 (-cannonProperties.baseProjectileSpeed * Mathf.Sin (angle.z * Mathf.Deg2Rad), cannonProperties.baseProjectileSpeed * Mathf.Cos (angle.z * Mathf.Deg2Rad));
				projectile.SetVelocity (velocity);
				projectile.SetDamage (cannonProperties.baseDamage);
				cannonProjectiles.Add (ProjectileObj);
			}
		}
	}
		
}


