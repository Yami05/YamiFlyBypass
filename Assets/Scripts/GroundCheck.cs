using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<IBorder>()?.BorderEnter();
	}
	private void OnTriggerExit(Collider other)
	{
		other.GetComponent<IBorder>()?.BorderLeave();
	}
}
