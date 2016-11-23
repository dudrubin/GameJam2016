using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
	public class WaveGenerator {

		private static int waveNumber = 0;

		private static Vector2 width = new Vector2(-1.8f, 1.8f);

		public static Wave GenerateWave(Vector3 emitterPosition, bool rtl = false) {
			Wave newWave = waves[waveNumber];
			newWave.path = MovementPaths.CreateSnakePath(emitterPosition, width, rightToLeft: rtl);
			waveNumber++;
			waveNumber = Math.Min(waveNumber, waves.Count);
			return newWave;
		}

		private static List<Wave> waves = new List<Wave>(){
			new Wave(new List<EnemyType>() {EnemyType.Shlomi, EnemyType.Nezach, EnemyType.Nezach,}),
			new Wave(new List<EnemyType>() {EnemyType.Nezach, EnemyType.Nezach, EnemyType.Shlomi,EnemyType.Nezach,}),
			new Wave(new List<EnemyType>() {EnemyType.Shlomi, EnemyType.Nezach, EnemyType.Ronel,EnemyType.Nezach,EnemyType.Shlomi,}),
			new Wave(new List<EnemyType>() {EnemyType.Shlomi, EnemyType.Nezach, EnemyType.Ronel,EnemyType.Nezach,EnemyType.Shlomi,}),
		};

	}
}