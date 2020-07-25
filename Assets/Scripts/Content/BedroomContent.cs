using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomContent : Content
{
    //Retroverse.Story story;

    //public void Start()
    //{
    //    story = new Retroverse.Story();
    //    story.AddSection(
    //        "Intro",
    //        Retroverse.Conversation.Begin()
    //        .Line("You wake from a bad dream in an unfamiliar place")
    //        .Line("As you stare at the ceiling, you remember you're on vacation")
    //        .Line("You are unsure what time it is, but the storm outside makes it hard to sleep")
    //        .Line("You wonder if the tour will be canceled. Better check the laptop")
    //        );
    //    story.AddSection(
    //        "LaptopFirst",
    //        Retroverse.Conversation.Begin()
    //        .Line("No Internet connection", "item")
    //        .Line("I think the router is in the living room downstairs.", "main")
    //    );
    //    story.AddSection(
    //        "Laptop",
    //        Retroverse.Conversation.Begin()
    //        .Line("The internet is still off.", "main")
    //        .Line("I think the router is in the living room downstairs.", "main")
    //    );

    //    story.AddSection(
    //        "WindowFirst",
    //        Retroverse.Conversation.Begin()
    //        .Line("A wet winter storm rages outside.", "main")
    //        .Line("You see the gravestones in the cemetary next door in a flash of lightning.", "main")
    //        .Line("In the flash, you see dark figures scurring amongst the stones.", "main")
    //    );
    //    story.AddSection(
    //        "Window",
    //        Retroverse.Conversation.Begin()
    //        .Line("The storm rages outside.", "main")
    //        .Line("You see the gravestones in the cemetary below in a flash of lightning.")
    //    );

    //    story.AddSection(
    //        "RadioFirst",
    //        Retroverse.Conversation.Begin()
    //        .Line("click...bzz", "item")
    //        .Line("the weather is quite bad for Ireland", "other")
    //        .Line("the hurricane has knocked down power lines all over the island", "other")
    //        .Line("wind gusts over 100 kilometers per...", "other")
    //        .Line("bzz...click", "item")
    //    );
    //    story.AddSection(
    //        "Radio",
    //        Retroverse.Conversation.Begin()
    //        .Line("click...bzz", "item")
    //        .Line("I can't seem to find any stations", "main")
    //    );

    //    RunStory(story, "Intro");
    //}

    //public override void ProcessInput(KeyStoryItem item, Clickable clickable, bool firstClick)
    //{
    //    switch (item)
    //    {
    //        case KeyStoryItem.BEDROOM_LAPTOP:
    //            RunStory(story, firstClick ? "LaptopFirst" : "Laptop");
    //            break;
    //        case KeyStoryItem.BEDROOM_WINDOW:
    //            RunStory(story, firstClick ? "WindowFirst" : "Window");
    //            break;
    //        case KeyStoryItem.BEDROOM_RADIO:
    //            RunStory(story, firstClick ? "RadioFirst" : "Radio");
    //            break;
    //        default: break;
    //    }
    //}
}