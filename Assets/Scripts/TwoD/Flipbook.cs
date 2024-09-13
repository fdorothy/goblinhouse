using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlipbookSequence
{
    public string name;
    public Sprite[] sprites;
}

public class Flipbook : MonoBehaviour
{
    public FlipbookSequence[] sequences;
    public string sequence = "";
    public int period = 1;

    private SpriteRenderer sr;
    private Dictionary<string, FlipbookSequence> sequenceLookup = new Dictionary<string, FlipbookSequence>();
    private int ticks = 0;
    private int currentFrame = 0;
    private FlipbookSequence currentSequence;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ticks = period;
        BuildSequenceLookup(false);
        if (sequence != "" && sequenceLookup.ContainsKey(sequence))
        {
            currentSequence = sequenceLookup[sequence];
            if (currentSequence.sprites.Length > 0)
                sr.sprite = currentSequence.sprites[0];
        }
        yield return new WaitUntil(() => FlipbookManager.singleton != null);
        FlipbookManager.singleton.RegisterFlipbook(this);
    }

    private void OnDestroy()
    {
        FlipbookManager.singleton.UnregisterFlipbook(this.name);
    }

    public void BuildSequenceLookup(bool rebuild = true)
    {
        if (rebuild)
            sequenceLookup.Clear();
        foreach (FlipbookSequence sequence in sequences)
        {
            sequenceLookup[sequence.name] = sequence;
        }
    }

    public void AddSequence(string name, Sprite[] sprites)
    {
        FlipbookSequence seq = new FlipbookSequence();
        seq.name = name;
        seq.sprites = sprites;
        sequenceLookup[name] = seq;
    }

    public void PlaySequence(string sequence, bool restart = false)
    {
        if (!restart && sequence.Equals(this.sequence))
            return;
        this.sequence = sequence;
        if (sequenceLookup.ContainsKey(sequence))
        {
            currentSequence = sequenceLookup[sequence];
            currentFrame = 0;
            ticks = 0;
        }
    }

    public void UpdateFrame()
    {
        if (currentSequence == null || currentSequence.sprites.Length == 0)
            return;
        ticks -= 1;
        if (ticks <= 0)
        {
            sr.sprite = currentSequence.sprites[currentFrame++];
            if (currentFrame >= currentSequence.sprites.Length)
                currentFrame = 0;
            ticks = period;
        }
    }
}
