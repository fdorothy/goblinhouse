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

    public void RunConversation(string text, System.Action cb = null)
    {
        Conversation c = new Conversation();
        c.Parse(text);
        RunConversation(c, cb);
    }

    public void RunConversation(Conversation c, System.Action cb = null)
    {
        StoryManager.singleton.RunConversation(c, cb);
    }

    public void Say(string text)
    {
        DialogueManager.singleton.CreateDialogue(text, DialogueType.SAY);
    }

    public void SayOther(string text)
    {
        DialogueManager.singleton.CreateDialogue(text, DialogueType.SAY_OTHER);
    }

    public void ShowAction(string text)
    {
        DialogueManager.singleton.CreateDialogue(text, DialogueType.ACTION);
    }
}
