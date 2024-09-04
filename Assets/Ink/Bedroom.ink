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
    + + [touch it]
        The fur is surprisingly bristly.
        Not at all like the teddy bears of your youth.
        You see something in its mouth.
        + + + [take it]
            It's a note.
            + + + + [read]
                "Hope not to see Heaven"
                "I have come to lead you to the other shore"
                "into eternal darkness"
                "into fire and into ice."
            + + + + [leave it]
        + + + [leave it]
    + + [leave it]
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
 + [open window]
   You slide the window open.
   The humid air seeps into the room.
    + + [crawl out the window]
        You crawl out the window.
        You are now perched on a thin slant of roof.
        The rain poors down around you.
        + + + [jump down]
            You jump off the roof.
            You fall, breaking your leg when you hit the ground.
            + + + + [cry for help]
                Something hears your cry.
                A strange being approaches you in the rain.
                They have a knife in their hand.
                + + + + + [fight]
                    You raise your hands in self defense.
                    It is no good, the knife jabs deep into your flesh.
                    You fall over face down, gasping for breath.
                    -> gameover
                + + + + + [run]
                    You hobble on your good leg away from the thing.
                    You make it a few yards before feeling the knife enter your back.
                    -> gameover
        + + + [go back inside]
            You crawl back inside the window, leaving the precipice behind.
            
    + + [close window]
 + [nevermind]
 - ->->

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
