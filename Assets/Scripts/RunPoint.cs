using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RunPoint : MonoBehaviour
{
    public static RunPoint instance;

    public List<Transform> runPoints = new List<Transform>();

    private void Awake()
    {
        instance = this;
        Sort();
    }

    private void Sort()
    {
        runPoints.AddRange(GetComponentsInChildren<Transform>());
        runPoints.Remove(transform);
        runPoints = runPoints.OrderBy((x => x.transform.position.z)).ToList();

        for (int i = 0; i < runPoints.Count; i++)
        {
            runPoints[i].transform.SetParent(null);
        }

    }

    public int targetcount(Transform go) => runPoints.IndexOf(go);

    public Transform GetNext(Transform tar)
    {
        int index = runPoints.IndexOf(tar);

        if (tar == runPoints.Last())
        {
            return tar;
        }
        return runPoints[index + 1];
    }


}
