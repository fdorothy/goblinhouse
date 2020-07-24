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

public class StoryContent : MonoBehaviour
{
    protected Story story;

    public void Awake()
    {
        StoryManager.singleton.content = this;
        story = StoryManager.singleton.gameState;
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
            List<KeyStoryItem> clicked = StoryManager.singleton.gameState.clicked;
            if (!clicked.Contains(item)) {
                clicked.Add(item);
                return true;
            }
        }
        return false;
    }

    public void RunConversation(Retroverse.Conversation c, System.Action cb = null)
    {
        StoryManager.singleton.RunConversation(c, cb);
    }

    public void Say(string text, string actor)
    {
        DialogueManager.singleton.CreateDialogue(text, actor);
    }
}
