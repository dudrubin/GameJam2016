using UnityEngine;

public class Main : MonoBehaviour {


	// Use this for initialization
	void Awake () {
		//GameObject.Instantiate(Resources.Load("Prefabs/Temp"));
		Enemy.OnEnemiesChange += OnEnemiesChange;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnEnemiesChange(int count) {
		Debug.LogFormat("Enemies Count {0} ",count);
	}
}
