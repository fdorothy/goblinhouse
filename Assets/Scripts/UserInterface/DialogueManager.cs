﻿using System.Collections;
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

    public void ShowChoice(string text, System.Action cb)
    {
        // do nothing for now
    }
}
