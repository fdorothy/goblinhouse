=== bedroom(_position) ===
{ update_location("Bedroom", _position) }
-> options

= options

# clickables: clear

 + [{investigate("laptop", "Laptop")}] -> laptop ->
 + [{investigate("window", "Window")}] -> window ->
 + [{investigate("radio", "An Old Radio")}] -> radio ->
 + [{investigate("bearhead", "Odd Decor")}]
    A bear head?
    The owners must be hunters.
 + [{exit("door", "Leave")}] -> leave_bedroom ->
 - -> options
 
= leave_bedroom

{ laptop:
    -> hallway("FromBedroom")
 - else:
    You go to open the door, but think twice.
    You should check your laptop.
}
->->

= laptop
{ laptop == 1 : No Internet connection}
{ laptop != 1 : Still no internet.}
I think the router is in the living room downstairs.
->->

= window
The storm rages outside.
{ window == 1 : You see the gravestones in the cemetary next door in a flash of lightning.}
{ window == 1 : In the flash, you see dark figures scurring amongst the stones.}
->->

= radio
{! -> radio_first ->->}
{radio != 1 : -> radio_normal ->->}
->->

= radio_first
click...bzz
the weather is quite bad for Ireland
the hurricane has knocked down power lines all over the island
wind gusts over 100 kilometers per...
bzz...click
->->

= radio_normal
click...bzz
I can't seem to find any stations
->->
