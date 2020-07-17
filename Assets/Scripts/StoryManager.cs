﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager singleton;
    public GameState gameState;
    protected Scene currentScene;
    bool loaded = false;

    Player player;

    StoryManager()
    {
        singleton = this;
        loaded = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameState = new GameState();
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