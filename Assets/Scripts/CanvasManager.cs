using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countPlace;

    private ScoreManager scoreManager;
    private GameEvents gameEvents;

    private void Start()
    {
        gameEvents = GameEvents.instance;
        scoreManager = ScoreManager.instance;

        gameEvents.OnFinish += () => { countPlace.gameObject.SetActive(true); countPlace.text = scoreManager.GetPlaceUI(); };
       
    }
}
