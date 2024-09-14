using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => StateManager.singleton != null && StateManager.singleton.content != null);
        StateManager.singleton.content.OnSoundEffect = (name) => {
            PlaySfx(name);
        };
    }

    void PlaySfx(string name)
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
