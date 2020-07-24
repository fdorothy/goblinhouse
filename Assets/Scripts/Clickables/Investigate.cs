using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Clickable
{
    [Multiline]
    public string text;
    protected Conversation conversation;
    public DialogueType type = DialogueType.SAY;
    public KeyStoryItem item = KeyStoryItem.NONE;

    public void Start()
    {
        conversation = new Conversation();
        conversation.Parse(text);
    }

    public override CursorType GetCursorType()
    {
        return CursorType.EYE;
    }

    public override void TakeAction()
    {
        if (!StoryManager.singleton.runningConversation)
        {
            if (item != KeyStoryItem.NONE)
            {
                StoryManager.singleton.content.ProcessInput(item, this);
            }
            else
            {
                StartCoroutine(StoryManager.singleton.ConversationRoutine(conversation, null));
            }
        }
    }
}
