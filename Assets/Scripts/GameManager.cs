using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
   private GameEvents gameEvents;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    public void Retry()
    {

        //Make them util
        SceneManager.LoadScene("SampleScene");
    }
    private void Start()
    {
        gameEvents = GameEvents.instance;
    }

    public void StartGame()
    {
        gameEvents.OnStart?.Invoke();
    }

    public void NextLevel()
    {
        Utilities.SetLevelPref();
        SceneManager.LoadScene("SampleScene");
    }
}
