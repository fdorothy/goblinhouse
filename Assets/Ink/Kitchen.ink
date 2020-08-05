=== kitchen(_position) ===
{ update_location("Kitchen", _position) }

{ ! Everyone is asleep. }
{ ! I should be quiet. }

-> options

= options

# clickables: clear

 + [{ exit("stairs", "Upstairs") }] -> hallway("FromKitchen")
 + [{ exit("garden", "Decrepit Garden") }] -> gardendoor ->
 + [{ exit("livingroom", "Living Room") }] -> livingroom("FromKitchen")
 - -> options

= gardendoor
It's storming too much to go out to the garden.
->->