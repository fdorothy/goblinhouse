=== hallway(_position) ===
{ update_location("Hallway", _position) }
~ music = "house_theme"
{ ! -> goblin1 -> }
-> options

= options

# clickables: clear

 + [{exit("bedroom", "Your Bedroom")}] -> bedroom_door ->
 + [{exit("guestroom", "Guest Bedroom")}] -> guestroom_door ->
 + [{exit("stairs", "Stairs Down")}] -> stairs ->
 + [{investigate("window", "Window")}] -> bedroom.window ->
 - -> options

= goblin1
{ sfx("laugh") }
...
You hear the shrill voice from your dreams.
It is coming from the wall.
"The man..."
"...he screamed in pain, didn't he, as we cut him down?"
{ sfx("laugh") }
"Hah hah hah!"
 + [flee back to bedroom]
    You jump back into the bedroom and slam the door.
    Sweat beads on your forehead.
    What was that?
    -> bedroom("FromHallway")
 + [stand ground]
    You stand your ground, and the laughing fades.
    Was that real, or are you still dreaming?
 - ->->

= guestroom_door
This is the other guest's room.
I think her name is Julia.
+ [knock] KNOCK KNOCK
    ...
    The door cracks opens, and Julia peeks out.
    "I'm hungry. Got anything to eat out there? I like cookies."
    + + [yes]
        { holding == "cookies":
            -> guestroom("FromHallway")
        - else:
            "Bullshit, you don't have anything"
            Julia slams the door shut.
        }
    + + [no]
        "..."
        Julia closes and locks the door.
    + + { masterbedroom } [there's been a murder]
        "Do I look like the police?"
        Julia slams the door shut.
+ [leave]
- ->->

= bedroom_door
-> bedroom("FromHallway") -> DONE

= stairs
-> kitchen("FromUpstairs") -> DONE

-> gameover