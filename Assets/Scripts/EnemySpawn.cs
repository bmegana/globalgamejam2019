using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

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
    public GameObject enemyBoxer;
    public GameObject enemyKrown;
    public GameObject enemyRoach;

    public RoundData[] rounds;
    public double roundTime = 0.0;
    public bool roundIsActive = true;
    private RoundData data;
    private bool newRoundSet = false;

    private bool musicIsPlaying = false;

    public int numEnemiesDead;
    public int totalEnemiesInCurrRound;
    private bool numEnemiesInRoundSet = false;

    public int currentRoundIndex = 0;
    private int currentNumEnemiesOnScreen;

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
            case RoundData.Enemy.Boxer:
                enemy = Instantiate(enemyBoxer, dir, new Quaternion());
                break;
            case RoundData.Enemy.Roach:
                enemy = Instantiate(enemyRoach, dir, new Quaternion());
                break;
            case RoundData.Enemy.Krown:
                enemy = Instantiate(enemyKrown, dir, new Quaternion());
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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        SoundPlayer.instance.PlayTrackOne();
    }

    public void ActivateNextRound()
    {
        roundIsActive = true;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (0 < rounds.Length && currentRoundIndex < rounds.Length &&
            roundIsActive)
        {
            if (!newRoundSet)
            {
                data = rounds[currentRoundIndex];
                newRoundSet = true;
            }
            if (!numEnemiesInRoundSet)
            {
                int[] groupCounts = rounds[currentRoundIndex].enemyGroupCounts;
                for (int i = 0; i < groupCounts.Length; i++)
                {
                    totalEnemiesInCurrRound += groupCounts[i];
                }
                numEnemiesInRoundSet = true;
            }

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
            else if (numEnemiesDead == totalEnemiesInCurrRound)
            {
                rounds[currentRoundIndex].intervalIndex = 0;
                currentRoundIndex++;
                roundIsActive = false;
                newRoundSet = false;
                roundTime = 0;
                numEnemiesInRoundSet = false;
                musicIsPlaying = false;
                Time.timeScale = 0;
                DecorationManager.instance.ActivateDecoratePanel();
            }
        }
        else if (currentRoundIndex >= rounds.Length)
        {
			Time.timeScale = 1;
			SceneManager.LoadScene("Compass");
        }
    }
}
