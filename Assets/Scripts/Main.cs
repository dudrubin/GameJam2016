using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Main : MonoBehaviour {

	const int MAX_CREATURES = 13;
	int money = 10000;
	int cannonLevel = 1;
	public Button upgradeButton;
	public Text moneyLabel;
	public Text upgradeCost;
	public Text cannonLevelLabel;
	Transform creaturesCount;
	EmittersControl emittersControl;
	GameObject cannonObj;
	Queue<int> breakSteps;
	WindSheild glass;
	GameObject startScreen;
	GameObject endScreen;
	Button startButton;
	Button restartButton;

	// Use this for initialization
	void Awake() {
		CreateBreakSteps();
		glass = GameObject.Find("Windshield").GetComponent<WindSheild>();
		startScreen  = GameObject.Find("StartScreen");
		endScreen  = GameObject.Find("EndScreen");
		startButton = GameObject.Find("StartButton").GetComponent<Button>();
		restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
		emittersControl = GameObject.Find("Emitters").GetComponent<EmittersControl>();
		creaturesCount = transform.FindChild("HUD/CreaturesCount");
		upgradeButton = GameObject.Find ("UpgradeButton").GetComponent<Button>();
		moneyLabel = GameObject.Find ("Money").GetComponent<Text>();
		cannonLevelLabel = GameObject.Find ("CannonLevel").GetComponent<Text>();
		upgradeCost = GameObject.Find ("UpgradeCost").GetComponent<Text>();
		Enemy.OnEnemiesChange += OnEnemiesChange;
		Enemy.OnEnemyKilled += OnEnemyKilled;
		startButton.onClick.AddListener(OnStartGameClicked);
		restartButton.onClick.AddListener(OnReStartGameClicked);
		endScreen.SetActive(false);
		upgradeButton.onClick.AddListener(UpgradeWeapon);
		InitCannon();
	}

	// Update is called once per frame
	void Update() {
		moneyLabel.text = string.Format("${0}", this.money.ToString());
		if (cannonLevel < Cannons.cannonList.Length - 1)
			upgradeCost.text = string.Format("UPGRADE COST:${0}", this.nextUpgradeCost());
		else {
			upgradeCost.text = "MAXED!";
			upgradeButton.interactable = false;
		}

		cannonLevelLabel.text = string.Format("CANNON LEVEL: {0}", cannonLevel.ToString());
	}

	void UpgradeWeapon() {
		money -= nextUpgradeCost();
		cannonLevel++;
		CannonProperties newCannonProperties = Cannons.getCannonProperties(cannonLevel);
		if (cannonObj.GetComponent<Cannon>().CannonProperties.cannonPrefab != newCannonProperties.cannonPrefab) {
			InitCannon();
		} else {
			cannonObj.GetComponent<Cannon>().CannonProperties = newCannonProperties;
		}
	}

	void InitCannon() {
		CannonProperties cannonProperties = Cannons.getCannonProperties(cannonLevel);
		if (cannonObj != null) {
			GameObject newcannonObj = (GameObject)GameObject.Instantiate(Resources.Load(string.Format("Prefabs/{0}", cannonProperties.cannonPrefab)));

			newcannonObj.GetComponent<Cannon>().CannonProperties = cannonProperties;

			initiateStylishCannonSwitch(cannonObj.GetComponent<Cannon>().transform, newcannonObj.transform);
			cannonObj.GetComponent<Cannon>().Kill ();
			cannonObj = newcannonObj;
		} else {
			cannonObj = (GameObject)GameObject.Instantiate(Resources.Load(string.Format("Prefabs/{0}", cannonProperties.cannonPrefab)));
			cannonObj.GetComponent<Cannon>().CannonProperties = cannonProperties;
		}
	}


	public void OnEnemiesChange(int count) {
		float ratio = Mathf.Min((float)count / MAX_CREATURES, 1);
		creaturesCount.DOScaleY(ratio, 0.1f);
		Debug.LogFormat("Enemies Count {0} ", count);
		//check if breaking windshield
		if (breakSteps.Count > 0 && count > breakSteps.Peek()) {
			glass.Damage++;
			breakSteps.Dequeue();
		}

		if (count >= MAX_CREATURES) {
			OnGameOver();
		}
	}

	public void OnEnemyKilled(Enemy enemy) {
		this.money += enemy.Reward * 10;
	}

	public void OnGameOver() {
		endScreen.SetActive(true);
		CanvasGroup canvasGroup =  endScreen.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		canvasGroup.DOFade(1,0.5f);
		Debug.LogFormat("GameOver");
		emittersControl.StopEmitting();
	}

	public void StartGame() {
		glass.ResetGlass();
		startScreen.SetActive(false);
		endScreen.SetActive(false);
		emittersControl.StartEmitting();
	}

	private int nextUpgradeCost() {
		return Cannons.cannonList[this.cannonLevel + 1].cost;
	}

	private void initiateStylishCannonSwitch(Transform oldCannon, Transform newCannon) {
		newCannon.position = new Vector3(0.0f, -8.0f, 0.0f);
		oldCannon.DOLocalMoveY (-8.5f, 0.5f).OnComplete (() => {
			newCannon.DOLocalMoveY (-4.25f, 0.5f);
		});

	}



	public void CreateBreakSteps() {
		breakSteps = new Queue<int>();
		breakSteps.Enqueue(3);
		breakSteps.Enqueue(6);
		breakSteps.Enqueue(9);
		breakSteps.Enqueue(12);
	}

	public void OnStartGameClicked() {
		CanvasGroup canvasGroup =  startScreen.GetComponent<CanvasGroup>();
		canvasGroup.DOFade(0,0.5f).OnComplete(StartGame);
	}

	public void OnReStartGameClicked() {
		CanvasGroup canvasGroup =  endScreen.GetComponent<CanvasGroup>();
		canvasGroup.DOFade(0,0.5f).OnComplete(StartGame);
	}
}
