using UnityEngine;

public class Turn : MonoBehaviour
{
	[SerializeField] Direction direction;
    
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IReturn>(out var @return))
		{
			@return.Return(direction);
		}
	}
}
