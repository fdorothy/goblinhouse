using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource quietSource;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => StateManager.singleton != null && StateManager.singleton.content != null);
        StateManager.singleton.content.OnSoundEffect = (name) => {
            PlaySfx(name, audioSource);
        };
        StateManager.singleton.content.OnTyping = () =>
        {
            if (Random.Range(0, 2) == 0)
                PlaySfx("keyboard01", quietSource);
            else
                PlaySfx("keyboard02", quietSource);
        };
    }

    void PlaySfx(string name, AudioSource audioSource)
    {
        var clip = Resources.Load(name) as AudioClip;
        if (clip == null)
        {
            Debug.Log("could not find audio clip " + name);
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
