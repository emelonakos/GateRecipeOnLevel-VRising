
# LevelRecipeGate

V Rising server mod originally based on RecipeTweaker, simplified to a single purpose:

**Block crafting recipes until players reach the required gear level.**

## Credits

* Original recipe blocking concept by **@Chimll**
* Source code originally based on **RecipeTweaker**
* Reworked and simplified into a progression-focused recipe gating system

---

## Features

### Recipe Level Gating

Lock any crafting recipe behind a required gear level.

Examples:

* Merciless Iron → Level 33
* Reinforced Iron → Level 50
* Dark Silver → Level 65
* Sanguine → Level 74

Players attempting to craft a locked recipe will have the craft cancelled automatically.

---

### Persistent Progression Tracking

The mod tracks the **highest gear level a player has ever reached**.

This means:

* Reach Gear Level 91 once → permanently recorded.
* Downgrade gear later → still considered Gear Level 91 for recipe unlocks.
* Previously unlocked recipe tiers remain available.

Highest player levels are stored in:

```txt
BepInEx/config/LevelRecipeGate/player_highest_levels.json
```

Example:

```json
{
  "76561199042557118": {
    "name": "Neen",
    "steam_id": 1234567890123446,
    "highest_gear_level": 91
  }
}
```

---

### Crafting Feedback

When a player attempts to craft a locked recipe:

* Craft is blocked
* Player receives a chat/system message
* Server logs the attempt

Example message:

```txt
You cannot craft this yet. This recipe unlocks at gear level 74. Your highest gear level is 65.
```

---

## Removed From RecipeTweaker

Removed from this clean version:

* Drop blocking
* Servant loot blocking
* Scheduled unlocks
* Global `blocked_recipes.txt`
* Recipe unlock schedules
* Drop restriction systems

---

## Configuration

Generated at:

```txt
BepInEx/config/LevelRecipeGate/level_recipe_blocks.json
```

Example:

```json
{
  "enabled": true,
  "message": "You cannot craft this yet. This recipe unlocks at gear level {level}. Your highest gear level is {current}.",
  "recipe_level_blocks": [
    {
      "min_level": 33,
      "recipes": [
        "Recipe_Weapon_Axe_T05_Iron",
        "Recipe_Weapon_Claws_T05_Iron"
      ]
    },
    {
      "min_level": 50,
      "recipes": [
        "Recipe_Weapon_Axe_T06_Iron_Reinforced"
      ]
    }
  ]
}
```

---

## Commands

```txt
.levelrecipegate reload
.levelrecipegate list
.levelrecipegate add <level> <recipePrefab>
.levelrecipegate remove <recipePrefab>
```

Examples:

```txt
.levelrecipegate add 74 Recipe_Weapon_Sword_T08_Sanguine
.levelrecipegate remove Recipe_Weapon_Sword_T08_Sanguine
.levelrecipegate list
.levelrecipegate reload
```

---

### Live Reload

Changes to `level_recipe_blocks.json` are watched and reloaded automatically.
The `.levelrecipegate reload` command is still available for manual reloads.

---

## How Gear Level Is Calculated

Uses the same calculation used by V Rising:

```txt
ArmorLevel + SpellLevel + WeaponLevel
```

The highest value ever reached is saved * its found BepInEx\config\LevelRecipeGate\player_highest_level *  and used for future recipe checks.

---

## Build

Copy to:

```txt
VRising_Server\BepInEx\plugins\
```

---

## Troubleshooting

Check:

```txt
BepInEx/LogOutput.log|

```

---

Dependencies

    BepInEx
    VampireCommandFramework
    ```
