=== kitchen(_position) ===
{ update_location("Kitchen", _position) }
{ ! -> goblin1 -> }
-> options

= options

# clickables: clear

 + [{ exit("stairs", "Upstairs") }] -> hallway("FromKitchen")
 + [{ exit("garden", "Decrepit Garden") }] -> gardendoor ->
 + [{ exit("livingroom", "Living Room") }] -> livingroom("FromKitchen")
 + [{ investigate("cabinets", "Cabinets") }] -> cabinets ->
 + [{ investigate("flashlight", "Flashlight") }] -> flashlight ->
 + [{ investigate("oven", "Oven") }] -> oven ->
 - -> options
 
= goblin1
You walk down the stairs into the kitchen.
You hear a scratchy voice coming from the cabinets.
"You know the girl is next, don't you?"
"She knows too much"
"Hah hah hah"
->->

= gardendoor
{ rainboots:
    -> garden("FromKitchen")
- else:
    It's storming too much to go out to the garden.
}
->->

= cabinets
There are several boxes of mix in the cabinets.
You can only take one of them at a time.

 + [cookie mix]
   { take("cookie mix") }
 + [brownie mix]
   { take("brownie mix") }
 + [pumpkin cake mix]
   { take("pumpkin cake mix") }
 - ->->

= oven
{ ! Odd, the oven is already preheated to just the right temperature for baking. }
 + { holding == "cookie mix" || holding == "brownie mix" || holding == "pumpkin cake mix" } [bake { holding }]
    You mix up and bake the { holding }.
    ...
    { holding == "cookie mix":
      ~ holding = "cookies"
    }
    { holding == "brownie mix": 
      ~ holding = "brownies"
    }
    { holding == "pumpkin cake mix": 
      ~ holding = "pumpkin cake"
    }
    They're done. You let the { holding } cool down and take them out of the oven.
+ [leave]
 - ->->
 
= flashlight
There is a flashlight sitting on the counter.
You can only take one of item at a time.
+ [take flashlight]
    You take the flash light
    { take("flashlight") }
+ [leave it]
- ->->