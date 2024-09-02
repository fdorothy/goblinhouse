=== road(_position) ===
{ update_location("Road", _position) }

-> options

= options

# clickables: clear

 + [{ investigate("phone", "Phone") }] -> phone ->
 + [{ exit("cemetary", "Cemetary") }] -> cemetary("FromRoad")
 - -> options

= phone
{ coins:
    -> can_dial ->
    ->->
- else:
    You pick up the receiver
    There is no dial tone.
    ->->
}

= can_dial
You pick up the receiver
There is no dial tone
+ [insert coins]
    You insert coins
    You hear a dial tone from the receiver.
    + + [dial police]
        You dial the police.
        -> police
    + + [hang up]
        You hang up.
        The coins a returned.
        You put the coins in your pocket.
+ [hang up]
- ->->

= police
The police show up shortly after your call.
They put you on an ambulance.
You made it out alive.
Congratulations.
-> win