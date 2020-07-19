using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipbookManager : MonoBehaviour
{
    public Dictionary<string, Flipbook> flipbooks = new Dictionary<string, Flipbook>();
    public float fps = 20;
    public static FlipbookManager singleton;

    FlipbookManager()
    {
        singleton = this;
    }

    void Start()
    {
        StartCoroutine(FrameUpdater());
    }

    public void RegisterFlipbook(Flipbook flipbook)
    {
        flipbooks[flipbook.name] = flipbook;
    }

    public void UnregisterFlipbook(string name)
    {
        flipbooks.Remove(name);
    }

    IEnumerator FrameUpdater()
    {
        while (true)
        {
            foreach (Flipbook flipbook in flipbooks.Values)
            {
                flipbook.UpdateFrame();
            }
            yield return new WaitForSecondsRealtime(1.0f / fps);
        }
    }
}