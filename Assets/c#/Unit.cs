using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, IGetDamage {
	public Animator animator;
	public float speed;
	public Transform thisTransform;
	public string dir;
	public Vector3 targetPos = Vector3.zero;
	public Node startNode;
	public Node curNode;
	public Node targetNode;
	public BoxCollider2D coll;
	public float startCollX;
	public SpriteRenderer sr;

	public void SetStartPos(){
		thisTransform.position = startNode.thisTransform.position;
	}

	public void GetPoint(){
		StartCoroutine (FindWay ());
	}

	IEnumerator FindWay(){
		while(targetPos == Vector3.zero){
			yield return null;
			int i = Random.Range (0, curNode.path.Count);
			Node targetNode = curNode.path [i];
			if(targetNode.free){
				curNode.free = true;
				targetNode.free = false;
				curNode = targetNode;
				targetNode = null;
				targetPos = curNode.thisTransform.position;
				Vector2 t = (targetPos - thisTransform.position).normalized;
				if(t == Vector2.right)	this.MoveRight();
				else if(t == -Vector2.right) this.MoveLeft();
				else if(t[1] > 0.9f) this.MoveTop();
				else if(t[1] < -0.9) this.MoveDown ();
			}
		}
	}

	public void GoRight(){
		if(dir != string.Empty && dir != DirEnum.goRight.ToString ())
			animator.SetBool (dir, false);
		animator.SetBool (DirEnum.goRight.ToString (), true);
		UpdateCollider (2);
	}

	public void GoLeft(){
		if(dir != string.Empty && dir != DirEnum.goLeft.ToString ())
			animator.SetBool (dir, false);
		animator.SetBool (DirEnum.goLeft.ToString (), true);
		UpdateCollider (2);
	}

	public void GoUp(){
		if(dir != string.Empty && dir != DirEnum.goUp.ToString ())
			animator.SetBool (dir, false);
		animator.SetBool (DirEnum.goUp.ToString (), true);
		UpdateCollider (0.5f);
	}

	public void GoDown(){
		if(dir != string.Empty && dir != DirEnum.goDown.ToString ())
			animator.SetBool (dir, false);
		animator.SetBool (DirEnum.goDown.ToString (), true);
		UpdateCollider (0.5f);
	}

	public void UpdateCollider(float i){
		if(startCollX == coll.size.x && i<2)
			coll.size = new Vector2(coll.size.x*i, coll.size.y);
		else if (startCollX > coll.size.x && i==2)
			coll.size = new Vector2(coll.size.x*i, coll.size.y);
	}

	public void Move(){
		thisTransform.position = Vector3.MoveTowards (
			thisTransform.position,
			targetPos,
			speed*Time.deltaTime);
		if (thisTransform.position == targetPos) {
			targetPos = Vector3.zero;
			GetPoint ();
		}
	}

	public void GetDamage(){
		this.DestroyObj ();
	}

	public virtual void MoveRight(){}
	public virtual void MoveLeft(){}
	public virtual void MoveTop(){}
	public virtual void MoveDown(){}
	public virtual void DestroyObj(){}
}
