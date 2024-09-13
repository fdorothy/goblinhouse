using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            TurnOn();
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));

            // flicker
            for (int i = 0; i < 5; i++)
            {
                TurnOff();
                yield return new WaitForSeconds(Random.Range(.03f, .1f));
                TurnOn();
                yield return new WaitForSeconds(Random.Range(.1f, .2f));
            }
        }
    }

    void TurnOff()
    {
        foreach (Lamp l in AllLamps())
            l.TurnOff();
    }
    void TurnOn()
    {
        foreach (Lamp l in AllLamps())
            l.TurnOn();
    }

    Lamp[] AllLamps() => FindObjectsOfType<Lamp>();
}
