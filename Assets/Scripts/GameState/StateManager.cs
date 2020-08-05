using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager singleton;
    private State currentState;
    public State customState;
    public bool useCustomState = false;
    public Content content;
    protected Scene currentScene;
    bool loaded = false;

    Player player;

    public State gameState
    {
        get { return currentState; }
        set { currentState = gameState; }
    }

    StateManager()
    {
        singleton = this;
        loaded = false;
    }

    private void Awake()
    {
        if (useCustomState)
        {
            currentState = customState;
        }
        else
        {
            currentState = new State();
            customState = currentState;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = new State();
        player = FindObjectOfType<Player>();
        loaded = true;

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            SetCurrentScene(scene);
            SetPlayerPosition();
        };
    }

    public void SetCurrentScene(Scene scene)
    {
        Debug.Log("Setting current scene to " + scene.name);
        currentScene = scene;
    }

    public void LoadScene(string sceneName, string entryPoint, System.Action cb = null)
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
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, parameters);
        op.completed += (AsyncOperation obj) =>
        {
            cb.Invoke();
        };
    }

    void SetPlayerPosition()
    {
        GameObject entryPoint = GameObject.Find(gameState.entryPoint);
        if (entryPoint)
        {
            Debug.Log("Found entry point " + gameState.entryPoint + ", " + entryPoint.transform.position);
            player.SetDestination(entryPoint.transform.position);
            player.agent.Warp(entryPoint.transform.position);
        }
        else
        {
            Debug.Log("Could not find entry point " + gameState.entryPoint);
        }
    }
}
