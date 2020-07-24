using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueVariant
{
    public DialogueType type;
    public Dialogue prefab;
}

public class DialogueManager : MonoBehaviour
{
    public List<DialogueVariant> dialoguePrefabs;

    public Transform dialogueParent;
    public static DialogueManager singleton;

    protected List<Dialogue> dialogues = new List<Dialogue>();

    public void Awake()
    {
        singleton = this;
    }

    public void CreateDialogue(string text, DialogueType type)
    {
        dialogues.ForEach((Dialogue dialog) => dialog.ShiftDown());
        Dialogue d = Instantiate<Dialogue>(GetDialogue(type), dialogueParent);
        d.transform.SetParent(dialogueParent);
        dialogues.Add(d);
        d.RunDialogue(text, () =>
        {
            dialogues.Remove(d);
        });
    }

    public Dialogue GetDialogue(DialogueType type)
    {
        DialogueVariant v = dialoguePrefabs.Find(x => x.type == type);
        if (v != null)
        {
            return v.prefab;
        } else
        {
            return null;
        }
    }
}
