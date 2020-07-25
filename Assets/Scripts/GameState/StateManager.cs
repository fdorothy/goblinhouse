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
}
