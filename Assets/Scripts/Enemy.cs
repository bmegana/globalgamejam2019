using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject[] drops;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Death() {
        print(string.Format("{0} is dying", name));
        GameObject drop = drops[Random.Range(0, drops.Length)];
        Instantiate(drop, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
