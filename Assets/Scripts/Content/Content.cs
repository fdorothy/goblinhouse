using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Content : MonoBehaviour
{
    public TextAsset inkJson;
    private Story story;
    protected State state;
    public bool runningStory = false;
    public float storyPace = 2.0f;

    public void Start()
    {
        StateManager.singleton.content = this;
        state = StateManager.singleton.gameState;
        story = new Story(inkJson.text);
        StartCoroutine(StoryRoutine());
    }

    public void ProcessInput(string item, Clickable clickable)
    {
        RunStory(item);
    }

    public IEnumerator StoryRoutine(System.Action cb = null)
    {
        yield return new WaitForEndOfFrame();
        while (story.canContinue)
        {
            string next = story.Continue();
            DialogueManager.singleton.CreateDialogue(trim(next), "main");
            yield return new WaitForSeconds(storyPace);
        }
        ShowChoices();
    }

    public void ShowChoices()
    {
        DialogueManager dm = DialogueManager.singleton;
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                dm.ShowChoice(trim(choice.text), () =>
                {
                    story.ChooseChoiceIndex(choice.index);
                    StartCoroutine(StoryRoutine());
                });
            }
        }
        else
        {
            runningStory = false;
        }
    }

    public string trim(string text)
    {
        return text.Trim();
    }

    public void RunStory(string path, System.Action cb = null)
    {
        if (!runningStory)
        {
            runningStory = true;
            story.ChoosePathString(path);
            StartCoroutine(StoryRoutine(cb));
        }
    }
}
