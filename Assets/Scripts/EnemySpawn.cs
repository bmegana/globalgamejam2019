using UnityEngine;
using Pathfinding;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;

    public float xSpawnFromOrigin;
    public float maxYOffset;
    public float ySpawnFromOrigin;
    public float maxXOffset;

    public bool spawnWhenUnderMinimum;
    public int minNumEnemiesOnScreen;
    public bool spawnWhenBelowMaximum;
    public int maxNumEnemiesOnScreen;

    public int[] enemyGroupCounts;
    public float[] spawnTimes;
    public enum Direction { North, South, East, West, Random };
    public Direction[] nextDirectionsToSpawn;

    private int currentIntervalIndex = 0;
    public int currentNumEnemiesOnScreen;
    public int totalNumEnemies;

    private Vector2 NorthDir()
    {
        return new Vector2(Random.Range(-maxXOffset, maxXOffset), ySpawnFromOrigin);
    }

    private Vector2 SouthDir()
    {
        return new Vector2(Random.Range(-maxXOffset, maxXOffset), -ySpawnFromOrigin);
    }

    private Vector2 EastDir()
    {
        return new Vector2(xSpawnFromOrigin, Random.Range(-maxYOffset, maxYOffset));
    }

    private Vector2 WestDir()
    {
        return new Vector2(-xSpawnFromOrigin, Random.Range(-maxYOffset, maxYOffset));
    }

    private Vector2 RandDir()
    {
        int areaOption = Random.Range(0, 4);
        const int NORTH = 0;
        const int SOUTH = 1;
        const int EAST = 2;

        if (areaOption == NORTH)
        {
            return NorthDir();
        }
        else if (areaOption == SOUTH)
        {
            return SouthDir();
        }
        else if (areaOption == EAST)
        {
            return EastDir();
        }
        else
        {
            return WestDir();
        }
    }

    private void SetEnemyVariables(GameObject enemy)
    {
        AIDestinationSetter destSetter =
            enemy.GetComponent<AIDestinationSetter>();
        destSetter.target = target;
    }

    private void Start()
    {
        for (int i = 0; i < enemyGroupCounts.Length; i++)
        {
            totalNumEnemies += enemyGroupCounts[i];
        }
    }

    private void SpawnGroup(Vector2 dir)
    {
        int numEnemiesToSpawn = enemyGroupCounts[currentIntervalIndex];
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, dir, new Quaternion());
            SetEnemyVariables(enemy);
            currentNumEnemiesOnScreen++;
        }
    }

    private void SpawnGroupAtDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                SpawnGroup(NorthDir());
                break;
            case Direction.South:
                SpawnGroup(SouthDir());
                break;
            case Direction.East:
                SpawnGroup(EastDir());
                break;
            case Direction.West:
                SpawnGroup(WestDir());
                break;
            case Direction.Random:
                SpawnGroup(RandDir());
                break;
            default:
                Debug.Log("Error: Unknown Direction");
                break;
        }
    }

    private bool EnemyNumberConditionMet()
    {
        return (spawnWhenUnderMinimum &&
            currentNumEnemiesOnScreen <= minNumEnemiesOnScreen) ||
            (spawnWhenBelowMaximum &&
            currentNumEnemiesOnScreen < maxNumEnemiesOnScreen);
    }

    private void Update()
    {
        if (0 < spawnTimes.Length &&
            currentIntervalIndex < spawnTimes.Length)
        {
            if (Time.timeSinceLevelLoad > spawnTimes[currentIntervalIndex] ||
                EnemyNumberConditionMet())
            {
                if (0 < nextDirectionsToSpawn.Length &&
                    currentIntervalIndex < nextDirectionsToSpawn.Length)
                {
                    SpawnGroupAtDirection(nextDirectionsToSpawn[currentIntervalIndex]);
                }
                else
                {
                    GameObject enemy = Instantiate(
                        enemyPrefab,
                        RandDir(),
                        new Quaternion()
                    );
                    SetEnemyVariables(enemy);
                }
                currentIntervalIndex++;
            }
        }
    }
}
