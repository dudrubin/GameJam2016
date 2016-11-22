using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	Vector2 velocity;
	public int Damage = 100;
	private float lifeTime = 2;

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
		//Debug.LogFormat ("posx:{0} posy:{1}", transform.position.x, transform.position.y);
		//Debug.LogFormat ("deltax:{0} deltay:{1}", deltaX, deltaY);
		//transform.Translate (new Vector2 (deltaX, deltaY));
		//Debug.Log(GetComponent<BoxCollider2D>().IsTouchingLayers());
	}

	public void SetVelocity(Vector2 velocity) {
		this.velocity = velocity;
	}

	/// <summary>
	///
	/// </summary>
	/// <param name="collision"></param>
	void OnTriggerEnter2D(Collider2D collision) {

		Debug.LogFormat("AAA" + collision.gameObject.name);
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();

		if (enemy != null) {
			enemy.Health -= Damage;
		}

		Kill();
	}

}
