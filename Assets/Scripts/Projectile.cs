using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	Vector2 velocity;
	private float lifeTime = 2;
	private float damage;
	// Use this for initialization
	void Start() {
		Invoke("Kill", lifeTime);
	}

	void Kill() {
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

}
