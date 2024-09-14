=== masterbedroom(_position) ===
{ update_location("MasterBedroom", _position) }
~ music = "house_theme"

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
The lines appear drawn with blood, sand and broken seashells.
The floor boards are broken upwards at the center, as if a beast tore his way up from the basement.
{ ! You hear a ghostly whisper from the opening. }
{ ! "Come on down deary, darkness won't hurt you, but I might." }
{ ! "Hah hah hah" }

 + [Go down]
    { holding == "flashlight":
        You hop down the hole with your flashlight ready.
        -> basement("FromMaster")
      - else:
        You heard something down there, laughing.
        You are too scared to go down without some kind of light.
        You back away from the hole.
    }
 + [Back away]
   You back away from the hole in the floor.
 - ->->

= sheets
Something terrible happened here.
Splattered blood covers the bed sheets.
A trail of blood leads to the pentagram on the floor.
->->