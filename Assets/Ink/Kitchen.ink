=== kitchen(_position) ===
{ update_location("Kitchen", _position) }
{ ! You walk down the stairs to the kitchen. }
{ ! You think you hear the sound of a mouse in the cabinets. }
-> options

= options

# clickables: clear

 + [{ exit("stairs", "Upstairs") }] -> hallway("FromKitchen")
 + [{ exit("garden", "Decrepit Garden") }] -> gardendoor ->
 + [{ exit("livingroom", "Living Room") }] -> livingroom("FromKitchen")
 + [{ investigate("counter", "Counter Top") }] -> countertop ->
 - -> options

= gardendoor
It's storming too much to go out to the garden.
->->

= countertop
There are several items sitting on the countertop.
You can only take one of them at a time.

 + [flashlight]
   { take("flashlight") }
 + [cookies]
   { take("cookies") }
 + [cat treats]
   { take("treats") }
 - ->->
 
 