using UnityEngine;


public class JsonReader : MonoBehaviour
{
    public static JsonReader instance;

    private Sprite[] sprites;
    //??

    private void Awake()
    {
        instance = this;
        BotNameIt();
        sprites = Resources.LoadAll<Sprite>("flags/flags");
    }

    public class Bot
    {
        public string[] names;
    }

    public Bot bot = new Bot();

    public void BotNameIt()
    {
        var jsonTextFile = Resources.Load<TextAsset>("flags/BotNames");

        bot = JsonUtility.FromJson<Bot>(jsonTextFile.text);

    }

    public string GetRandomName()
    {
        return bot.names[Random.Range(0, bot.names.Length)];
    }

    public Sprite GetRandomSprite()
    {
        return sprites[Random.Range(0,sprites.Length)];
    }

}
