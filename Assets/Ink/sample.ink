VAR cmdline = true

-> bedroom

=== bedroom ===

{ ! You wake up as the world around you spins. }

{ cmdline : -> options}

-> DONE

== options

You are in the bedroom, what would you like to do?

 + [Check Window] -> check_window ->
 + [Leave] -> leave
 - -> bedroom

== check_window

It is storming outside. You see a garden and beyond that, a graveyard below.

{ ! Lightning flashses, and you see dark figures among the gravestones. }

->->

== leave

You open the bedroom door and walk out.
-> hallway


=== hallway ===

You are standing in the hallway. There are two doors here, one to your room and one to another guest room. There are stairs that lead down.

 + [Go to your room] -> your_room
 + [Guest room] -> guest_room ->
 + [Go down the stairs] -> stairs
 - -> hallway

== your_room

You open the door to your room and enter.
-> bedroom

== guest_room

You attempt to open the door to the guest room, but it is locked.
->->

== stairs

You go down the stairs.
-> kitchen

=== kitchen ===

You are standing in a kitchen.

-> DONE