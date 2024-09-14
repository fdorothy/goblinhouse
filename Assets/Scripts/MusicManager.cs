using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> clips;
    string musicPlaying = "";
    float targetVolume = 0.0f;
    public float maxVolume = 1.0f;
    Coroutine fadeRoutine = null;

    // Update is called once per frame
    void Update()
    {
        Content content = FindObjectOfType<Content>();
        if (content)
        {
            string music = content.story.variablesState["music"].ToString();
            if (music != musicPlaying)
            {
                if (fadeRoutine != null)
                    StopCoroutine(fadeRoutine);
                fadeRoutine = StartCoroutine(SetNextTrackRoutine(music));
                musicPlaying = music;
            }
        }
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.unscaledDeltaTime);
    }

    IEnumerator SetNextTrackRoutine(string nextTrack)
    {
        targetVolume = 0.0f;
        yield return new WaitForSeconds(1.0f);
        audioSource.clip = clips.Find(clip => clip.name == nextTrack);
        audioSource.Play();
        audioSource.loop = true;
        targetVolume = maxVolume;
    }
}
