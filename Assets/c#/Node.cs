using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public Transform thisTransform;
	public int x;
	public int y;
	public List<Node> path;
	public bool free;

    private void Awake() {
        thisTransform = transform;
    }

    void Start () {		
		// ставим камни
		if(x%2 != 0 && y%2 != 0){
			Transform stone = GameManager.ins.AddStone();
			stone.SetParent (transform);
			Stone s = stone.GetComponent<Stone>();
			s.layer = y;
			s.curNode = this;
			free = false;
		}
	}
}
