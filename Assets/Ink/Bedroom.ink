=== bedroom(_position) ===
{ update_location("Bedroom", _position, -> bedroom) }

{ cmdline == true : -> options }
-> DONE

= options

 + [Laptop] -> option(-> bedroom_laptop)
 + [Window] -> option(-> bedroom_window)
 + [Radio] -> option(-> bedroom_radio)
 + [Door] -> option(-> bedroom_door)
 - -> DONE

=== bedroom_laptop ===
{ bedroom_laptop == 1 : No Internet connection}
{ bedroom_laptop != 1 : Still no internet.}
I think the router is in the living room downstairs.
->->

=== bedroom_window ===
The storm rages outside.
{ bedroom_window == 1 : You see the gravestones in the cemetary next door in a flash of lightning.}
{ bedroom_window == 1 : In the flash, you see dark figures scurring amongst the stones.}
->->

=== bedroom_radio ===
{! -> first}
->->

= first
click...bzz
the weather is quite bad for Ireland
the hurricane has knocked down power lines all over the island
wind gusts over 100 kilometers per...
bzz...click
->->

= normal
click...bzz
I can't seem to find any stations
->->

=== bedroom_bearhead ===
A bear head?
The owners must be hunters.
->->

=== bedroom_door ===
-> hallway("FromBedroom")
