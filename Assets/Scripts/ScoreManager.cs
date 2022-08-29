using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int place = 1;

    private void Awake()
    {
        instance = this;
    }

    public string GetPlaceUI() => place.ToString() + GetOrdinalNumber();
    public int GetPlace() => place++;

    private string GetOrdinalNumber()
    {
        if (place.ToString().EndsWith("1")) return "st";
        if (place.ToString().EndsWith("2")) return "nd";
        if (place.ToString().EndsWith("3")) return "rd";
        return "th";
    }


    //switch
}
