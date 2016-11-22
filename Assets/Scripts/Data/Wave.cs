using System.Collections.Generic;
using UnityEngine;

namespace Data {
	public class Wave {
		public List<Vector3> path;
		public List<EnemyType> enemies;
		public float timeBetweenEmits = 0.3f;
	}
}