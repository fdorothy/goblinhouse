using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Clickable
{
    public List<Retroverse.Line> lines;
    protected Retroverse.Conversation conversation;
    public KeyStoryItem item = KeyStoryItem.NONE;

    public void Start()
    {
        conversation = new Retroverse.Conversation();
        lines.ForEach(line => conversation.Line(line.text, line.actor));
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
