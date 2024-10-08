VAR scene = ""
VAR position = ""
VAR interactive = false
VAR DEBUG = false

// global game variables
VAR music = ""
VAR master_key = false
VAR house_key = false
VAR rainboots = false
VAR coins = false
VAR master_unlocked = false
VAR front_door_unlocked = false
VAR holding = ""
VAR investigated_laptop = false
VAR inserted_coins = false
VAR found_body = false
VAR dialed_taxi = false
VAR dialed_police = false
VAR julia_dead = false
VAR router_hint = false
VAR have_router = false
VAR router_plugged_in = false
VAR garda = false

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
INCLUDE Garden.ink


<- intro

=== intro ===
{ ! <b><size=24><color=red>GOBLINS IN THE WALLS</color></size></b> }
~ music = "title_theme"
 + [START]
    -> start
 * [HELP]
    Made by Fredric Dorothy
    Left-button click to move
    The cursor will change if you can interact with an object
    Click on the object to interact with it
    -> intro

=== start ===
{ sfx("laugh") }
They're all around you.
Their knives as sharp as their teeth.
Their intent as evil as their skin is green.
Small monsters, hell bent on murder.
 + [run]
    You make a run for it.
    They catch up to you.
 + [fight]
    You stand your ground against them.
    There are too many, they surround you.
 -
 You feel the cold steel of their blades as they begin to rip into your flesh.
 You suddenly realize that this is a dream.
 + [open your eyes]
    ...
    You wake from the bad dream in an unfamiliar place.
    You are drenched in sweat.
    As you stare at the ceiling, you remember you're on vacation.
    You are unsure what time it is, but the storm outside makes it hard to sleep.
    You wonder if the tour will be canceled. Better check the { obj("laptop") }.
    + + [get up] -> bedroom("FromStart")

=== gameover ===
YOU DIED
:scene Death Anywhere
-> DONE

=== win ===
THE END
:scene Death Anywhere
-> DONE

== function obj(text)
<b>{text}</b>

== function update_location(_scene, _position)

{ position != _position || _scene != scene :
  ~ scene = _scene
  ~ position = _position
  { interactive: :scene {scene} {position} }
  { debug("You are in the { scene }.") }
}

== function sfx(name)
{ interactive: :sfx {name} }

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