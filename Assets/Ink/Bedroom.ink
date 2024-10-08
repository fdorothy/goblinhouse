=== bedroom(_position) ===
{ update_location("Bedroom", _position) }
~ music = "house_theme"
-> options

= options

# clickables: clear

 + [{investigate("laptop", "Laptop")}] -> laptop ->
 + [{investigate("window", "Window")}] -> window ->
 + [{investigate("bearhead", "Odd Decor")}] -> bearhead ->
 + [{exit("door", "Leave")}] -> leave_bedroom ->
 - -> options
 
= bearhead
A bear head?
The owners must be hunters.
+ [touch it]
    The fur is surprisingly bristly.
    Not at all like the teddy bears of your youth.
    You see something in its mouth.
    + + [take it]
        It's a note.
        + + + [read it]
            "Hope not to see Heaven"
            "I have come to lead you to the other shore"
            "into eternal darkness"
            "into fire and into ice."
        + + + [leave it]
    + + [leave it]
+ [leave it]
- ->->

= leave_bedroom

{ investigated_laptop:
    { sfx("door_open") }
    -> hallway("FromBedroom")
 - else:
    You go to open the door, but think twice.
    You should check the online tour website first.
}
->->

= laptop
You're looking at the laptop's login screen. The time reads 12:53AM.
 + [login]
    You type on the laptop.
    Login name: jhenry, password: ********
    The computer screen flickers green as you stare at what you were last working on.
    -> operate_laptop ->
 + [leave]
 - ->->

= operate_laptop
+ [read work notes]
    You open and read your work notes.
    Merge GAL-3421, contact Jim about API errors, scrum @ 8:45AM
    -> operate_laptop
+ [read journal]
    To talk about with therapist:
    \- Hearing the voices again
    \- Waking up in strange places
    \- Lovely trip, hoping to see puffins tomorrow
    -> operate_laptop
+ [open browser]
    { router_plugged_in:
        You open the browser, and an email inbox pops up.
        { ! "There is internet!" you say, relieved. }
        -> browser_internet ->
    - else:
        -> browser_no_internet ->
    }
    -> operate_laptop
+ [logout]
    "That's enough for now"
    You logout of the laptop.
 - ->->

= browser_no_internet
You open the browser, and stare at a blank page.
There is no internet.
{ livingroom.wires == 0: I think the router is in the living room downstairs. Maybe I can reset it? }
{ livingroom.wires > 0: With the router missing there is no hope to get online. }
~ investigated_laptop = true
->->

= browser_internet
 + [email someone]
    Who would you like to email?
    * * [therapist]
        You send an email to your therapist.
        ...
        No response.
        "It's 1 AM, what did I expect?"
        -> browser_internet ->
    * * [friend]
        You send an email to your friend.
        ...
        You see a response.
        * * * [read it]
            "Hey man, I got your email."
            "I don't know all the details,"
            "But it sounds like a bad situation."
            "Have you tried emailing the police?"
        - - -
        -> browser_internet ->
    * * [police]
        You send an email to the police.
        ...
        You see a response.
        * * * [read it]
            "We are sending Garda over now."
            "Please meet them by the road outside the house."
            ~ garda = true
        - - -
        -> browser_internet ->
    + + [nevermind]
    - -
        ->->
 + [close browser]
 - ->->

= window
The storm rages outside.
{ window == 1 : You see the gravestones in the cemetary outside in a flash of lightning.}
{ window == 1 : In the flash, you see dark figures scurring amongst the stones.}
 + [open window]
   You slide the window open.
   The humid air seeps into the room.
    + + [crawl out the window]
        You crawl out the window.
        You are now perched on a thin slant of roof.
        The rain poors down around you.
        + + + [jump down]
            You move your legs to jump, but chicken out last minute.
            You crawl back inside the house and close the window, leaving the precipice behind.
        + + + [go back inside]
            You crawl back inside the window, leaving the precipice behind.
    + + [close window]
 + [nevermind]
 - ->->