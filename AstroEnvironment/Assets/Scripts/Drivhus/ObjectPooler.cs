using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	#region Singelton
	public static ObjectPooler Instance;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Instance = this;
	}
	#endregion
	
	public List<Pool> pools;

	public Dictionary<string, Queue<GameObject>> poolDictionary;

	// Use this for initialization
	void Start () {
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach(Pool pool in pools){
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for(int i = 0; i < pool.size; i++){
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}

	}
	
	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation){
		if(! poolDictionary.ContainsKey(tag)){
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
			return null;
		}

		GameObject objectToSpawn = poolDictionary[tag].Dequeue();
		Debug.Log(objectToSpawn.ToString());
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

		if(pooledObject != null){
			pooledObject.onObjectSpawn();
		}

		poolDictionary[tag].Enqueue(objectToSpawn);

		return objectToSpawn;
	}
	
}
