using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Main : MonoBehaviour {

	const string SCORE_KEY = "BestScore";
	const int MAX_CREATURES = 13;
	int money = 300;
	int cannonLevel = 1;
	public List<Sprite> cannonSprites;
	Button upgradeButton;
	Text moneyLabel;
	Text upgradeCost;
	Text cannonLevelLabel;
	Transform creaturesCount;
	EmittersControl emittersControl;
	GameObject cannonObj;
	Queue<int> breakSteps;
	WindSheild glass;
	GameObject startScreen;
	GameObject endScreen;
	Button startButton;
	Button restartButton;
	DateTime timeStarted;


	Text textYourTime;
	Text textBestTime;

	// Use this for initialization
	void Awake() {
		CreateBreakSteps();
		if (!PlayerPrefs.HasKey(SCORE_KEY)) {
			PlayerPrefs.SetInt(SCORE_KEY, 0);
		}
		textYourTime = GameObject.Find("YourTime").GetComponent<Text>();
		textBestTime = GameObject.Find("BestTime").GetComponent<Text>();
		glass = GameObject.Find("Windshield").GetComponent<WindSheild>();
		startScreen = GameObject.Find("StartScreen");
		endScreen = GameObject.Find("EndScreen");
		startButton = GameObject.Find("StartButton").GetComponent<Button>();
		restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
		emittersControl = GameObject.Find("Emitters").GetComponent<EmittersControl>();
		creaturesCount = transform.FindChild("HUD/CreaturesCount");
		upgradeButton = GameObject.Find("UpgradeButton").GetComponent<Button>();
		moneyLabel = GameObject.Find("Money").GetComponent<Text>();

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
		if (cannonLevel < Cannons.cannonList.Length - 1) {
			upgradeButton.interactable = money > nextUpgradeCost();
		}
		else {
			upgradeButton.interactable = false;
		}

	}

	void UpgradeWeapon() {
		money -= nextUpgradeCost();
		cannonLevel++;
		CannonProperties newCannonProperties = Cannons.getCannonProperties(cannonLevel);
		if (cannonObj.GetComponent<Cannon>().GetCannonProperties().cannonType != newCannonProperties.cannonType) {
			InitCannon();
		} else {
			cannonObj.GetComponent<Cannon>().SetCannonProperties(newCannonProperties);
		}

		switch (newCannonProperties.cannonType) {
			case 1:
				upgradeButton.GetComponent<Image>().sprite = cannonSprites[0];
			break;
			case 2:
				upgradeButton.GetComponent<Image>().sprite = cannonSprites[1];
			break;
			case 3:
				upgradeButton.GetComponent<Image>().sprite = cannonSprites[2];
			break;
		}

	}

	void InitCannon() {
		CannonProperties cannonProperties = Cannons.getCannonProperties(cannonLevel);
		if (cannonObj != null) {
			GameObject newcannonObj = (GameObject)GameObject.Instantiate(Resources.Load(string.Format("Prefabs/{0}", Cannons.getPrefabName(cannonProperties.cannonType))));

			newcannonObj.GetComponent<Cannon>().SetCannonProperties(cannonProperties);

			initiateStylishCannonSwitch(cannonObj.GetComponent<Cannon>().transform, newcannonObj.transform);
			cannonObj.GetComponent<Cannon>().Kill();
			cannonObj = newcannonObj;
		} else {
			cannonObj = (GameObject)GameObject.Instantiate(Resources.Load(string.Format("Prefabs/{0}", Cannons.getPrefabName(cannonProperties.cannonType))));
			cannonObj.GetComponent<Cannon>().SetCannonProperties(cannonProperties);
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
		Enemy.OnEnemiesChange -= OnEnemiesChange;
		TimeSpan yourTime = DateTime.Now.Subtract(timeStarted);
		textYourTime.text =  string.Format("{0:D2}:{1:D2}",yourTime.Minutes,yourTime.Seconds);

		int bestScore = PlayerPrefs.GetInt(SCORE_KEY);
		int yourScore = (int) yourTime.TotalMilliseconds;

		if (yourScore > bestScore ) {
			PlayerPrefs.SetInt(SCORE_KEY, yourScore);
			bestScore = yourScore;
		}

		TimeSpan best = TimeSpan.FromMilliseconds(bestScore);
		textBestTime.text = string.Format("{0:D2}:{1:D2}",best.Minutes,best.Seconds);
		endScreen.SetActive(true);
		CanvasGroup canvasGroup = endScreen.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		canvasGroup.DOFade(1, 0.5f);
		Debug.LogFormat("GameOver");
		Enemy.KillAll();
		emittersControl.StopEmitting();
	}

	public void StartGame() {

		Enemy.OnEnemiesChange += OnEnemiesChange;
		WaveGenerator.Init();
		this.cannonLevel = 1;
		this.money = 0;
		glass.Damage = 0;

		timeStarted = DateTime.Now;
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
		oldCannon.DOLocalMoveY(-8.5f, 0.5f).OnComplete(() => {
			newCannon.DOLocalMoveY(-4.25f, 0.5f);
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
		CanvasGroup canvasGroup = startScreen.GetComponent<CanvasGroup>();
		canvasGroup.DOFade(0, 0.5f).OnComplete(StartGame);
	}

	public void OnReStartGameClicked() {
		CanvasGroup canvasGroup = endScreen.GetComponent<CanvasGroup>();
		canvasGroup.DOFade(0, 0.5f).OnComplete(StartGame);
	}
}
