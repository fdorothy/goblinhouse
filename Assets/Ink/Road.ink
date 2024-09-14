=== road(_position) ===
{ update_location("Road", _position) }
~ music = "outdoor_theme"

-> options

= options

# clickables: clear

 + [{ investigate("phone", "Phone") }] -> phone ->
 + [{ exit("cemetary", "Cemetary") }] -> cemetary("FromRoad")
 - -> options

= phone
{ sfx("door_open") }
{ ! -> goblin1 -> }
You pick up the receiver
{ not inserted_coins: There is no dial tone }
+ { coins } [insert coins]
    ~ inserted_coins = true
    You insert coins
    You hear a dial tone from the receiver.
    + + [dial police]
        -> police ->
    + + [dial taxi]
        -> taxi ->
+ { inserted_coins } [dial taxi]
    -> taxi ->
+ [dial police]
    -> police ->
+ [hang up]
- ->->

= police
You dial the police.
You clear your throat, "hello, police? I'd like to report a murder."
...
"Hello? Is there anyone there?" you say.
...
A shrill voice is heard from the receiver.
{ sfx("laugh") }
"Hah hah hah"
You quickly hang up.
->->

= taxi
You dial for a taxi.
...
The taxi arrives on the dark street.
The driver stares ahead as you get into the car.
Where do you want to tell the driver to go?
 + [airport]
    You tell him to go to the airport.
 + [downtown]
    You tell him to go downtown.
 -
He doesn't respond.
 + [excuse me?]
    "Excuse me, did you hear me?"
 + [leave car]
    You try to open the car door, but it's locked.
 -
The driver slowly turns his head.
His nose is long. Longer than it should be.
His skin is green.
He stares at you and laughs.
{ sfx("laugh") }
"Hah hah hah"
-> win

= goblin1
You enter the phone booth.
{ sfx("laugh") }
You hear the scratchy voice from above.
Something is on top of the phone booth.
"Ah, running away are we?
"Scared of the consequences of your actions?"
"Murderer, murderer, murderer"
{ sfx("laugh") }
"Hah hah hah"
Something jumps down from atop the phone booth.
It scurries away into the darkness.
->->