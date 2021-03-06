﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	PooledObject prefab;
	List<PooledObject> availableObjects = new List<PooledObject>();

	public PooledObject GetObject () {
		PooledObject obj; // = Instantiate<PooledObject>(prefab);
		int lastAvailableIndex = availableObjects.Count - 1;
		if (lastAvailableIndex >= 0) {
			obj = availableObjects[lastAvailableIndex];
			availableObjects.RemoveAt(lastAvailableIndex);
			obj.gameObject.SetActive(true);
		}
		else {
			obj = Instantiate<PooledObject>(prefab);
			obj.transform.SetParent(transform, false);
			obj.Pool = this;
		}
		return obj;
	}

	public void AddObject (PooledObject obj) {
		//Object.Destroy(o.gameObject) ;
		obj.gameObject.SetActive(false);
	    availableObjects.Add(obj);
	}

	public static ObjectPool GetPool (PooledObject prefab) {
		GameObject obj;  // = new GameObject(prefab.name + " Pool");
		ObjectPool pool; // = obj.AddComponent<ObjectPool>();

		if (Application.isEditor) {
			obj = GameObject.Find(prefab.name + " Pool");
			if (obj) {
				pool = obj.GetComponent<ObjectPool>();
				if (pool) {
					return pool;
				}
			}
		}

		obj = new GameObject(prefab.name + " Pool");
		DontDestroyOnLoad (obj);
		pool = obj.AddComponent<ObjectPool>();
		pool.prefab = prefab;
		return pool;
	}

	public void OnDestroy()
	{
		Debug.Log ("Destroying Pool");
	}
}
