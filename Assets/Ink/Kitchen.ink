=== kitchen(_position) ===
{ update_location("Kitchen", _position, -> kitchen) }

{ ! Everyone is probably asleep. }
{ ! I should be quiet. }

~ location = -> kitchen
{ cmdline == true : -> options }
-> DONE

= options

 + [Upstairs] -> option(-> kitchen_upstairs)
 + [Garden] -> option(-> kitchen_garden)
 + [Living Room] -> option(-> kitchen_livingroom)

=== kitchen_upstairs ===
-> hallway("FromKitchen")

=== kitchen_garden ===
It's storming too much to go out to the garden.
->->

=== kitchen_livingroom ===
It is too dark down there, and I don't have a flashlight.
->->

