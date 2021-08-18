using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour, IGetDamage {
	public Node curNode;
	public int layer;
	SpriteRenderer sr;
	public Transform thisTransform;

	void Start () {
		thisTransform = transform;
		sr = GetComponent<SpriteRenderer>();
		sr.sortingOrder = layer;
		transform.localPosition = Vector2.zero;
	}

	public void GetDamage(){
		gameObject.SetActive (false);
		curNode.free = true;
	}
}
