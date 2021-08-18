using UnityEngine;
using System.Collections;
public class Dog : Unit {
	
	void Start () {
		this.thisTransform = transform;
		this.speed = 1f;
		this.animator = GetComponent<Animator>();
		SetStartPos ();
		this.curNode = startNode;
		coll = GetComponent<BoxCollider2D>();
		startCollX = coll.size.x;
		GetPoint ();
	}

	void  OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag ("Player")) {
			GameManager.ins.player.DestroyObj ();
		}
	}

	void Update () {
		if (this.targetPos != Vector3.zero)
			Move ();
	}
	
	public override void MoveRight(){
		GoRight ();
		this.dir = DirEnum.goRight.ToString ();
	}
	
	public override void MoveLeft(){
		GoLeft ();
		this.dir = DirEnum.goLeft.ToString ();
	}
	
	public override void MoveTop(){
		GoUp ();
		this.dir = DirEnum.goUp.ToString ();
	}
	
	public override void MoveDown(){
		GoDown ();
		this.dir = DirEnum.goDown.ToString ();
	}

	public override void DestroyObj(){
		gameObject.SetActive (false);
		curNode.free = true;
	}
}
