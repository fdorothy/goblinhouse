=== garden(_position) ===
{ update_location("Garden", _position) }

-> options

= options

# clickables: clear

 + [{ investigate("catnip", "Catnip") }] -> catnip ->
 + [{ investigate("rosemary", "Rosemary") }] -> rosemary ->
 + [{ investigate("mint", "Mint") }] -> mint ->
 + [{ exit("door", "House") }] -> kitchen("FromGarden")
 - -> options

= catnip
You pick some fresh catnip
{ take("catnip") }
->->

= rosemary
You pick some rosemary.
{ take("rosemary") }
The odor rubs off on your fingers.
->->

= mint
You pick some mint.
{ take("mint") }
The odor rubs off on your fingers.
->->