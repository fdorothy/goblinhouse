=== hallway(_position) ===
{ update_location("Hallway", _position) }
~ music = "house_theme"
{ ! -> goblin1 -> }
{ garda: { ! -> goblin2 -> } }
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
    { sfx("door_open") }
    -> bedroom("FromHallway")
 + [stand ground]
    You stand your ground, and the laughing fades.
    Was that real, or are you still dreaming?
 - ->->

= goblin2
...
You hear the shrill voice once again.
It sounds angry.
"You won't be wanting to leave us, would you?"
"We were having so much fun together."
"..."
->->

= guestroom_door
{ julia_dead:
    -> guestroom("FromHallway")
- else:
    -> guestroom_door_julia_alive
}
->->

= guestroom_door_julia_alive
This is the other guest's room.
I think her name is Julia.
+ [knock] KNOCK KNOCK
    ...
    The door cracks opens, and Julia peeks out.
    { holding == "machete":
        "What is that you're holding!?"
        "It's...covered in blood!"
        Julia slams the door and locks it.
        ->->
    }
    "I'm hungry. Got anything to eat out there? I like {obj("cookies")}."
    + + [yes]
        { holding == "cookies":
            { sfx("door_open") }
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
{ sfx("door_open") }
-> bedroom("FromHallway") -> DONE

= stairs
-> kitchen("FromUpstairs") -> DONE

-> gameover