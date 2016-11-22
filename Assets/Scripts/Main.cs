using DG.Tweening;
using UnityEngine;

public class Main : MonoBehaviour {

	Transform creaturesCount;
	const int MAX_CREATURES = 10;
	EmittersControl emittersControl;

	// Use this for initialization
	void Awake () {
		emittersControl = GameObject.Find("Emitters").GetComponent<EmittersControl>();
		creaturesCount = transform.FindChild("HUD/CreaturesCount");
		Enemy.OnEnemiesChange += OnEnemiesChange;
		DOVirtual.DelayedCall(1,StartGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEnemiesChange(int count) {
		float ratio = Mathf.Min((float)count/MAX_CREATURES,1);
		creaturesCount.DOScaleY(ratio,0.1f);
		Debug.LogFormat("Enemies Count {0} ",count);
		if (count >= MAX_CREATURES) {
			OnGameOver();
		}
	}

	public void OnGameOver() {
		Debug.LogFormat("GameOver");
		emittersControl.StopEmitting();
	}

	public void StartGame() {
		emittersControl.StartEmitting();
	}
}
