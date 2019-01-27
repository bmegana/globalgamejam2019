﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {


    [SerializeField] GameObject[] drops;

    public void Death() {
        print(string.Format("{0} is dying", name));
        if (drops.Length > 0)
        {
            GameObject drop = drops[Random.Range(0, drops.Length)];
			GameObject loot = Instantiate(drop, transform.position, transform.rotation);
			Loot lootScript = loot.GetComponent<Loot> (); 
			lootScript.StartCoroutine (lootScript.DestroyLoot());
        }  
		Destroy(gameObject);
    }


}
