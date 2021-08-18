using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	
	[System.Serializable]
	public class Pool{
		public string tag;
		public int size;
		public GameObject prefab;
	}
	
	public Dictionary<string, Queue<GameObject>> poolDict;
	public List<Pool> pools;
	
	public void StartPool () {
		poolDict = new Dictionary<string, Queue<GameObject>> ();
		foreach(Pool p in pools){
			Queue<GameObject> queuePool = new Queue<GameObject>();
			for(int i=0; i<p.size; i++){
				GameObject obj = Instantiate (p.prefab) as GameObject;
				obj.SetActive (false);
				queuePool.Enqueue (obj);
			}
			poolDict.Add (p.tag, queuePool);
		}
	}
	
	public GameObject Spawn(string tag){
		if (!poolDict.ContainsKey (tag)) return null;
		GameObject objSpawn = poolDict [tag].Dequeue ();
		objSpawn.SetActive (true);
		poolDict [tag].Enqueue (objSpawn);
		return objSpawn;
	}
}
