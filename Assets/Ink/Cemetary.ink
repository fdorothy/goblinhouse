=== cemetary(_position) ===
{ update_location("Cemetary", _position) }

-> options

= options

# clickables: clear

 + [{ investigate("dirt", "Dirt") }] -> investigate_dirt ->
 + [{ exit("door", "House") }] -> livingroom("FromOutside")
 + [{ exit("shed", "Shed") }] -> shed("FromOutside")
 + [{ exit("road", "Road") }] -> road("FromCemetary")
 - -> options

= investigate_dirt
{ holding == "shovel":
    -> find_coffin ->
 - else:
    You claw through the dirt.
    You are panting, there is too much dirt for your bare hands.
 }
 ->->
 
 = find_coffin
{ ! You dig for a while with the shovel. }
You strike something hard.
+ [keep digging]
    You keep on digging.
    The outline of a coffin is visible.
    + + [open coffin]
        You open the coffin.
        A recently deceased man lays here.
        Two coins cover the eyes.
        + + + [take coins]
            You take the coins and put them in your pocket.
            ~ coins = true
        + + + [stop]
    + + [stop]
+ [stop]
- ->->