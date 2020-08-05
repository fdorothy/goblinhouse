=== livingroom(_position) ===
{ update_location("LivingRoom", _position) }

{ ! The router should be around here somewhere. }
{ ! I should be quiet to not wake up the landlord. }

-> options

= options

# clickables: clear

 + [{ investigate("router", "Wires") }] -> wires ->
 + [{ exit("kitchendoor", "Kitchen") }] -> kitchen("FromLivingRoom")
 + [{ exit("frontdoor", "Front Door") }] -> frontdoor ->
 + [{ exit("masterdoor", "Landlord's Bedroom") }] -> masterdoor ->
 + [{ exit("basement", "Dark Basement") }] -> basement ->
 - -> options
 
= masterdoor
The door is locked.
->->

= basement
It is much too dark to go down there.
->->

= frontdoor
The door is locked.
->->

= wires
{ ! Wires protrude from the wall where the router once was. }
{ ! I guess I won't be getting online any time soon. }
Maybe the landlords know what is going on.
{ wires > 1: I should ask them. Their room was on the first floor. }
->->