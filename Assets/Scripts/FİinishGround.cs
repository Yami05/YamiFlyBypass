using UnityEngine;

public class FİinishGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IFinishPart>().Finished(other.gameObject);
    }
}
