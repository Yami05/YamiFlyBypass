using DG.Tweening;
using System.Collections;
using UnityEngine;
using TMPro;
public class AIController : CharacterBaseControl, IReturn
{


    [SerializeField] private Transform target;
    [SerializeField] private AIdir aIdir;
    [SerializeField] private TextMeshPro botName;

    private SpriteRenderer sprite;
    private JsonReader jsonReader;
    private RunPoint runPoint;
    private SkinnedMeshRenderer skinned;

    private Vector3 direction;
    private Vector3 pos;
    private Vector3 targetPos;

    private bool co = true;
    private float waitTime;

    protected override void Start()
    {
        base.Start();

        skinned = GetComponentInChildren<SkinnedMeshRenderer>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        runPoint = RunPoint.instance;
        jsonReader = JsonReader.instance;

        target = runPoint.runPoints[0];

        SetAIPref();
        StartCoroutine(RandomDirection());

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        AIMovement();
    }

    private void SetAIPref()
    {
        botName.text = jsonReader.GetRandomName();
        sprite.sprite = jsonReader.GetRandomSprite();
        skinned.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public override void BorderLeave()
    {
        base.BorderLeave();

        if (!isAir)
        {
            StartCoroutine(openWing());
            wingController.wingCond = WingCond_.Rise;
        }
    }

    IEnumerator openWing()
    {
        while (fly == Fly.OnAir)
        {

            yield return new WaitForSeconds(waitTime);
            wingController.wingCond = WingCond_.Rise;

            if (fly == Fly.Ground)
            {
                yield return null;
            }
        }
    }


    private void AIMovement()
    {
        GoNext();

        if (wingController.GetWingCount() < 1)
        {
            wingController.wingCond = WingCond_.Fall;

            rb.useGravity = true;
        }

        RigidControl();
    }

    IEnumerator RandomDirection()
    {
        while (co == true)
        {
            direction = (aIdir == AIdir.X ? new Vector3(targetPos.x, pos.y, Random.Range(targetPos.z - 4, targetPos.z + 4)) :
                new Vector3(Random.Range(targetPos.x - 4, targetPos.x + 4), pos.y, targetPos.z));

            transform.DOLookAt(direction, 1);
            yield return new WaitForSeconds(waitTime);
            waitTime = Random.Range(1, 3);
        }
    }


    private void GoNext()
    {
        pos = transform.position;
        targetPos = target.position;

        float distance = Vector3.Distance(pos, targetPos);

        if (distance < 7 || (aIdir == AIdir.X ? pos.x < targetPos.x : pos.z > targetPos.z))
        {
            target = runPoint.GetNext(target);
        }
    }

    public AIdir Return(Direction direction) => direction == Direction.Z ? aIdir = AIdir.Z : aIdir = AIdir.X;

}
