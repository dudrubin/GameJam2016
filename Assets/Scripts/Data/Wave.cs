using System.Collections.Generic;
using UnityEngine;

namespace Data {
	public class Wave {
		public Wave() {

		}
		public Wave(	 List<EnemyType> enemies,
						float timeBetweenEmits = 0.3f) {
			this.path = path;
			this.enemies = enemies;
			this.timeBetweenEmits = timeBetweenEmits;
		}

		public List<Vector3> path;
		public List<EnemyType> enemies;
		public float timeBetweenEmits = 0.3f;
	}
}