using System;
using UnityEngine;
using System.Collections.Generic;

public class BaseCannonFire
{
	
	Transform[] barrelTransforms;
	public CannonProperties CannonProperties;
	List<GameObject> cannonProjectiles;
	public BaseCannonFire (Transform[] barrelTransforms, CannonProperties properties)
	{
		this.barrelTransforms = barrelTransforms;
		this.cannonProjectiles = new List<GameObject> (); 
		this.CannonProperties = properties;
		Debug.LogFormat ("Barrel position:{0}", barrelTransforms[0].position);
	}

	public virtual void RespondToInput(Vector2[] touchPoints){
		
		if (touchPoints.Length > 0) {
			Sounds.Play("laser");
			foreach (Transform barrelTransform in barrelTransforms) {
				Vector3 angle = barrelTransform.parent.localEulerAngles;
				string projectilePrefabName = Projectile.getPrefabName (this.CannonProperties.projectileType);
				GameObject ProjectileObj = GameObject.Instantiate (Resources.Load (string.Format("Prefabs/{0}",projectilePrefabName))) as GameObject;

				Vector3 nozzlePos = barrelTransform.FindChild ("Nozzle").position;
				Projectile projectile = ProjectileObj.GetComponent<Projectile> ();
				ProjectileObj.transform.position = nozzlePos;
				ProjectileObj.transform.localEulerAngles = angle;

				Vector2 velocity = new Vector2 (-CannonProperties.baseProjectileSpeed * Mathf.Sin (angle.z * Mathf.Deg2Rad), CannonProperties.baseProjectileSpeed * Mathf.Cos (angle.z * Mathf.Deg2Rad));
				projectile.SetVelocity (velocity);
				projectile.SetDamage (CannonProperties.baseDamage);
				cannonProjectiles.Add (ProjectileObj);
			}
		}
	}
		
}


