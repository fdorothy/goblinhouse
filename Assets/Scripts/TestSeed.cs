using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSeed : MonoBehaviour
{
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
        }
        StateManager.singleton.SetCurrentScene(this.gameObject.scene);
        FindObjectOfType<Player>().transform.position = transform.position;
        SceneManager.sceneLoaded -= this.SceneLoaded;
    }
}
