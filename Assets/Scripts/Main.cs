using UnityEngine;

public class Main : MonoBehaviour {


	// Use this for initialization
	void Awake () {
		GameObject cannonObj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/DoubleBarrelCannon"));
		cannonObj.transform.position = new Vector3 (){ x = 0.0f, y = -4.25f, z = 0.0f };
		emittersControl = GameObject.Find("Emitters").GetComponent<EmittersControl>();
		creaturesCount = transform.FindChild("HUD/CreaturesCount");
		Enemy.OnEnemiesChange += OnEnemiesChange;
		DOVirtual.DelayedCall(1,StartGame);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
