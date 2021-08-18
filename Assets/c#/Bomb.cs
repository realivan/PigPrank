using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bomb : MonoBehaviour {
	[SerializeField] public GameObject radiusImg;
	private float waitTime = 5f;
	Transform thisTransform;
	bool scaleUpBtn;
	bool scaleDownBtn;
	float scaleSpeed = 0.5f;
	SpriteRenderer sr;
	
	void Start () {
		thisTransform = transform;
		thisTransform.position = GameManager.ins.player.thisTransform.position;
		StartCoroutine (Explosion());
		scaleDownBtn = true;
		sr = radiusImg.GetComponent<SpriteRenderer>();
		thisTransform.DetachChildren ();
	}
	
	void Update () {
		sr.color = Color.Lerp(sr.color, new Color(1f,0,0,0.2f), Time.deltaTime/waitTime);
		ScaleBtn();
	}
	
	IEnumerator Explosion(){
		yield return new WaitForSeconds (waitTime);
		float radius = radiusImg.GetComponent<Renderer>().bounds.size.x*0.5f;
		ExplosionDamage(radius);
	}
	
	void ScaleBtn(){
		if (thisTransform.localScale.x > 1f && scaleDownBtn) {
			thisTransform.localScale = new Vector2 (
				thisTransform.localScale.x - scaleSpeed * Time.deltaTime,
				thisTransform.localScale.y - scaleSpeed * Time.deltaTime
				);
		} else if(scaleDownBtn){
			scaleDownBtn = false;
			scaleUpBtn = true;
		}
		if(thisTransform.localScale.x < 1.5f && scaleUpBtn){
			thisTransform.localScale = new Vector2 (
				thisTransform.localScale.x + scaleSpeed * Time.deltaTime,
				thisTransform.localScale.y + scaleSpeed * Time.deltaTime
				);
		} else if(scaleUpBtn){
			scaleDownBtn = true;
			scaleUpBtn = false;
		}
	}

	void ExplosionDamage(float radius){
		Destroy (this.gameObject);
		Destroy (radiusImg);
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(thisTransform.position, radius);
		foreach (Collider2D hitCollider in hitColliders){
			IGetDamage obj = hitCollider.GetComponent(typeof(IGetDamage)) as IGetDamage;
			if(obj != null)
				obj.GetDamage ();
		}
	}
}
