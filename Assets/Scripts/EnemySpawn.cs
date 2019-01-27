using UnityEngine;
using Pathfinding;

public class EnemySpawn : MonoBehaviour
{
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

    public GameObject enemySmallRat;
    public GameObject enemyBigRat;
    public GameObject enemyHando;
    public GameObject enemySlick;
    public GameObject enemyFred;

    public enum Enemy { SmallRat, BigRat, Hando, Slick, Fred };
    public Enemy[] nextEnemiesToSpawn;

    public bool isSpawning = true;

    private int currentIntervalIndex = 0;
    private int currentNumEnemiesOnScreen;
    private int totalNumEnemies;

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

    private void SpawnEnemy(Vector2 dir)
    {
        Enemy enemyType = nextEnemiesToSpawn[currentIntervalIndex];
        GameObject enemy = null;
        switch (enemyType)
        {
            case Enemy.SmallRat:
                enemy = Instantiate(enemySmallRat, dir, new Quaternion());
                break;
            case Enemy.BigRat:
                enemy = Instantiate(enemyBigRat, dir, new Quaternion());
                break;
            case Enemy.Hando:
                enemy = Instantiate(enemyHando, dir, new Quaternion());
                break;
            case Enemy.Slick:
                enemy = Instantiate(enemySlick, dir, new Quaternion());
                break;
            case Enemy.Fred:
                enemy = Instantiate(enemyFred, dir, new Quaternion());
                break;
        }
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

    private void SpawnGroup(Direction dir)
    {
        int numEnemiesToSpawn = enemyGroupCounts[currentIntervalIndex];
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            switch (dir)
            {
                case Direction.North:
                    SpawnEnemy(NorthDir());
                    break;
                case Direction.South:
                    SpawnEnemy(SouthDir());
                    break;
                case Direction.East:
                    SpawnEnemy(EastDir());
                    break;
                case Direction.West:
                    SpawnEnemy(WestDir());
                    break;
                case Direction.Random:
                    SpawnEnemy(RandDir());
                    break;
                default:
                    Debug.Log("Error: Unknown Direction");
                    break;
            }
            currentNumEnemiesOnScreen++;
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
                    SpawnGroup(nextDirectionsToSpawn[currentIntervalIndex]);
                }
                else
                {
                    Debug.Log("Error: Interval index out of bounds.");
                }
                currentIntervalIndex++;
            }
        }
    }
}
