using UnityEngine;


[CreateAssetMenu(menuName = "Levels")]

public class LevelScriptableObject : ScriptableObject
{
    public GameObject LevelPrefab;
    public int AISpawnCount;
}
