using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offsetPosition;

    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offsetPosition), 0.1f);
        transform.LookAt(target);

    }
}