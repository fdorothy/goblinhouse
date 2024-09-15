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
    public DialogueChoice choicePrefab;
    public Choices choicesPrefab;

    public Transform dialogueParent;
    public static DialogueManager singleton;
    public bool runningConversation = false;

    protected List<Dialogue> dialogues = new List<Dialogue>();
    protected List<DialogueChoice> choices = new List<DialogueChoice>();
    protected Choices choicesRow;

    public void Awake()
    {
        singleton = this;
    }

    public void CreateDialogue(string text, string actor, bool smooth)
    {
        dialogues.ForEach((Dialogue dialog) => dialog.Shift(smooth));
        Dialogue d = Instantiate<Dialogue>(GetDialogue(actor), dialogueParent);
        d.transform.SetParent(dialogueParent);
        d.targetPosition = d.transform.position;
        KillAllDialogues();
        dialogues.Add(d);
        d.RunDialogue(text);
    }

    public void KillAllDialogues()
    {
        foreach (Dialogue dialogue in dialogues)
        {
            dialogue.DestroyDialogue(() =>
            {
                dialogues.Remove(dialogue);
            });
        }
    }

    public void KillAllChoices()
    {
        if (choicesRow != null)
        {
            Destroy(choicesRow.gameObject);
        }
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

    public void ShowChoice(string text, System.Action cb)
    {
        if (choicesRow == null)
        {
            choicesRow = Instantiate<Choices>(choicesPrefab, dialogueParent);
            dialogues.ForEach((Dialogue dialog) => dialog.Shift(true));
            KillAllDialogues();
        }
        DialogueChoice dc = Instantiate<DialogueChoice>(choicePrefab, dialogueParent);
        choicesRow.AddChoice(dc);
        dc.RunChoice(text, () =>
        {
            KillAllDialogues();
            KillAllChoices();
            cb.Invoke();
        });
    }
}
