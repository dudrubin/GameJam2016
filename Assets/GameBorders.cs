using UnityEngine;

public class GameBorders : MonoBehaviour {

	private static bool valuesSet = false;
	static float borderLeft;
	static float borderRight;
	static float borderTop;
	static float borderBottom;

	// Use this for initialization
	void Awake() {
		if (!valuesSet) {
			Bounds borders = GameObject.Find("Borders").GetComponent<BoxCollider2D>().bounds;
			borderLeft = borders.center.x - borders.extents.x;
			borderRight = borders.center.x + borders.extents.x;
			borderTop = borders.center.y - borders.extents.y;
			borderBottom = borders.center.y + borders.extents.y;
			valuesSet = true;
		}
	}

	public static bool IsOutHorizontal(Vector3 position) {
		return (position.x > borderRight || position.x < borderLeft);
	}

	public static bool IsOutVertical(Vector3 position) {
		return (position.y < borderTop || position.y > borderBottom);
	}

}
