=== hallway(_position) ===
{ update_location("Hallway", _position) }
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
"*knock* *knock*"
...
The door cracks opens, and Julia peeks out.
"I'm hungry. Got anything to eat out there?"
+ [yes]
    { holding == "cookies":
        -> guestroom("FromHallway")
    - else:
        "Bullshit, you don't have anything"
        Julia slams the door shut.
    }
+ [no]
    "..."
    Julia closes and locks the door.
- ->->

= bedroom_door
-> bedroom("FromHallway") -> DONE

= stairs
-> kitchen("FromUpstairs") -> DONE

= stairs_death

You fall down the stairs in the darkness and break your neck.
-> gameover