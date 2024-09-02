=== guestroom(_position) ===
{ update_location("GuestRoom", _position) }

-> options

= options

# clickables: clear

 + [{ investigate("julia", "Julia") }] -> julia ->
 + [{ investigate("clothes", "Clothes") }] -> clothes ->
 + [{ exit("door", "Leave") }] -> hallway("FromGuestRoom")
 - -> options

= julia
"Hello, did you notice the power is out?"
->->

= clothes
Julia's clothes are scattered on the floor here.
{ not rainboots: -> see_rainboots -> }
->->

= see_rainboots
A pair of rainboots catch your eye.
 + [put on boots]
    You pull the rainboots on your feet.
    ~ rainboots = true
 + [leave] ...
 - ->->