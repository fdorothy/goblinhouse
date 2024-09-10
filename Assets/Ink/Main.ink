VAR scene = ""
VAR position = ""
VAR interactive = false
VAR DEBUG = false

// global game variables
VAR master_key = false
VAR house_key = false
VAR rainboots = false
VAR coins = false
VAR master_unlocked = false
VAR front_door_unlocked = false
VAR holding = ""

INCLUDE Bedroom.ink
INCLUDE Hallway.ink
INCLUDE Kitchen.ink
INCLUDE LivingRoom.ink
INCLUDE MasterBedroom.ink
INCLUDE Basement.ink
INCLUDE Cemetary.ink
INCLUDE Shed.ink
INCLUDE Road.ink
INCLUDE GuestRoom.ink

<- intro

=== intro ===
{ ! GOBLINS IN THE WALLS }
 + [START]
    -> start
 + [HELP]
    Left-button click to move
    The cursor will change if you can interact with an object
    Click on the object to interact with it
    -> intro

=== start ===
They're all around you.
Their knives nearly as sharp as their teeth.
Their intent as evil as their skin is green.
 + [open your eyes]
    ...
    You wake from the bad dream in an unfamiliar place.
    As you stare at the ceiling, you remember you're on vacation.
    You are unsure what time it is, but the storm outside makes it hard to sleep.
    You wonder if the tour will be canceled. Better check the laptop.
    + + [get up] -> bedroom("FromStart")

=== gameover ===
YOU DIED
:scene Death Anywhere
-> DONE

=== win ===
YOU WIN
-> DONE

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

== function take(item)

~ holding = item