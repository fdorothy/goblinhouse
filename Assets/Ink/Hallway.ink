=== hallway(_position) ===
{ update_location("Hallway", _position) }
-> options

= options

 + [{exit("bedroom", "Your Bedroom")}] -> bedroom_door
 + [{exit("guestroom", "Guest Bedroom")}] <- guestroom_door
 + [{exit("stairs", "Stairs Down")}] -> stairs
 + [{investigate("window", "Window")}] -> window
 - -> options

= guestroom_door
{ ! This is the other guest's room. }
{ ! I think her name is Julia. }
"*knock* *knock*"
...
No answer.
-> DONE

= bedroom_door
-> bedroom("FromHallway")

= stairs
-> kitchen("FromUpstairs")

= window
The storm rages outside.
There is a cemetary below.
You see a payphone at the edge of the cemetary.
-> DONE