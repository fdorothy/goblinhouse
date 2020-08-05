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
{ ! This is the other guest's room. }
{ ! I think her name is Julia. }
"*knock* *knock*"
...
No answer.
->->

= bedroom_door
-> bedroom("FromHallway") -> DONE

= stairs
-> kitchen("FromUpstairs") -> DONE
