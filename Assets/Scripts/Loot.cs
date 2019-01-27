using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	
	public float timeTilDestroyed = 5f;
	//public int ListSize;
	//private lootlist[ListSize];
	//touching loot? // loot collision

	void Start () {
	}
	void Update () {
	}
	public IEnumerator DestroyLoot(){
		yield return new WaitForSeconds (timeTilDestroyed);
		Destroy(gameObject);
	}
}
