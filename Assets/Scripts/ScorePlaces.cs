using UnityEngine;
using TMPro;
public class ScorePlaces : MonoBehaviour
{
    private TextMeshPro multiplier;

    private int a;

    void Start()
    {
        multiplier = GetComponentInChildren<TextMeshPro>();

        GetRandomScore();
    }

    private void GetRandomScore()
    {
        a = Random.Range(1, 9);
        multiplier.text = "X" + a.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IFinishPart>().Finished(other.gameObject);
    }
}
