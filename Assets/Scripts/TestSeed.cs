using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSeed : MonoBehaviour
{
    public string scene = "";
    public string position = "";

    // Start is called before the first frame update
    void Start()
    {
        if (!SceneManager.GetSceneByName("Main").isLoaded)
        {
            Debug.Log("Using test seed");
            SceneManager.sceneLoaded += this.SceneLoaded;
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        }
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Finding content...");
        Content content = FindObjectOfType<Content>();
        if (content != null)
        {
            Debug.Log("Disabling story");
            content.runStoryOnStart = false;
            content.startScene = this.scene;
            content.startPosition = this.position;
        }
        StateManager.singleton.SetCurrentScene(this.gameObject.scene);
        FindObjectOfType<Player>().transform.position = transform.position;
        SceneManager.sceneLoaded -= this.SceneLoaded;
    }
}
