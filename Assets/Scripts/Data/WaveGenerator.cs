using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
	public class WaveGenerator {

		private static int waveNumber = 0;

		private static Vector2 width = new Vector2(-1.8f, 1.8f);

		public static void Init(){
			waveNumber = 0;
		}

		public static Wave GenerateWave(Vector3 emitterPosition, bool rtl = false) {
			Wave newWave = waves[waveNumber];
			newWave.path = MovementPaths.CreateSnakePath(emitterPosition, width, rightToLeft: rtl);
			waveNumber++;
			waveNumber = Math.Min(waveNumber, waves.Count-1);
			return newWave;
		}

		private static List<Wave> waves = new List<Wave>(){
			new Wave(new List<EnemyType>() {EnemyType.Shlomi, EnemyType.Ronel, EnemyType.Nezach,},0.5f ),
			new Wave(new List<EnemyType>() {EnemyType.Nezach, EnemyType.Ronel, EnemyType.Ronel,EnemyType.Ronel,},0.5f),
			new Wave(new List<EnemyType>() {EnemyType.Shlomi, EnemyType.Ronel, EnemyType.Ronel,EnemyType.Ronel, EnemyType.Shlomi,},0.5f),
			new Wave(new List<EnemyType>() {EnemyType.Shlomi, EnemyType.Nezach, EnemyType.Ronel,EnemyType.Ronel, EnemyType.Ronel,},0.5f),
		};

	}
}