using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This enum is a list of all important items that appear in the game. When it is clicked, we call
 * StoryContent, which will maybe do some dialogue and set our Story state.
 * */
[Serializable]
public enum KeyStoryItem
{
    NONE,

    // main bedroom
    BEDROOM_LAPTOP,
    BEDROOM_RADIO,
    BEDROOM_DOOR,
    BEDROOM_WINDOW
}

public class Content : MonoBehaviour
{
    protected State state;

    public void Awake()
    {
        StateManager.singleton.content = this;
        state = StateManager.singleton.gameState;
    }

    public void ProcessInput(KeyStoryItem item, Clickable clickable)
    {
        ProcessInput(item, clickable, RememberClick(item));

    }

    public virtual void ProcessInput(KeyStoryItem item, Clickable clickable, bool firstClick)
    {

    }

    public bool RememberClick(KeyStoryItem item)
    {
        if (item != KeyStoryItem.NONE) {
            List<KeyStoryItem> clicked = StateManager.singleton.gameState.clicked;
            if (!clicked.Contains(item)) {
                clicked.Add(item);
                return true;
            }
        }
        return false;
    }

    public void RunStory(Retroverse.Story c, string section, System.Action cb = null)
    {
        DialogueManager.singleton.RunStory(c, section, cb);
    }
}
