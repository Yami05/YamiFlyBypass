using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.Jobs;

//public struct BuildJob : IJobParallelFor
//{
//    public void Execute(int index)
//    {
//        throw new System.NotImplementedException();
//    }
//}



//public class BuildingManager : MonoBehaviour
//{
//    [SerializeField] private List<WingController> wingControllers;


//    private void Update()
//    {
//        var job = new BuildJob();
//        var jobHandle = job.Schedule(wingControllers.Count, 1);
//        jobHandle.Complete();

//    }
//}

public class WingController : MonoBehaviour
{

    //public struct Data
    //{
    //    private Vector3 scale;

    //    private void Update()
    //    {
    //        //WingCond();
    //    }

    //    public Data(WingController wingController)
    //    {
    //        scale = Vector3.zero;
    //    }
    //}

    private TextMeshPro wingCountText;
    private WingPool wingPool;
    private GameEvents gameEvents;
    private List<GameObject> wings = new List<GameObject>();
    private CharacterBaseControl characterBase;

    public WingCond_ wingCond;
    private Vector3 scale;
    private GameObject wingPart;
    private bool canFly;

    private void Start()
    {
        wingCountText = GetComponentInChildren<TextMeshPro>();
        characterBase = transform.GetComponentInParent<CharacterBaseControl>();
        gameEvents = GameEvents.instance;
        wingPool = WingPool.instance;
        gameEvents.WingPosition += WingAdd;
    }

    public void OnGround()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnFly()
    {
        transform.Rotate(90, 0, 0);
    }

    private void Update()
    {
        WingCond();
    }

    public void WingAdd()
    {
        wingPart = wingPool.GetwingFromPool(0);
        scale = wingPart.transform.localScale;
        wingPart.transform.SetParent(transform);
        wings.Add(wingPart);
        wingCountText.text = wings.Count.ToString();
        wingPart.transform.localScale = scale;
    }

    private void WingCond()
    {

        if (wingCond == WingCond_.Rise)
        {
            canFly = true;
            for (int i = 2; i < wings.Count; i++)
            {

                wings[i].transform.localPosition = Vector3.Lerp(wings[i].transform.localPosition, new Vector3(wings[i - 2].transform.localPosition.x + (i % 2 == 0 ? .2f : -.2f), 0, i * 0.005f), 0.4f);
                wings[i].transform.localScale = new Vector3(wings[i].transform.localScale.x, wings[i - 1].transform.localScale.y + 0.05f, wings[i].transform.localScale.z);
            }

        }
        else
        {
            canFly = false;
            for (int i = 0; i < wings.Count; i++)
            {
                float mod = i % 2 == 0 ? -0.11f : 0.11f;
                wings[i].transform.localScale = scale;
                if (characterBase.fly == Fly.OnAir)
                {
                    wings[i].transform.localPosition = Vector3.Lerp(wings[i].transform.localPosition, new Vector3(mod, 0, 0), 0.5f);
                    wings[i].transform.rotation = transform.rotation;

                }
                else
                {
                    wings[i].transform.localPosition = new Vector3(mod, 0, 0);
                    wings[i].transform.rotation = transform.rotation;

                }

            }
        }
    }

    public void DropIt()
    {

        if (canFly == true)
        {
            StartCoroutine(DropWingPart());
        }

    }

    IEnumerator DropWingPart()
    {

        while (characterBase.fly == Fly.OnAir)
        {
            wingCountText.text = wings.Count.ToString();
            if (wingCond == WingCond_.Rise)
            {
                if (wings.Count % 2 == 1)
                {
                    wingPool.ReturnWingToPool(wings[0],0);
                    wings.Remove(wings[0]);
                }
                List<GameObject> gameObjects = wings.GetRange(wings.Count - 2, 2).ToList();

                foreach (GameObject go in gameObjects)
                {
                    go.transform.SetParent(null);
                    go.transform.localScale = scale;
                    go.AddComponent<Rigidbody>();
                }
                wings.RemoveRange(wings.Count - 2, 2);

                yield return new WaitForSeconds(0.5f);



                foreach (GameObject go in gameObjects)
                {
                    Destroy(go.GetComponent<Rigidbody>());
                    go.transform.localScale = scale;
                    wingPool.ReturnWingToPool(go,0);
                }
                if (wings.Count == 0)
                {
                    wingCountText.text = wings.Count.ToString();
                    yield break;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public int GetWingCount() => wings.Count;

}