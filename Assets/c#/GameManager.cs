using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	[SerializeField] public BombBtn bombBtn;
	[SerializeField] public Transform startPos;
	[SerializeField] public Transform stonePrefab;
	[SerializeField] public Transform nodePrefab;
	[SerializeField] public Transform map;
	[SerializeField] public GameObject finishPanel;
	public ObjectPool pool;
	public Pig player;
	public static GameManager ins;
	int width = 17;
	int height = 9;
	float stepX = 1.1f;
	float stepY = 1f;
	float offsetX = 0.12f;
	int[] startPigPos = new int[2]{0, 4};
	List<int[]> startDogPos = new List<int[]>();


	void Awake(){
		if (ins == null)
			ins = this;
	}

	void Start () {
		pool.StartPool ();
		startDogPos.Add (new int[2]{12, 0});
		startDogPos.Add (new int[2]{13, 4});
		startDogPos.Add (new int[2]{15, 8});
		GetPath ();
	}

	void GetPath(){
		for(int i=0; i<width; i++){
			Node n = null;
			for(int j=0; j<height; j++){
				// добавляем точки на карту
				Transform t = Instantiate (nodePrefab) as Transform;
				t.SetParent (map);
				t.localPosition = new Vector2(
					startPos.localPosition.x + i*stepX - j*offsetX,
					startPos.localPosition.y - j*stepY
					);
				Node curNode = t.GetComponent<Node>();
				// вертикальные связи
				if(j != 0){
					n.path.Add (curNode);
					curNode.path.Add (n);
				}
				// горизонтальные связи
				if(i != 0){
					Node pastNode = map.GetChild (map.childCount-(height+1)).GetComponent<Node>();
					pastNode.path.Add (curNode);
					curNode.path.Add (pastNode);
				}
				n = curNode;
				n.x = i;
				n.y = j;
				if(startDogPos.Count != 0 && i == startDogPos[0][0] && j == startDogPos[0][1]){
					GameObject obj = pool.Spawn ("dog");
					obj.GetComponent<Dog>().startNode = n;
					n.free = false;
					startDogPos.RemoveAt(0);
				}
				if(i == startPigPos[0] && j == startPigPos[1]){
					GameObject obj = pool.Spawn ("pig");
					obj.GetComponent<Pig>().startNode = n;
				}
			}
		}
	}

	public Transform AddStone(){
		return Instantiate (stonePrefab) as Transform;
	}

	public void MoveRight(){
		player.MoveRight();
	}
	
	public void MoveLeft(){
		player.MoveLeft ();
	}
	
	public void MoveTop(){
		player.MoveTop ();
	}
	
	public void MoveDown(){
		player.MoveDown ();
	}

    public void Stop() {
        player.Stop();
    }

    public void PlantBomb(){
		bombBtn.PressBtn ();
		pool.Spawn ("bomb");
	}

	public void GameEnd(){
		finishPanel.SetActive (true);
	}

	public void RestartGame(){
        SceneManager.LoadScene(0);
    }
}
