=== basement(_position) ===
{ update_location("Basement", _position) }
~ music = "basement_theme"
-> options

= options

# clickables: clear

 + [{ investigate("body", "Body") }] -> body ->
 + [{ exit("up", "Upstairs") }] -> masterbedroom("FromBasement")
 - -> options

= body
~ found_body = true
A body lays on the floor here.

 + [examine]
    { ! You turn the body over and see the face of the landlord. }
    A deep slash is on his throat.
    The body is still warm.
    + + [check pockets]
        { not house_key: You find a key. }
        { house_key: You already found a key in their pocket. }
        ~ house_key = true
    + + [leave]
 + [leave]
 - ->->
