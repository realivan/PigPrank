using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BombBtn : MonoBehaviour {
	[SerializeField] public Image hideImage;
	private float waitTime = 2f;
	private Button btn;
	private bool wait;
	Transform thisTransform;
	bool scaleUpBtn;
	bool scaleDownBtn;
	float scaleSpeed = 0.5f;

	void Start () {
		thisTransform = transform;
		btn = GetComponent<Button>();
		scaleDownBtn = true;
	}

	void Update () {
		if (wait) {
			hideImage.fillAmount -= Time.deltaTime / waitTime;
			if(hideImage.fillAmount == 0){
				wait = false;
				btn.interactable = true;
				if (thisTransform.localScale.x > 0.5f) {
					scaleDownBtn = true;
				}else scaleUpBtn = true;
			}
		}
		if (scaleDownBtn || scaleUpBtn) {
			ScaleBtn();
		}
	}

	public void PressBtn(){
		if (!wait) {
			btn.interactable = false;
			hideImage.fillAmount = 1;
			wait = true;
			scaleDownBtn = false;
			scaleUpBtn = false;
		}
	}

	void ScaleBtn(){
		if (thisTransform.localScale.x > 0.7f && scaleDownBtn) {
			thisTransform.localScale = new Vector2 (
			thisTransform.localScale.x - scaleSpeed * Time.deltaTime,
			thisTransform.localScale.y - scaleSpeed * Time.deltaTime
			);
		} else if(scaleDownBtn){
			scaleDownBtn = false;
			scaleUpBtn = true;
		}
		if(thisTransform.localScale.x < 1f && scaleUpBtn){
			thisTransform.localScale = new Vector2 (
				thisTransform.localScale.x + scaleSpeed * Time.deltaTime,
				thisTransform.localScale.y + scaleSpeed * Time.deltaTime
				);
		} else if(scaleUpBtn){
			scaleDownBtn = true;
			scaleUpBtn = false;
		}
	}
}
