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

    public GameObject enemySmallRat;
    public GameObject enemyBigRat;
    public GameObject enemyHando;
    public GameObject enemySlick;
    public GameObject enemyFred;

    public RoundData[] rounds;
    private double roundTime = 0.0;
    private bool roundIsActive = true;

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

    private void SpawnEnemy(RoundData data, Vector2 dir)
    {
        RoundData.Enemy enemyType = data.nextEnemiesToSpawn[data.intervalIndex];
        GameObject enemy = null;
        switch (enemyType)
        {
            case RoundData.Enemy.SmallRat:
                enemy = Instantiate(enemySmallRat, dir, new Quaternion());
                Debug.Log("Spawning Small Rat.");
                break;
            case RoundData.Enemy.BigRat:
                enemy = Instantiate(enemyBigRat, dir, new Quaternion());
                Debug.Log("Spawning Big Rat.");
                break;
            case RoundData.Enemy.Hando:
                enemy = Instantiate(enemyHando, dir, new Quaternion());
                Debug.Log("Spawning Hando.");
                break;
            case RoundData.Enemy.Slick:
                enemy = Instantiate(enemySlick, dir, new Quaternion());
                Debug.Log("Spawning Slick.");
                break;
            case RoundData.Enemy.Fred:
                enemy = Instantiate(enemyFred, dir, new Quaternion());
                Debug.Log("Spawning Fred.");
                break;
        }
        AIDestinationSetter destSetter =
            enemy.GetComponent<AIDestinationSetter>();
        destSetter.target = target;
    }

    private void SpawnGroup(RoundData data, int intervalIndex)
    {
        int numEnemiesToSpawn = data.enemyGroupCounts[intervalIndex];
        RoundData.Direction dir = data.nextSpawnDirections[intervalIndex];
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            switch (dir)
            {
                case RoundData.Direction.North:
                    SpawnEnemy(data, NorthDir());
                    break;
                case RoundData.Direction.South:
                    SpawnEnemy(data, SouthDir());
                    break;
                case RoundData.Direction.East:
                    SpawnEnemy(data, EastDir());
                    break;
                case RoundData.Direction.West:
                    SpawnEnemy(data,WestDir());
                    break;
                case RoundData.Direction.Random:
                    SpawnEnemy(data, RandDir());
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
        if (0 < rounds.Length && currentRoundIndex < rounds.Length &&
            roundIsActive)
        {
            RoundData data = rounds[currentRoundIndex];
            totalNumEnemies += rounds[currentRoundIndex].enemyGroupCounts.Length;

            if (data.intervalIndex < data.numIntervals)
            {
                roundTime += Time.deltaTime;
                if (roundTime > data.spawnTimes[data.intervalIndex] ||
                    EnemyNumberConditionMet())
                {
                    SpawnGroup(data, data.intervalIndex);
                    data.intervalIndex++;
                }
            }
            else if (totalNumEnemies == 0)
            {
                rounds[currentRoundIndex].intervalIndex = 0;
                currentRoundIndex++;
                roundIsActive = false;
                roundTime = 0;
            }
        }
    }
}
