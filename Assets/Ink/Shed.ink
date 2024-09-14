=== shed(_position) ===
{ update_location("Shed", _position) }
~ music = "outdoor_theme"

-> options

= options

# clickables: clear

 + [{ investigate("toolbench", "Toolbench") }] -> toolbench ->
 + [{ exit("door", "Go Outside") }] -> cemetary("FromShed")
 - -> options

= toolbench
There are several tools here.
You can only hold one item.
What would you like to take?
+ [shovel]
    You take the shovel.
    ~ holding = "shovel"
+ [machete]
    You take the machete.
    { ! -> goblin1 -> }
    ~ holding = "machete"
+ [leave]
- ->->

= goblin1
You hear a scratchy voice coming from everywhere in the shed.
Its sound bouncing off the walls.
"There it is, the murder weapon. The machete."
"But you already knew that, didn't you?"
"Murderer, murderer, murderer!"
"Hah hah hah"
The voice stops as the laughter echoes off the walls.
->->