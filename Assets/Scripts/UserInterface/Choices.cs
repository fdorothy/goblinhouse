using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choices : MonoBehaviour
{
    public void AddChoice(DialogueChoice choice)
    {
        choice.transform.SetParent(this.transform);
    }
}
