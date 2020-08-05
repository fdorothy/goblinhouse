=== kitchen(_position) ===
{ update_location("Kitchen", _position) }

{ ! Everyone is probably asleep. }
{ ! I should be quiet. }

-> options

= options

 + [Upstairs] <- kitchen_upstairs
 + [Garden] <- kitchen_garden
 + [Living Room] <- kitchen_livingroom
 - -> options
 
=== kitchen_upstairs ===
-> hallway("FromKitchen")

=== kitchen_garden ===
It's storming too much to go out to the garden.
-> DONE

=== kitchen_livingroom ===
It is too dark down there, and I don't have a flashlight.
-> DONE

