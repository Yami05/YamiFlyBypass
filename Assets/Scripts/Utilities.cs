using UnityEngine;

public enum Fly
{
    OnAir,
    Ground,
}

public enum WingCond_
{
    Rise,
    Fall,
}

public enum Direction
{
    Z,
    X,
}

public enum AIdir
{
    Z,
    X,
}

public class Utilities : MonoBehaviour
{
    public const string LevelIndex = "LevelIndex";
    public const string isFly = "isFlying";
    public const string isRun = "isRunning";
    public const string isDance = "isDance";

    public static void SetLevelPref(int levelCount = 1)
    {
        PlayerPrefs.SetInt(LevelIndex, PlayerPrefs.GetInt(LevelIndex, 1) + levelCount);

    }
}
