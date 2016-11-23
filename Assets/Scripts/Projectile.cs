using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	Vector2 velocity;
	private float lifeTime = 2;
	private float damage;
	private static Object xplosion;
	// Use this for initialization
	void Start() {

		if (xplosion == null) {
			xplosion= Resources.Load("Prefabs/enemyXplostion");
		}
		Invoke("Kill", lifeTime);
	}

	void Kill() {
		GameObject s = Instantiate(xplosion) as GameObject;
		s.transform.SetParent(transform.parent);
		s.transform.localPosition = transform.localPosition;
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update() {
		float deltaX = Time.deltaTime * velocity.x;
		float deltaY = Time.deltaTime * velocity.y;
		transform.position = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.z);
		//Debug.Log(GetComponent<BoxCollider2D>().IsTouchingLayers());

	}

	public void SetVelocity(Vector2 velocity) {
		this.velocity = velocity;
	}

	public void SetDamage(float damage) {
		this.damage = damage;
	}



	/// <summary>
	///
	/// </summary>
	/// <param name="collision"></param>
	void OnTriggerEnter2D(Collider2D collision) {

		Debug.LogFormat("AAA" + collision.gameObject.name);
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();

		if (enemy != null) {
			enemy.Hit (damage);	
		}

		Kill();
	}

	public static string getPrefabName(int projectileType){
		switch (projectileType) {
		case 1:
			return "Projectile1";
			break;
		case 2:
			return "Projectile2";
			break;
		case 3:
			return "Projectile3";
			break;
		}
		return null;
	}

}
