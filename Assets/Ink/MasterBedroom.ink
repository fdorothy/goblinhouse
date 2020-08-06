=== masterbedroom(_position) ===
{ update_location("MasterBedroom", _position) }

{ ! -> first_entrance -> }

-> options

= first_entrance
The bedroom is darkly lit, and the landlords are no where to be found.
A pentagram is drawn on the floor, with candles still burning at the corners.
->->

= options
# clickables: clear
 + [{ investigate("pentagram", "Dark Symbol") }] -> pentagram ->
 + [{ investigate("bed", "Bloody Sheets") }] -> sheets ->
 + [{ exit("door", "Door") }] -> livingroom("FromMaster")
 - -> options
 
= pentagram
The lines appear drawn with sand and broken seashells.
The floor boards are broken upwards at the center, as if a beast tore his way up from the basement.
{ ! You hear a ghostly whisper from the opening. }

 + [Go down]
   You hop down the hole.
   It is dark down here.
   You cannot see an exit.
   + + [Light a match]
     You fumble around your pockets for a match.
     You feel something touching your leg.
     + + + [Scream]
     + + + [Keep Calm]
       You find and light the match.
       You see the killer's face, ugly in the match's light.
   + + [Cry for help]
     You yell, but no one hears you.
     You feel something touching your leg.
     + + + [Scream]
   - -
     Oh, god!
     -> gameover
 + [Back away]
   You back away from the hole in the floor.
 - ->->

= sheets
Something terrible happened here.
Splattered blood covers the bed sheets.
A trail of blood leads to the pentagram on the floor.
->->