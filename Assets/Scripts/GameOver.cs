using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        other.GetComponent<IGameOver>()?.GameOver(other.gameObject);
    }
}
