=== guestroom(_position) ===
{ update_location("GuestRoom", _position) }
~ music = "house_theme"

-> options

= options

# clickables: clear

 + { not julia_dead } [{ investigate("julia", "Julia") }] -> julia ->
 + { julia_dead } [{ investigate("julia_ghost", "Julia's Ghost") }] -> julia_ghost ->
 + [{ investigate("clothes", "Rain gear") }] -> clothes ->
 + [{ exit("door", "Leave") }]
    { sfx("door_open") }
    -> hallway("FromGuestRoom")
 - -> options

= julia
"Hello again...what was your name?"
 + [James]
 -
"Right, James..." Julia remembers.
"I heard some horrible sounds earlier. What was that all about?"
 + [I was asleep]
    "I see, you were asleep?"
    "How did you sleep through all that?"
    "...and I thought I heard your voice."
 + { found_body } [A murder]
    "The landlord has been murdered!?"
    "This is horrible, we've got to call the police."
    "I'm getting no reception"
    "But I think there's a pay phone outside."
 + [leave] ->->
 - ->->
 
= julia_ghost
"Hello again, James."
{ ! "I remembered your name this time." }
{ ! "I can remember a lot of things." }
 + [what is happening]
    "Many things have happened, James."
    "They are using you to kill the ones around you."
    "You've got to get out of here, somehow."
 + [how do I get out]
    "The router...it's the only way of communication left."
    "They ripped it out of the wall and hid it from us."
    "It is somewhere...I can smell the spot vividly."
    "I can smell the fragrance about it."
    "..."
    ~ router_hint = true
 + [leave]
 - ->->

= clothes
{ rainboots: You've already taken the raincoat. }
{ not rainboots: -> see_rainboots -> }
->->

= see_rainboots
Julia's rain coat is on the floor here.
 + [put on raincoat]
    You put the raincoat on.
    ~ rainboots = true
 + [leave] ...
 - ->->