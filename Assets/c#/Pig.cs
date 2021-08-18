using UnityEngine;
using System.Collections;

public class Pig : Unit {
	float vert;
	float hor;

	void Start () {
		GameManager.ins.player = this;
		thisTransform = transform;
		speed = 3f;
		animator = GetComponent<Animator>();
		coll = GetComponent<BoxCollider2D>();
		startCollX = coll.size.x;
		sr = GetComponent<SpriteRenderer>();
		SetStartPos ();
	}

	void Update () {
		if (vert != 0 || hor != 0)
			thisTransform.Translate (hor * speed * Time.deltaTime, vert * speed * Time.deltaTime, 0);
	}

	public override void MoveRight(){
		hor = 1;
		vert = 0;
		GoRight ();
		dir = DirEnum.goRight.ToString ();
	}

	public override void MoveLeft(){
		hor = -1;
		vert = 0;
		GoLeft ();
		dir = DirEnum.goLeft.ToString ();
	}

	public override void MoveTop(){
		vert = 1;
		hor = 0;
		GoUp ();
		dir = DirEnum.goUp.ToString ();
	}
	
	public override void MoveDown(){
		vert = -1;
		hor = 0;
		GoDown ();
		dir = DirEnum.goDown.ToString ();
	}

    public void Stop() {
        vert = 0;
        hor = 0;
    }

    public override void DestroyObj(){
		gameObject.SetActive (false);
		GameManager.ins.GameEnd ();
	}
}
