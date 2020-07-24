using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomContent : StoryContent
{
    Retroverse.Conversation laptopFirstClick;
    Retroverse.Conversation laptopClick;

    Retroverse.Conversation windowFirstClick;
    Retroverse.Conversation windowClick;

    Retroverse.Conversation radioFirstClick;
    Retroverse.Conversation radioClick;

    public void Start()
    {
        laptopFirstClick = Retroverse.Conversation.Begin()
            .Line("No Internet connection", "item")
            .Line("I think the router is in the living room downstairs.", "main");
        laptopClick = Retroverse.Conversation.Begin()
            .Line("The internet is still off.", "main")
            .Line("I think the router is in the living room downstairs.", "main");

        windowFirstClick = Retroverse.Conversation.Begin()
            .Line("A wet winter storm rages outside.", "main")
            .Line("You see the gravestones in the cemetary next door in a flash of lightning.", "main")
            .Line("In the flash, you see dark figures scurring amongst the stones.", "main");
        windowClick = Retroverse.Conversation.Begin()
            .Line("The storm rages outside.", "main")
            .Line("You see the gravestones in the cemetary below in a flash of lightning.");

        radioFirstClick = Retroverse.Conversation.Begin()
            .Line("click...bzz", "item")
            .Line("the weather is quite bad for Ireland", "other")
            .Line("the hurricane has knocked down power lines all over the island", "other")
            .Line("wind gusts over 100 kilometers per...", "other")
            .Line("bzz...click", "item");

        radioClick = Retroverse.Conversation.Begin()
            .Line("click...bzz", "item")
            .Line("I can't seem to find any stations", "main");
    }

    public override void ProcessInput(KeyStoryItem item, Clickable clickable, bool firstClick)
    {
        switch (item)
        {
            case KeyStoryItem.BEDROOM_LAPTOP:
                RunConversation(firstClick ? laptopFirstClick : laptopClick);
                break;
            case KeyStoryItem.BEDROOM_WINDOW:
                RunConversation(firstClick ? windowFirstClick : windowClick);
                break;
            case KeyStoryItem.BEDROOM_RADIO:
                RunConversation(firstClick ? radioFirstClick : radioClick);
                break;
            default: break;
        }
    }
}