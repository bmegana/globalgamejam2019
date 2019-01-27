using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public List<GameObject> inventory = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col) {
        switch (col.gameObject.tag) {
            case "Loot":
                print("collided w/ loot");
                inventory.Add(col.gameObject);
                Destroy(col.gameObject);
                print(inventory.Count);
                break;
        }
    }
}
