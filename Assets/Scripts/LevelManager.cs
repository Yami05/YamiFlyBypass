using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject[] level;
    [SerializeField] private List<GameObject> AI = new List<GameObject>();
    private void Awake()
    {

        level = Resources.LoadAll<LevelScriptableObject>("Levels");
        Instantiate(level[PlayerPrefs.GetInt(Utilities.LevelIndex) % level.Length].LevelPrefab);
        
        for (int i = 0; i < level[PlayerPrefs.GetInt(Utilities.LevelIndex) % level.Length].AISpawnCount; i++)
        {
            AI[i].gameObject.SetActive(true);
        }
    }
}
