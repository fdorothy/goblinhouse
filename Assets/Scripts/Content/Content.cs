using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using DG.Tweening;
using System.Text.RegularExpressions;

public class Content : MonoBehaviour
{
    public TextAsset inkJson;
    public UnityEngine.UI.RawImage viewImage;
    public Story story;
    protected State state;
    public bool runningStory = false;
    public float storyPace = 5.0f;
    public bool skipNext = false;
    public float skipTimer = 3.0f;
    public bool tagsDone = true;
    public bool runStoryOnStart = true;
    bool startOfStory = true;
    public Dictionary<string, int> clickables = new Dictionary<string, int>();
    public System.Action<string> OnSoundEffect;

    public string startScene = "";
    public string startPosition = "";

    protected Regex clickChoiceRegex = new Regex(@":(?<option>(i|e)) (?<gameobject>\w+) (?<hovertext>.*)");
    protected Regex sceneRegex = new Regex(@":(?<option>(scene)) (?<scene>\w+) (?<position>\w+)");
    protected Regex sfxRegex = new Regex(@":(?<option>(sfx)) (?<name>\w+)");

    public void Start()
    {
        StateManager.singleton.content = this;
        state = StateManager.singleton.gameState;
        RestartStory();
    }

    public void RunFrom(string scene, string position)
    {
        object[] args = new object[] { position };
        story.ChoosePathString(scene, true, args);
        RunStory();
    }

    public void ProcessInput(string item, Clickable clickable)
    {
        if (clickables.ContainsKey(item))
        {
            story.ChooseChoiceIndex(clickables[item]);
            RunStory();
        }
        else
        {
            Debug.LogError("Could not find item in options: " + item);
        }
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
        skipNext = true;
        bool showedText = false;
        while (story.canContinue)
        {
            string next = trim(story.Continue());
            bool showText = ProcessText(next);
            yield return new WaitUntil(() => tagsDone);
            ProcessTags();
            yield return new WaitUntil(() => tagsDone);
            if (showText && next != "")
            {
                DialogueManager.singleton.CreateDialogue(next, "main", !startOfStory);
                if (next != "")
                {
                    yield return new WaitUntil(() => skipNext);
                    showedText = true;
                }
                skipTimer = storyPace;
                if (skipNext)
                {
                    if (!startOfStory)
                        yield return new WaitForSeconds(1f);
                }
            }
        }
        yield return new WaitUntil(() => tagsDone);
        if (showedText)
        {
            skipNext = false;
            yield return new WaitUntil(() => skipNext);
        }
        startOfStory = false;
        ShowChoices();
    }

    public bool ShowChoices()
    {
        DialogueManager dm = DialogueManager.singleton;
        clickables.Clear();
        bool canContinue = true;
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                if (!ProcessClickableChoice(choice))
                {
                    // manually show the choice and keep on showing the story!
                    canContinue = false;
                    dm.ShowChoice(trim(choice.text), () =>
                    {
                        story.ChooseChoiceIndex(choice.index);
                        StartCoroutine(StoryRoutine());
                    });
                }
            }
        }
        if (canContinue)
            StopStory();
        return canContinue;
    }

    public bool WillShowChoices()
    {
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                Match m = clickChoiceRegex.Match(choice.text);
                if (!m.Success)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool ProcessClickableChoice(Choice choice)
    {
        Match m = clickChoiceRegex.Match(choice.text);
        if (m.Success)
        {
            string goName = m.Groups["gameobject"].Value;
            GameObject go = GameObject.Find(m.Groups["gameobject"].Value);
            if (go)
            {
                Clickable c = go.GetComponent<Clickable>();
                if (c)
                {
                    string hoverText = m.Groups["hovertext"].Value;
                    Debug.Log("found " + goName + " and setting custom title to " + hoverText);
                    c.customTitle = hoverText;
                    clickables[goName] = choice.index;
                }
                else
                {
                    Debug.LogError("Missing clickable on game object: " + goName);
                }
            }
            else
            {
                Debug.LogError("Could not find game object for clickable: " + goName);
            }
            return true;
        }
        return false;
    }

    public bool ProcessText(string text)
    {
        Match m = sceneRegex.Match(text);
        if (m.Success)
        {
            string scene = m.Groups["scene"].Value;
            string position = m.Groups["position"].Value;
            tagsDone = false;
            Debug.Log("loading scene: " + scene);
            Sequence seq = DOTween.Sequence();
            float originalAlpha = viewImage.color.a;
            if (originalAlpha > 0.0f)
                seq.Append(viewImage.DOFade(0.0f, 0.5f));
            seq.AppendCallback(() => StateManager.singleton.LoadScene(scene, position, () =>
            {
                tagsDone = true;
            }));
            if (originalAlpha > 0.0f)
                seq.Append(viewImage.DOFade(originalAlpha, 0.5f));
            return false;
        }
        m = sfxRegex.Match(text);
        if (m.Success)
        {
            string name = m.Groups["name"].Value;
            if (OnSoundEffect != null)
                OnSoundEffect.Invoke(name);
            return false;
        }
        return true;
    }

    public void ProcessTags()
    {
        Dictionary<string, string> tags = GetTags();
        if (tags.ContainsKey("Clickables"))
        {
            switch (tags["clickables"])
            {
                case "clear": clickables.Clear(); break;
                default: break;
            }
        }
    }

    public Dictionary<string, string> GetTags()
    {
        Dictionary<string, string> lookup = new Dictionary<string, string>();
        foreach (string tag in story.currentTags)
        {
            string[] tags = tag.Split(':');
            if (tags.Length == 2)
            {
                string key = trim(tags[0]).ToLower();
                string value = trim(tags[1]);
                Debug.Log(key + ": " + value);
                lookup[key] = value;
            }
        }
        return lookup;
    }

    public string trim(string text)
    {
        return text.Trim();
    }

    public void RunStory(string path, System.Action cb = null)
    {
        if (!runningStory)
        {
            story.ChoosePathString(path, false);
            RunStory(cb);
        }
    }

    public void RunStory(System.Action cb = null)
    {
        if (!runningStory)
        {
            runningStory = true;
            ClickableManager.singleton.ClearCursor();
            float originalAlpha = viewImage.color.a;
            if (originalAlpha > 0.25f)
            {
                viewImage.DOFade(0.25f, 0.5f).OnComplete(() => {
                    startOfStory = true;
                    StartCoroutine(StoryRoutine(cb));
                });
            }
            else
            {
                startOfStory = true;
                StartCoroutine(StoryRoutine(cb));
            }
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

    public void RestartStory()
    {
        story = new Story(inkJson.text);

        // make sure the story is set to 'interactive' so that we can move around and
        // click on things
        story.variablesState["interactive"] = true;

        if (runStoryOnStart)
            RunStory();
        else
        {
            viewImage.DOFade(1.0f, 0.5f);
            RunFrom(startScene, startPosition);
        }
    }
}
