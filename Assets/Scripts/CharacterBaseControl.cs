using System.Collections;
using UnityEngine;
using Unity.Jobs;
using System.Collections.Generic;

//public struct BuildJob : IJobParallelFor
//{
//    public void Execute(int index)
//    {
//        throw new System.NotImplementedException();
//    }
//}


//public class BuildingManager : MonoBehaviour
//{
//    [SerializeField] private List<CharacterBaseControl> cbc;


//    private void Update()
//    {
//        var job = new BuildJob();
//        var jobHandle = job.Schedule(cbc.Count, 1);
//        jobHandle.Complete();

//    }
//}

public class CharacterBaseControl : MonoBehaviour, IBorder, IFinishPart, IGameOver
{

    [SerializeField] protected float speed;

    public Fly fly;

    protected Animations animations;
    protected Rigidbody rb;
    protected WingController wingController;
    protected GameEvents gameEvents;

    protected bool canFly;
    protected float velocity;
    protected bool isAir = false;
    protected bool isStart;
    protected bool youCanRun = false;

    private void Awake()
    {
        animations = GetComponentInChildren<Animations>();
        rb = GetComponent<Rigidbody>();
        wingController = GetComponentInChildren<WingController>();
    }

    protected virtual void Start()
    {
        gameEvents = GameEvents.instance;

        gameEvents.OnStart += () => { isStart = true; youCanRun = true; };
    }

    protected virtual void FixedUpdate()
    {
        SharedMovement();
    }

    public virtual void BorderEnter()
    {
        fly = Fly.Ground;
        wingController.wingCond = WingCond_.Fall;
        wingController.OnGround();

        if (youCanRun)
        {
            animations.Run();
        }

        canFly = false;
    }

    public virtual void BorderLeave()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + new Vector3(0, transform.position.y + 1, 0), Vector3.down, out raycastHit, 4))
        {
            isAir = true;
        }
        else
        {
            isAir = false;
            fly = Fly.OnAir;
            wingController.OnFly();
            animations.Fly();
            rb.AddForce(0, 500, 0);
            velocity = 50;
            StartCoroutine(Rigid());
        }
    }

    IEnumerator Rigid()
    {
        yield return new WaitForSeconds(0.5f);
        canFly = true;
        rb.useGravity = false;
        wingController.DropIt();
    }

    protected void RigidControl()
    {
        if (canFly && wingController.wingCond == WingCond_.Rise)
        {
            velocity = 0;
        }

        if (wingController.wingCond == WingCond_.Fall)
        {
            rb.useGravity = true;
            velocity = 20;
        }
    }

    protected void SharedMovement()
    {
        if (isStart)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, velocity);
            rb.velocity = new Vector3(0, rb.velocity.y, 0) + transform.forward * speed;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IInteract>()?.Interact(gameObject);
    }

    public void Finished(GameObject go)
    {
        isStart = false;
        rb.isKinematic = true;
        animations.Dance();
        wingController.OnGround();

        if (go.GetComponent<PlayerController>())
        {
            gameEvents.OnEnd?.Invoke();
        }

    }

    public void GameOver(GameObject go)
    {
        if (go.GetComponent<PlayerController>())
        {
            go.SetActive(false);
            gameEvents.GameOver?.Invoke();
        }
    }
}
