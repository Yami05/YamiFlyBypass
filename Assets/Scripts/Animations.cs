using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private GameEvents gameEvents;

    private void Start()
    {
        gameEvents = GameEvents.instance;
        gameEvents.OnStart += () => Run();
    }

    public void Run()
    {
        animator.SetBool(Utilities.isRun, true);
        animator.SetBool(Utilities.isFly, false);
    }

    public void Fly()
    {
        animator.SetBool(Utilities.isFly, true);
    }

    public void Dance()
    {
        animator.SetBool(Utilities.isDance, true);
    }
}
