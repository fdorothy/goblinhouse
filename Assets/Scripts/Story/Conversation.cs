using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    SAY, ACTION, SAY_OTHER
}

[System.Serializable]
public class Blurb
{
    public DialogueType type;
    public string text;
}

public enum ConversationActionType
{
    NONE, BLURB, DIALOGUE_TYPE
}

[System.Serializable]
public class ConversationAction
{
    public ConversationActionType type;
    public Blurb blurb;
    public DialogueType dialogueType;
}

[System.Serializable]
public class Conversation
{
    public List<ConversationAction> actions;

    public void Parse(string text)
    {
        string[] lines = text.Split("\n"[0]);
        actions = new List<ConversationAction>();
        DialogueType type = DialogueType.SAY;
        List<string> validActions = new List<string>(new string[] { "SAY", "ACTION", "SAY_OTHER" });
        foreach (string line in lines)
        {
            string x = line.Trim();
            if (x == "") continue;

            ConversationAction action = new ConversationAction();
            if (validActions.Contains(x))
            {
                Debug.Log("Found an action: " + x);
                switch (x)
                {
                    case "SAY":
                        action.dialogueType = DialogueType.SAY;
                        break;
                    case "ACTION":
                        action.dialogueType = DialogueType.ACTION;
                        break;
                    case "SAY_OTHER":
                        action.dialogueType = DialogueType.SAY_OTHER;
                        break;
                    default: break;
                }
                type = action.dialogueType;
            }
            else
            {
                action.blurb = new Blurb();
                action.blurb.text = x;
                action.blurb.type = type;
                action.type = ConversationActionType.BLURB;
            }
            actions.Add(action);
        }
    }
}
