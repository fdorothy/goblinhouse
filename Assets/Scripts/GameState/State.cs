using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class State
{
    public string scene = "Bedroom";
    public string entryPoint = "FromStart";
    public bool bedroomRadioOn = false;

    public List<KeyStoryItem> clicked = new List<KeyStoryItem>();
}
