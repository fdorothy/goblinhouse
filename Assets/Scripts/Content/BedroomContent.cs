using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomContent : StoryContent
{
    static string laptop_first_click = "ACTION\nNo Internet connection\nSAY\nI think the router is in the living room downstairs.";
    static string laptop_click = "The internet is still off.\nI think the router is in the living room downstairs.";

    static string window_click = "The storm rages outside.\nYou see the gravestones in the cemetary below in a flash of lightning.";
    static string window_first_click = "A wet winter storm rages outside.\nYou see the gravestones in the cemetary next door in a flash of lightning.\nIn the flash, you see dark figures scurring amongst the stones.";

    static string radio_first_click = @"
    ACTION
    click...bzz
    SAY_OTHER
    the weather is quite bad for Ireland
    the hurricane has knocked down power lines all over the island
    wind gusts over 100 kilometers per...
    ACTION
    bzz...click";

    static string radio_click = "ACTION\nclick...bzz\nSAY\nI can't seem to find any stations";

    public override void ProcessInput(KeyStoryItem item, Clickable clickable, bool firstClick)
    {
        switch (item)
        {
            case KeyStoryItem.BEDROOM_LAPTOP:
                RunConversation(firstClick ? laptop_first_click : laptop_click);
                break;
            case KeyStoryItem.BEDROOM_WINDOW:
                RunConversation(firstClick ? window_first_click : window_click);
                break;
            case KeyStoryItem.BEDROOM_RADIO:
                RunConversation(firstClick ? radio_first_click : radio_click);
                break;
            default: break;
        }
    }
}