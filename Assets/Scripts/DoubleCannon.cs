using System;
using UnityEngine;
namespace AssemblyCSharp
{
	public class DoubleCannon : Cannon
	{
		protected override void Init ()
		{
			GameObject barrelRef = (GameObject)transform.FindChild("Base/BarrelBase1/Barrel").gameObject;
			GameObject barrel2Ref = (GameObject)transform.FindChild ("Base/BarrelBase2/Barrel").gameObject;

			Vector3 pivot = transform.FindChild ("Base/BarrelBase2").position;
			movementHandler = new BaseCannonMovement (new Transform[] { barrelRef.transform, barrel2Ref.transform}, pivot, cannonProperties);
			fireHandler = new BaseCannonFire (new Transform[] { barrelRef.transform, barrel2Ref.transform }, cannonProperties);
		}

		protected override void RespondToInput ()
		{
			base.RespondToInput ();
		} 
	}
}

