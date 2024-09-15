=== garden(_position) ===
{ update_location("Garden", _position) }
~ music = "outdoor_theme"

-> options

= options

# clickables: clear

 + [{ investigate("catnip", "Catnip") }] -> catnip ->
 + [{ investigate("rosemary", "Rosemary") }] -> rosemary ->
 + [{ investigate("mint", "Mint") }] -> mint ->
 + [{ exit("door", "House") }]
    { sfx("door_open") }
    -> kitchen("FromGarden")
 - -> options

= catnip
{ router_hint:
    -> catnip_investigate ->
- else:
    You pick some fresh catnip
    { take("catnip") }
}
->->

= catnip_investigate
 + [search]
    You search through the catnip plants.
    All you find is wet dirt and bugs.
 + [pick]
    You pick some fresh catnip
    { take("catnip") }
 - ->->

= rosemary
{ router_hint:
    -> rosemary_investigate ->
- else:
    You pick some rosemary.
    { take("rosemary") }
    The odor rubs off on your fingers.
}
->->

= rosemary_investigate
 + [search]
    You search through the rosemary plants.
    You find a electronic device, half buried in the dirt.
    It appears to be the router.
    + + [take it]
        You take the router, brushing off the dirt.
        "I hope it still works after being in the rain."
        ~ have_router = true
    + + [leave it]
 + [pick]
    You pick some fresh rosemary
    { take("rosemary") }
 - ->->

= mint
{ router_hint:
    -> mint_investigate ->
- else:
    You pick some mint.
    { take("mint") }
    The odor rubs off on your fingers.
}
->->

= mint_investigate
 + [search]
    You search through the mint plants.
    All you find is wet dirt and bugs.
 + [pick]
    You pick some fresh mint
    { take("mint") }
 - ->->
