using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	Vector2 velocity;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float deltaX =Time.deltaTime * velocity.x;
		float deltaY = Time.deltaTime * velocity.y;
		transform.position = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.z);
		//Debug.LogFormat ("posx:{0} posy:{1}", transform.position.x, transform.position.y);
		//Debug.LogFormat ("deltax:{0} deltay:{1}", deltaX, deltaY);
		//transform.Translate (new Vector2 (deltaX, deltaY));
	}

	public void SetVelocity(Vector2 velocity){
		this.velocity = velocity;
	}
}
