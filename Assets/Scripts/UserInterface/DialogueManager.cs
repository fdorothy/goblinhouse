using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialoguePrefab;
    public Transform dialogueParent;
    public static DialogueManager singleton;

    public void Awake()
    {
        singleton = this;
    }

    public void CreateDialogue(string text)
    {
        Dialogue d = Instantiate<Dialogue>(dialoguePrefab, dialogueParent);
        d.transform.SetParent(dialogueParent);
        d.RunDialogue(text);
    }
}
