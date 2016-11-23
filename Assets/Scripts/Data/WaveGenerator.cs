using System.Collections.Generic;
using UnityEngine;

namespace Data {
	public class WaveGenerator {

		private static Vector2 width = new Vector2(-1.8f,1.8f);

		public static Wave GenerateWave(Vector3 emitterPosition,bool rtl = false) {
			Wave newWave = new Wave();
			newWave.enemies = new List<EnemyType>() {
				EnemyType.Ronel,EnemyType.Ronel,EnemyType.Ronel,
			};
			newWave.path =  MovementPaths.CreateSnakePath(emitterPosition,width, rightToLeft: false);
			return newWave;
		}

	}
}