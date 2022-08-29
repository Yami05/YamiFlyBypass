using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelProgress : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image fillImage;

    private FinishLine finishLine;
    private GameEvents gameEvents;

    private float startDis;

    void Start()
    {
        gameEvents = GameEvents.instance;
        finishLine = FinishLine.instance;

        levelText.text = "Lv." + PlayerPrefs.GetInt(Utilities.LevelIndex).ToString();
        startDis = Vector3.Distance(player.transform.position, finishLine.transform.position);

        gameEvents.OnFinish += () => gameObject.SetActive(false);
    }

    private void Update()
    {
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        float dis = Vector3.Distance(finishLine.transform.position, player.transform.position);
        fillImage.fillAmount = dis / startDis;
    }
}
