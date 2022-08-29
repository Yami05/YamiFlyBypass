using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject gameOverPanel;

    private GameEvents gameEvents;

    private void Start()
    {
        gameEvents = GameEvents.instance;

        gameEvents.OnStart += () => startPanel.SetActive(false);
        gameEvents.OnEnd += () => endPanel.SetActive(true);
        gameEvents.GameOver += () => gameOverPanel.SetActive(true);
    }
}
