using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]
public class RoundData : ScriptableObject
{
    public int intervalIndex;
    public int numIntervals;

    public int[] enemyGroupCounts;
    public float[] spawnTimes;
    public enum Direction { North, South, East, West, Random };
    public Direction[] nextSpawnDirections;
    public enum Enemy { SmallRat, BigRat, Hando, Slick, Fred, Boxer, Krown, Roach };
    public Enemy[] nextEnemiesToSpawn;
}
