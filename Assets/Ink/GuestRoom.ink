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
 + [leave]
 - ->->
 
= julia_ghost
{ ! Julia's body lays on the floor." }
{ ! Her throat is slit open." }
{ ! A ghostly apparition appears above her." }
"Hello again, James."
{ ! "I remembered your name this time." }
{ ! "I can remember a lot of things." }
 - (opts)
 * [what happened to you?]
    "I seem to have died, James."
    "The...things. They came. They had knives, James."
    "I couldn't fend them off."
 * [what is happening]
    "Many things have happened, James."
    "They are using you to kill the ones around you."
    "You've got to get out of here, somehow."
 + { not router_plugged_in } [how do I get out]
    "The {obj("router")}...it's the only way of communication left."
    "They ripped it out of the wall and hid it from us."
    "It is somewhere...I can smell the spot vividly."
    "I can smell the fragrance about it."
    "It smells like {obj("rosemary")}."
    "..."
    ~ router_hint = true
 + { router_plugged_in } [now what] 
    "Use your laptop, James."
    "Use it to leave this place."
 + [leave]
    ->->
 - -> opts

= clothes
{ rainboots: You've already taken the raincoat. }
{ not rainboots: -> see_rainboots -> }
->->

= see_rainboots
Julia's raincoat is on the floor here.
 + [put on raincoat]
    You put the raincoat on.
    ~ rainboots = true
 + [leave] ...
 - ->->