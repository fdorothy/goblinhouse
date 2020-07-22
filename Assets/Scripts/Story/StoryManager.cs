using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager singleton;
    private Story currentStory;
    public Story customStory;
    public bool useCustomStory = false;
    protected Scene currentScene;
    bool loaded = false;

    Player player;

    public Story gameState
    {
        get { return currentStory; }
        set { currentStory = gameState; }
    }

    StoryManager()
    {
        singleton = this;
        loaded = false;
    }

    private void Awake()
    {
        if (useCustomStory)
        {
            currentStory = customStory;
        } else
        {
            currentStory = new Story();
            customStory = currentStory;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = new Story();
        player = FindObjectOfType<Player>();
        LoadScene(gameState.scene, gameState.entryPoint);
        loaded = true;

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            currentScene = scene;
            SetPlayerPosition();
        };
    }

    public void LoadScene(string sceneName, string entryPoint)
    {
        Debug.Log("Load scene " + sceneName + " at " + entryPoint);
        gameState.scene = sceneName;
        gameState.entryPoint = entryPoint;

        // remove all objects from previous scene
        if (loaded && currentScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(currentScene);
            UnityEngine.AI.NavMesh.RemoveAllNavMeshData();
        }

        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(sceneName, parameters);
    }

    public void QuickText(string text)
    {
        Debug.Log(text);
        DialogueManager.singleton.CreateDialogue(text);
    }

    void SetPlayerPosition()
    {
        GameObject entryPoint = GameObject.Find(gameState.entryPoint);
        if (entryPoint)
        {
            player.transform.position = entryPoint.transform.position;
        }
        else
        {
            Debug.Log("Could not find entry point " + gameState.entryPoint);
        }
    }

    Story LoadStory(string rawJson)
    {
        return JsonUtility.FromJson<Story>(rawJson);
    }

    string SaveStory(Story story)
    {
        return JsonUtility.ToJson(story, true);
    }

    void SaveStoryToPrefs()
    {
        PlayerPrefs.SetString("save_data", SaveStory(currentStory));
    }

    void LoadStoryFromPrefs()
    {
        string raw = PlayerPrefs.GetString("save_data", "");
        if (raw == "")
        {
            currentStory = new Story();
        }
        else
        {
            currentStory = LoadStory(raw);
            if (currentStory == null)
                currentStory = new Story();
        }
    }

    bool HasSavedStory()
    {
        return PlayerPrefs.HasKey("save_data");
    }

    void NewStory()
    {
        currentStory = new Story();
    }
}
