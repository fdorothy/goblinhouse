using UnityEngine;

class Save
{
    public static State LoadStory(string rawJson)
    {
        return JsonUtility.FromJson<State>(rawJson);
    }

    public static string SaveStory(State story)
    {
        return JsonUtility.ToJson(story, true);
    }

    public void SaveStoryToPrefs(State story)
    {
        PlayerPrefs.SetString("save_data", SaveStory(story));
    }

    public State LoadStoryFromPrefs()
    {
        string raw = PlayerPrefs.GetString("save_data", "");
        if (raw == "")
        {
            return new State();
        }
        else
        {
            State story = LoadStory(raw);
            if (story == null)
                story = new State();
            return story;
        }
    }

    public static bool HasSavedStory()
    {
        return PlayerPrefs.HasKey("save_data");
    }
}
