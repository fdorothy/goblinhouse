using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager singleton;
    public GameState gameState;

    protected Dictionary<string, System.Action> stateChanges;

    Player player;

    StoryManager()
    {
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameState = new GameState();
        player = FindObjectOfType<Player>();
        BuildStory();
        SetPlayerPosition();
    }

    void BuildStory()
    {
        stateChanges = new Dictionary<string, System.Action>();

        //  bedroom 1 clickables
        stateChanges["BedroomDoorClickable"] = () => LoadScene("Hallway", "Bedroom1");
        stateChanges["BearHeadClickable"] = () => QuickText("A mounted bear's head. What an odd decoration.");
        stateChanges["LaptopClickable"] = () => QuickText("No Network Connection Found. I think the router is downstairs in the living room.");
        stateChanges["RadioClickable"] = () =>
        {
            QuickText("bzz...severe storms...trees down...power outages expected in the area. Now back to our normal scheduling.");
        };
    }

    public void TakeAction(string key)
    {
        System.Action action;
        if (stateChanges.TryGetValue(key, out action))
        {
            action.Invoke();
        }
    }

    void LoadScene(string sceneName, string loadPoint)
    {
        Debug.Log("Load scene " + sceneName + " at " + loadPoint);
    }

    void SetPlayerPosition()
    {
        GameObject entryPoint = GameObject.Find(gameState.entryPoint);
        if (entryPoint) {
            player.transform.position = entryPoint.transform.position;
        }
    }

    void QuickText(string text)
    {
        Debug.Log(text);
    }
}
