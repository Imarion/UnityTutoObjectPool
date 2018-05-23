using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

	public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;
	public float velocity;
	public Material stuffMaterial;

	public Stuff[] stuffPrefabs;

	float timeSinceLastSpawn;
	float currentSpawnDelay;

	void FixedUpdate () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= currentSpawnDelay) {
			timeSinceLastSpawn -= currentSpawnDelay;
			currentSpawnDelay = timeBetweenSpawns.RandomInRange;
			SpawnStuff();
		}
	}

	void SpawnStuff () {
		Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
		//Stuff spawn = Instantiate<Stuff>(prefab);
		Stuff spawn = prefab.GetPooledInstance<Stuff>();
		//spawn.GetComponent<MeshRenderer>().material = stuffMaterial;
		spawn.SetMaterial(stuffMaterial);
		spawn.Body.velocity = transform.up * velocity + Random.onUnitSphere * randomVelocity.RandomInRange;
		spawn.Body.angularVelocity = Random.onUnitSphere * angularVelocity.RandomInRange;
		spawn.transform.localPosition = transform.position;		
		spawn.transform.localScale = Vector3.one * scale.RandomInRange;
		spawn.transform.localRotation = Random.rotation;
	}

}
