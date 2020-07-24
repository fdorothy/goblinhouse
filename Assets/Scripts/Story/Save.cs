using UnityEngine;

class Save
{
    public static Story LoadStory(string rawJson)
    {
        return JsonUtility.FromJson<Story>(rawJson);
    }

    public static string SaveStory(Story story)
    {
        return JsonUtility.ToJson(story, true);
    }

    public void SaveStoryToPrefs(Story story)
    {
        PlayerPrefs.SetString("save_data", SaveStory(story));
    }

    public Story LoadStoryFromPrefs()
    {
        string raw = PlayerPrefs.GetString("save_data", "");
        if (raw == "")
        {
            return new Story();
        }
        else
        {
            Story story = LoadStory(raw);
            if (story == null)
                story = new Story();
            return story;
        }
    }

    public static bool HasSavedStory()
    {
        return PlayerPrefs.HasKey("save_data");
    }
}
