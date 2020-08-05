VAR scene = ""
VAR position = ""
VAR interactive = false
VAR DEBUG = false

INCLUDE Bedroom.ink
INCLUDE Hallway.ink
INCLUDE Kitchen.ink

<- intro

=== intro ===
You wake from a bad dream in an unfamiliar place.
As you stare at the ceiling, you remember you're on vacation.
You are unsure what time it is, but the storm outside makes it hard to sleep.
You wonder if the tour will be canceled. Better check the laptop.
-> bedroom("FromStart")

== function update_location(_scene, _position)

{ position != _position || _scene != scene :
  ~ scene = _scene
  ~ position = _position
  { interactive: :scene {scene} {position} }
  { debug("You are in the { scene }.") }
}

== function debug(text)

{DEBUG: > text}

== function opt(type, object, text)

{ interactive:
  ~ return ":{type} {object} {text}"
  - else: ~ return "{opt_name(type)} {text} ({object})"
}

== function opt_name(type)

{ type:
  - "i": ~ return "Investigate"
  - "e": ~ return "Exit"
}

== function investigate(object, text)

~ return opt("i", object, text)

== function exit(object, text)

~ return opt("e", object, text)