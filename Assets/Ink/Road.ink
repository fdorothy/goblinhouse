=== road(_position) ===
{ update_location("Road", _position) }
~ music = "outdoor_theme"
{ ! -> goblin1 -> }

-> options

= options

# clickables: clear

 + [{ investigate("phone", "Phone") }] -> phone ->
 + [{ exit("cemetary", "Cemetary") }] -> cemetary("FromRoad")
 - -> options

= phone
{ sfx("door_open") }
You pick up the receiver
{ inserted_coins:
    There is a dial tone
- else:
    There is no dial tone
}
-> phone_options

= phone_options
{ dialed_taxi and dialed_police: There's no one to call. }
* { coins } [insert coins]
    ~ inserted_coins = true
    You insert coins
    You hear a dial tone from the receiver.
    -> phone_options ->
* { inserted_coins } [dial taxi]
    -> taxi ->
* [dial police]
    -> police ->
+ [hang up]
    You hang up the receiver.
- ->->

= police
~ dialed_police = true
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
~ dialed_taxi = true
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
 + [fight]
    You punch the man in his face.
    Blood splatters from his nose.
    He screams at you "get out!"
    + + [get out of car]
        You unlock the car door and fall out onto the street.
        You scrape your palms on the cold wet concrete.
        The car revs its engine and peels away into the rain.
        You are alone again on the street.
 + [get out of car]
    In a panic you unlock the car door and fall out onto the street.
    "What's the matter with you?" the driver says in a scratchy voice.
    + + ["get away from me!"]
        The driver grunts and puts the car in drive.
        He drives away into the rain.
        You are alone again on the street.
    + + ["help!"]
        The driver grunts, "you're insane, you know."
        He closes the door and drives off into the rain.
        You are alone again on the street.
 - ->->

= goblin1
You walk out onto the road.
You see a phone booth down the street.
{ sfx("laugh") }
A small figure stands atop it, and it yells out to you.
"Ah, running away are we?
"Scared of the consequences of your actions?"
"Murderer, murderer, murderer"
{ sfx("laugh") }
"Hah hah hah"
The thing jumps down from atop the phone booth.
It scurries away into the darkness.
->->