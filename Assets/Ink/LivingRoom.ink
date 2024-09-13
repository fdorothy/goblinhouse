=== livingroom(_position) ===
{ update_location("LivingRoom", _position) }

{ ! The router should be around here somewhere. }
{ ! I should be quiet to not wake up the landlord. }

-> options

= options

# clickables: clear

 + [{ investigate("router", "Wires") }] -> wires ->
 + [{ investigate("cat", "Cat") }] -> cat ->
 + [{ investigate("radio", "An Old Radio") }] -> radio ->
 + [{ exit("kitchendoor", "Kitchen") }] -> kitchen("FromLivingRoom")
 + [{ exit("frontdoor", "Front Door") }] -> frontdoor ->
 + [{ exit("masterdoor", "Landlord's Bedroom") }] -> masterdoor ->
 - -> options
 
= masterdoor
{ wires == 0:
    This is the landlords' room.
    I shouldn't bother them in the middle of the night.
    I should just find and reset the router.
  - else:
    -> masterdoor_check_lock
}
->->

= masterdoor_check_lock
{ not master_unlocked: The door is locked }
{ master_unlocked: -> masterbedroom("FromLivingRoom") }
+ { master_key } [use cat's key]
    ~ master_unlocked = true
    You use the cat's key to unlock the master bedroom door.
    + + [enter room]
        -> masterbedroom("FromLivingRoom")
    + + [leave]
+ [knock]
    KNOCK KNOCK
    ...
    No response
+ [leave]
- ->->

= frontdoor
{ front_door_unlocked: -> go_outside }
{ not front_door_unlocked: The door has a pad lock on it. Is that safe? }
+ { house_key } [use padlock key]
    You take out the key you got from the landlord's body from your pocket.
    You open the padlock
    ~ front_door_unlocked = true
    + + [go outside]
        -> go_outside ->
    + + [stay inside]
+ [nevermind]
- ->->

= go_outside
{ rainboots:
    -> cemetary("FromHouse")
- else:
    It's raining too much and there is mud all over the ground.
    You can't go out there without some rainboots.
    ->->
}

= wires
{ ! Wires protrude from the wall where the router once was. }
{ ! I guess I won't be getting online any time soon. }
Maybe the landlords know what is going on.
{ wires > 1: I should ask them. Their room was on the first floor. }
->->

= cat
{ holding == "catnip":
    -> caught_the_cat ->
  - else:
    -> cat_slips_away ->
}
->->

= caught_the_cat
You catch the cat in your hands.
He struggles, but he smells the catnip and stays in your arms.
~ holding = ""
{ not master_key:
    -> taking_the_key_from_cat ->
 - else:
    The cat has nothing on them.
    + [Put down the cat] ->->
}
->->

= taking_the_key_from_cat
The cat has a key on his collar.
 + [take key] ->
    You take the key
    ~ master_key = true
 + [leave]
 - ->->
 
= cat_slips_away
You struggle to catch the cat with your hands.
"Hiss"
The cat slips away
->->

= radio
It's an old FM radio.
 + [turn on]
    You turn on the radio.
    click...bzz
    Just static. There are some stations that it can be tuned to.
    -> operate_radio
 + [leave it]
    You leave the radio alone.
 - ->->

= operate_radio
 + [90.3]
    click...bzz
    the weather is quite bad for Ireland
    the ex-tropical storm has knocked down power lines all over the island
    wind gusts over 100 kilometers per...
    bzz...click
 + [94.6]
    Droning music plays.
    You cannot place the melody.
 + [106.9]
    Nothing but static plays
 + [97.3]
    click...bzz
    A preacher is on, giving a sermon in a stern tone.
    "You belong to your father, the devil, and you want to carry out your father’s desires."
    "He was a murderer from the beginning, not holding to the truth, for there is no truth in him."
    "When he lies, he speaks his native language, for he is a liar and the father of lies."
    bzz...click
    The radio station goes back to static.
 + [turn off]
    You turn off the radio.
    ->->
 - -> operate_radio