=== cemetary(_position) ===
{ update_location("Cemetary", _position) }
~ music = "outdoor_theme"
{ ! -> goblin1 -> }
-> options

= options

# clickables: clear

 + [{ investigate("dirt", "Dirt") }] -> investigate_dirt ->
 + [{ exit("door", "House") }] -> livingroom("FromOutside")
 + [{ exit("shed", "Shed") }] -> shed("FromOutside")
 + [{ exit("road", "Road") }] -> road("FromCemetary")
 - -> options
 
= goblin1
You walk out the door and down the path to the cemetary on the grounds.
You see a shape standing amongst the cemetary stones.
Its long face turns to look at you as it slowly raises a knife in the air.
Suddenly in a flash of lightning, the shape is gone.
The cemetary is yours to explore.
->->

= investigate_dirt
{ holding == "shovel":
    -> find_coffin ->
 - else:
    -> find_nothing_in_dirt ->
 }
 ->->
 
 = find_nothing_in_dirt
    You claw through the dirt.
    You are panting, there is too much dirt for your bare hands.
  + [keep digging]
    You wipe the sweat from your forehead.
    -> find_nothing_in_dirt
  + [leave]
  - ->->
 
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