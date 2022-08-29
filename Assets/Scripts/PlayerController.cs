using UnityEngine;

public class PlayerController : CharacterBaseControl
{

    [SerializeField] private float sens;
    [SerializeField] private Camera cam;

    private Vector3 firstClickPoint;
    private Vector3 lastClickPoint;
    private Vector3 diff;

    protected override void Start()
    {
        base.Start();
        animations = GetComponentInChildren<Animations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }

        if (Input.GetMouseButton(0))
        {
            MouseDrag();
            if (fly == Fly.OnAir && wingController.GetWingCount() > 1)
            {
                wingController.wingCond = WingCond_.Rise;
            }
            else
            {
                wingController.wingCond = WingCond_.Fall;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
            if (fly == Fly.OnAir)
            {
                wingController.wingCond = WingCond_.Fall;
            }

        }

        RigidControl();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CharacterMovement();
    }

    private void OnMouseClick()
    {
        firstClickPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        lastClickPoint = firstClickPoint;
    }

    private void MouseDrag()
    {
        lastClickPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        diff = lastClickPoint - firstClickPoint;
        diff *= sens;
    }

    private void OnMouseUp()
    {
        diff.x = 0;
    }

    private void CharacterMovement()
    {
        if (diff.x != 0 && isStart)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + diff.x * 0.3f, 0);
        }
    }

}
