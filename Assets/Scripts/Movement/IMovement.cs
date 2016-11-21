using System;
using UnityEngine;
public interface IMovement
{
	void Init (Vector3 position, Vector3 direction);
	Vector3 UpdatePosition ();
}

