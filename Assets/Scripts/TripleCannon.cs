using System;
using UnityEngine;
public class TripleCannon : Cannon
{
	protected override void Init ()
	{
		GameObject barrelRef = (GameObject)transform.FindChild("Base/BarrelBase1/Barrel").gameObject;
		GameObject barrel2Ref = (GameObject)transform.FindChild ("Base/BarrelBase2/Barrel").gameObject;
		GameObject barrel3Ref = (GameObject)transform.FindChild ("Base/BarrelBase3/Barrel").gameObject;
		Vector3 pivot = transform.FindChild ("Base/BarrelBase2").position;


		cannonProperties = Cannons.BASIC_CANNON;
		movementHandler = new BaseCannonMovement (new Transform[] { barrelRef.transform, barrel2Ref.transform, barrel3Ref.transform},pivot, cannonProperties);
		fireHandler = new BaseCannonFire (new Transform[] { barrelRef.transform, barrel2Ref.transform, barrel3Ref.transform }, cannonProperties);
	}

	protected override void RespondToInput ()
	{
		base.RespondToInput ();
	} 
}


