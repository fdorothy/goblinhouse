using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using DG.Tweening;

public class Content : MonoBehaviour
{
    public TextAsset inkJson;
    public UnityEngine.UI.RawImage viewImage;
    private Story story;
    protected State state;
    public bool runningStory = false;
    public float storyPace = 5.0f;
    public bool skipNext = false;
    public float skipTimer = 3.0f;

    public void Start()
    {
        StateManager.singleton.content = this;
        state = StateManager.singleton.gameState;
        story = new Story(inkJson.text);
        RunStory();
    }

    public void ProcessInput(string item, Clickable clickable)
    {
        RunStory(item);
    }

    public void Update()
    {
        if (runningStory)
        {
            //skipTimer -= Time.deltaTime;
            if (Input.anyKeyDown || skipTimer < 0.0f)
            {
                skipNext = true;
            }
        }
    }

    public IEnumerator StoryRoutine(System.Action cb = null)
    {
        yield return new WaitForEndOfFrame();
        skipNext = false;
        while (story.canContinue)
        {
            string next = story.Continue();
            DialogueManager.singleton.CreateDialogue(trim(next), "main");
            yield return new WaitUntil(() => skipNext);
            skipTimer = storyPace;
            if (skipNext)
            {
                yield return new WaitForSeconds(0.25f);
            }
            skipNext = false;
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
            StopStory();
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
            story.ChoosePathString(path);
            RunStory(cb);
        }
    }

    public void RunStory(System.Action cb = null)
    {
        if (!runningStory)
        {
            runningStory = true;
            viewImage.DOFade(0.25f, 0.5f).OnComplete(() =>
            {
                StartCoroutine(StoryRoutine(cb));
            });
        }
    }

    public void StopStory()
    {
        if (runningStory)
        {
            DialogueManager.singleton.KillAllDialogues();
            viewImage.DOFade(1.0f, 0.5f).OnComplete(() =>
            {
                runningStory = false;
            });
        }
    }
}
