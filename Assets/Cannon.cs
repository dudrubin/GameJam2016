using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class Cannon : MonoBehaviour {

	protected BaseCannonMovement movementHandler;
	protected BaseCannonFire fireHandler;
	protected CannonProperties cannonProperties;

	private float lifetime = 1.0f;
	private bool mouseDown = false;
	// Use this for initialization
	void Start () {
		Init ();
	}

	// Update is called once per frame
	void Update () {
		if (!EventSystem.current.IsPointerOverGameObject())
			RespondToInput ();
	}

	public void Kill(){
		Destroy (this.gameObject, lifetime);
	}

	protected virtual void Init(){
		GameObject barrelRef = (GameObject)transform.Find("Base/BarrelBase/Barrel").gameObject;
		Vector3 pivot = transform.FindChild ("Base/BarrelBase").position;
		transform.position = new Vector3 { x = 0.0f, y = -4.25f, z = 0.0f };

		movementHandler = new BaseCannonMovement (new Transform[] { barrelRef.transform},pivot, this.GetCannonProperties());
		fireHandler = new BaseCannonFire (new Transform[] { barrelRef.transform }, this.GetCannonProperties());
	}

	protected virtual void RespondToInput(){
		Vector2 pos;
		if (!getTouchPos(out pos))
			return;
		movementHandler.RespondToInput (new Vector2[]{ pos }, () => {
			fireHandler.RespondToInput (new Vector2[]{ pos });
		});
	}

	protected bool getTouchPos(out Vector2 touchPos){
		touchPos = new Vector2();
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Moved || 
				Input.GetTouch (i).phase == TouchPhase.Began ||
				Input.GetTouch (i).phase == TouchPhase.Stationary) {
				touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch (i).position);
				Debug.LogFormat ("touch detected {0},{1}", touchPos.x, touchPos.y);
				return true;
			}
		}
		updateMouseStatus ();
	
		if (mouseDown){
			touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
			Debug.LogFormat ("click detected {0},{1}", touchPos.x, touchPos.y);
			return true;
		}

		return false;
	}

	private void updateMouseStatus(){
		if (Input.GetMouseButtonDown(0))
			mouseDown = true;
		
		if (Input.GetMouseButtonUp(0))
			mouseDown = false;
	}

	public virtual void InitiateStylishExit(){
		transform.DOLocalMoveY (-6, 1);
	}

	public virtual void InitiateStylishEnterance(){
		transform.DOLocalMoveY (0, 1);
	}

	public void  SetCannonProperties(CannonProperties p){
		this.cannonProperties = p;
		if (this.fireHandler != null)
			this.fireHandler.CannonProperties = p;
		if (this.movementHandler != null)
			this.movementHandler.CannonProperties = p;
	}

	public CannonProperties GetCannonProperties(){
		return this.cannonProperties;
	}
}
