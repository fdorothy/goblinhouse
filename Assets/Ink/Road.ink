=== road(_position) ===
{ update_location("Road", _position) }

-> options

= options

# clickables: clear

 + [{ investigate("phone", "Phone") }] -> phone ->
 + [{ exit("cemetary", "Cemetary") }] -> cemetary("FromRoad")
 - -> options

= phone
{ ! -> goblin1 -> }
You pick up the receiver
{ not inserted_coins: There is no dial tone }
+ { coins } [insert coins]
    ~ inserted_coins = true
    You insert coins
    You hear a dial tone from the receiver.
    + + [dial police]
        You dial the police.
        -> police
    + + [dial taxi]
        -> taxi
+ { inserted_coins } [dial taxi]
    -> taxi
+ [dial police]
    You dial the police.
    -> police
+ [hang up]
- ->->

= police
The police show up shortly after your call.
The police question you and Julie.
They find you suspicious, with your blood and rain soaked clothes.
They put you in handcuffs and stuff you into the back of the police car.
-> win

= taxi
You dial for a taxi.
...
The taxi arrives on the dark street.
You climb in.
The driver gives you an odd look.
"Where to?"
-> win

= goblin1
You enter the phone booth.
You hear the scratchy voice from above.
Something is on top of the phone booth.
"Ah, running away are we?
"Scared of the consequences of your actions?"
"Murderer, murderer, murderer"
"Hah hah hah"
Something jumps down from atop the phone booth.
It scurries away into the darkness.
->->