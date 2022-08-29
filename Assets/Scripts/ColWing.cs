using UnityEngine;

public class ColWing : MonoBehaviour, IInteract
{
    public bool cango = false;

    public void Interact(GameObject gameObject)
    {
        cango = true;
        gameObject.transform.GetComponentInChildren<WingController>().WingAdd();
    }
}
