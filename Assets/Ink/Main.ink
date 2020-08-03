VAR location = -> bedroom
VAR scene = ""
VAR position = ""

INCLUDE Bedroom.ink
INCLUDE Hallway.ink
INCLUDE Kitchen.ink

<- intro
-> DONE

=== intro ===
You wake from a bad dream in an unfamiliar place.
As you stare at the ceiling, you remember you're on vacation.
You are unsure what time it is, but the storm outside makes it hard to sleep.
You wonder if the tour will be canceled. Better check the laptop.
-> bedroom("FromStart")

== function update_location(_scene, _position, -> _location)

{ location != _location || position != _position || _scene != scene :
  ~ location = _location
  ~ scene = _scene
  ~ position = _position
  { cmdline : You are in the { scene }. }
}


