=== shed(_position) ===
{ update_location("Shed", _position) }

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
    ~ holding = "machete"
+ [leave]
- ->->