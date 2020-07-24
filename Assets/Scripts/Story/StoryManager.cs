using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static float StoryPace = 1.5f;
    public static StoryManager singleton;
    private Story currentStory;
    public Story customStory;
    public bool useCustomStory = false;
    public StoryContent content;
    protected Scene currentScene;
    bool loaded = false;
    public bool runningConversation = false;

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
        }
        else
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

    public void Dialogue(string text, string actor)
    {
        DialogueManager.singleton.CreateDialogue(text, actor);
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

    void NewStory()
    {
        currentStory = new Story();
    }

    public IEnumerator ConversationRoutine(Retroverse.Conversation conversation, System.Action cb = null)
    {
        yield return new WaitForEndOfFrame();

        foreach (var action in conversation.actions)
        {
            if (action.type == Retroverse.ConversationActionType.LINE)
            {
                Dialogue(action.line.text, action.line.actor);
                yield return new WaitForSeconds(StoryPace);
            } else if (action.type == Retroverse.ConversationActionType.DELAY)
            {
                yield return new WaitForSeconds(action.delay);
            } else if (action.type == Retroverse.ConversationActionType.FUNCTION)
            {
                action.function.Invoke();
            }
        }
        runningConversation = false;
        if (cb != null)
            cb.Invoke();
    }

    public void RunConversation(Retroverse.Conversation conversation, System.Action cb = null)
    {
        runningConversation = true;
        StartCoroutine(ConversationRoutine(conversation, cb));
    }
}
