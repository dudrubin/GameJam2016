using System.Collections.Generic;
using UnityEngine;

public class WindSheild : MonoBehaviour {

	int damage = -1;
	List<GameObject> glassImages = new List<GameObject>();

	public int Damage {
		get {
			return damage;
		}
		set {
			damage = value;
			SetImages();
		}
	}

	// Use this for initialization
	void Awake() {
		damage = -1;
		glassImages.Add(transform.FindChild("WINDSHIELD1").gameObject);
		glassImages.Add(transform.FindChild("WINDSHIELD2").gameObject);
		glassImages.Add(transform.FindChild("WINDSHIELD3").gameObject);
		glassImages.Add(transform.FindChild("WINDSHIELD4").gameObject);
		SetImages();
	}

	public void SetImages() {
		for (int i = 0; i < glassImages.Count; i++) {
			GameObject gameObject = glassImages[i];
			gameObject.SetActive(i == damage);
		}
	}

	public void ResetGlass() {
		damage = -1;
		SetImages();
	}
}
