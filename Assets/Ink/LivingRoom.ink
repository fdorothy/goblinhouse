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
{ wires == 0:
    This is the landlords' room.
    I shouldn't bother them in the middle of the night.
    I should just find and reset the router.
  - else:
    -> masterbedroom("FromLivingRoom")
}
->->

= basement
It is much too dark to go down there.
->->

= frontdoor
{ wires == 0:
    It's storming too much to go outside.
  - else:
    The door won't budge.
    Odd, why would they lock it from the outside?
    Isn't that a big fire hazard?
}
->->

= wires
{ ! Wires protrude from the wall where the router once was. }
{ ! I guess I won't be getting online any time soon. }
Maybe the landlords know what is going on.
{ wires > 1: I should ask them. Their room was on the first floor. }
->->