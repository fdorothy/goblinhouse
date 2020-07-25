using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueVariant
{
    public string actor;
    public Dialogue prefab;
}

public class DialogueManager : MonoBehaviour
{
    public List<DialogueVariant> dialoguePrefabs;

    public Transform dialogueParent;
    public static DialogueManager singleton;
    public static float StoryPace = 1.5f;
    public bool runningConversation = false;

    protected List<Dialogue> dialogues = new List<Dialogue>();

    public void Awake()
    {
        singleton = this;
    }

    public void CreateDialogue(string text, string actor)
    {
        dialogues.ForEach((Dialogue dialog) => dialog.Shift());
        Dialogue d = Instantiate<Dialogue>(GetDialogue(actor), dialogueParent);
        d.transform.SetParent(dialogueParent);
        dialogues.Add(d);
        d.RunDialogue(text, () =>
        {
            dialogues.Remove(d);
        });
    }

    public Dialogue GetDialogue(string actor)
    {
        DialogueVariant v = dialoguePrefabs.Find(x => x.actor == actor);
        if (v != null)
        {
            return v.prefab;
        } else
        {
            return null;
        }
    }

    public IEnumerator StoryRoutine(Retroverse.Story story, string section, System.Action cb = null)
    {
        Debug.Log("Running story, chapter " + section);
        Retroverse.Interpreter interpreter = new Retroverse.Interpreter();
        interpreter.Start(story, section);
        yield return new WaitForEndOfFrame();
        while (interpreter.IsRunning())
        {
            Retroverse.ConversationAction action = interpreter.NextAction();
            if (action != null)
            {
                Debug.Log("Interpret action " + action);
                if (action.type == Retroverse.ConversationActionType.LINE)
                {
                    CreateDialogue(action.line.text, action.line.actor);
                    yield return new WaitForSeconds(StoryPace);
                }
                else if (action.type == Retroverse.ConversationActionType.DELAY)
                {
                    yield return new WaitForSeconds(action.delay);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            } else
            {
                break;
            }
        }
        runningConversation = false;
        if (cb != null)
            cb.Invoke();
    }

    public void RunStory(Retroverse.Story story, string section, System.Action cb = null)
    {
        if (!runningConversation)
        {
            runningConversation = true;
            StartCoroutine(StoryRoutine(story, section, cb));
        }
    }
}
