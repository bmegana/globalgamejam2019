using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] int numRounds = 10;
    [SerializeField] EnemySpawn enemySpawn;

    PlayerInventory playerInventory;
    GameState gameState = GameState.BATTLE;
    int round = 1;


	// Use this for initialization
	void Start () {
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }
	
	// Update is called once per frame
	void Update () {
		switch (gameState) {
            case GameState.BATTLE:
                if (!enemySpawn.isSpawning) {
                    gameState = GameState.DESIGN;
                }
                break;
            case GameState.DESIGN:
                // when user clicks DONE DESIGN button or whatever, flip gameState to GameState.BATTLE
                round++;
                break;
        }
	}
}

enum GameState {
    BATTLE, DESIGN
}

