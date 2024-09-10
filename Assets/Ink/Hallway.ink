=== hallway(_position) ===
{ update_location("Hallway", _position) }
{ ! Everyone is asleep. }
{ ! I should be quiet. }
-> options

= options

# clickables: clear

 + [{exit("bedroom", "Your Bedroom")}] -> bedroom_door ->
 + [{exit("guestroom", "Guest Bedroom")}] -> guestroom_door ->
 + [{exit("stairs", "Stairs Down")}] -> stairs ->
 + [{investigate("window", "Window")}] -> bedroom.window ->
 - -> options

= guestroom_door
This is the other guest's room.
I think her name is Julia.
+ [knock] KNOCK KNOCK
    ...
    The door cracks opens, and Julia peeks out.
    "I'm hungry. Got anything to eat out there?"
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