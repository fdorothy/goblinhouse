using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : Clickable
{
    public List<Retroverse.Line> lines;
    protected Retroverse.Conversation conversation;
    protected Retroverse.Story story;
    public KeyStoryItem item = KeyStoryItem.NONE;

    public void Start()
    {
        conversation = new Retroverse.Conversation();
        lines.ForEach(line => conversation.Line(line.text, line.actor));
        story = new Retroverse.Story();
        story.AddSection("_default", conversation);
    }

    public override CursorType GetCursorType()
    {
        return CursorType.EYE;
    }

    public override void TakeAction()
    {
        if (!DialogueManager.singleton.runningConversation)
        {
            if (item != KeyStoryItem.NONE)
            {
                StateManager.singleton.content.ProcessInput(item, this);
            }
            else
            {
                StartCoroutine(DialogueManager.singleton.StoryRoutine(story, "_default", null));
            }
        }
    }
}
