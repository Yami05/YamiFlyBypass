using UnityEngine;

public class FinishLine : MonoBehaviour, IInteract
{
    public static FinishLine instance;

    private ScoreManager scoreManager;
    private GameEvents gameEvents;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameEvents = GameEvents.instance;
        scoreManager = ScoreManager.instance;
    }

    public void Interact(GameObject go)
    {
        scoreManager.GetPlace();

        if (go.GetComponent<PlayerController>())
        {
            gameEvents.OnFinish?.Invoke();
        }
    }

}
