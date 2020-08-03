=== hallway(_position) ===
{ update_location("Hallway", _position, -> hallway) }
{ cmdline == true : -> options }
-> DONE

= options

 + [Your Bedroom] -> option(-> hallway_bedroom_door)
 + [Guest Bedroom] -> option(-> hallway_guest_room)
 + [Stairs Down] -> option(-> hallway_stairs)

=== hallway_guest_room ===

{ ! This is the other guest's room. }
{ ! I think her name is Julia. }

"*knock* *knock*"
...
No answer.

->->

=== hallway_bedroom_door ===
-> bedroom("FromHallway")

=== hallway_stairs ===
-> kitchen("FromUpstairs")

=== hallway_window ===

The storm rages outside.
There is a cemetary below.
You see a payphone at the edge of the cemetary.

->->