=== guestroom(_position) ===
{ update_location("GuestRoom", _position) }

-> options

= options

# clickables: clear

 + [{ investigate("julia", "Julia") }] -> julia ->
 + [{ investigate("clothes", "Rain gear") }] -> clothes ->
 + [{ exit("door", "Leave") }] -> hallway("FromGuestRoom")
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