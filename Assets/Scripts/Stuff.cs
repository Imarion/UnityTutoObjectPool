using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject  {

	public Rigidbody Body { get; private set; }

	MeshRenderer[] meshRenderers;

	void Awake () {
		Body = GetComponent<Rigidbody>();
		//DontDestroyOnLoad (this);
		meshRenderers = GetComponentsInChildren<MeshRenderer>();
	}

	/*
	void OnLevelWasLoaded()
	{
		ReturnToPool ();
	}
	*/

	void OnTriggerEnter (Collider enteredCollider) {
		if (enteredCollider.CompareTag("KillZone")) {
			//Destroy(gameObject);
			ReturnToPool();
		}
	}

	public void SetMaterial (Material m) {
		for (int i = 0; i < meshRenderers.Length; i++) {
			meshRenderers[i].material = m;
		}
	}

	public void OnDestroy()
	{
		Debug.Log ("Destroying PooledObject");
	}

}
