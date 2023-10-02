# Project Ganymede - A Senior Thesis Project by Kevin Gillmore

## Introduction

"Project Ganymede'' is a first-person survival game set in an abandoned space station orbiting a distant, eerie planet. Players wake up as a lone astronaut, isolated and struggling to survive in the haunting environment. The game incorporates horror elements in the form of suspenseful atmospheres, occasional jump scares, and mysterious occurrences. This Experience is story-driven, meaning players will discover and affect how the story unfolds. Gameplay consists of Survival, Exploration, Defense against Mysterious Enemies with cameras and avoidance, Narrative Choices, and Movement Levels with Environmental Hazards. The story unfolds through player discovery and cutscenes. This game is 3D and made in Unity using Blender for Asset Creation. The Game takes on a Low Poly style with High Fidelity lighting using Unity's URP shader.

Users are anyone who enjoys less competitive, single-player experiential games. Controls and UI are simple and intuitive, with focus being put on what the player discovers and experiences in the game rather than their skill in the game by the end.

## "Use Cases" (Objects, Systems, and Features)

| **Player** <br> Objects: Player Model w/ animations, Player Cam, Player UI "HUD," Audio Source |
|------------------------------------------------------------------------------------|
| **First Person Controller** <br> Overview: The First Person Controller is a script and Input System Component that controls all Player Input and then translates that to the Player character in the game. <br> Systems and Features: <br> &bull; The Actor's input using their keyboard and Mouse/Controller must translate to Game Movement or interaction<br> &bull; The movement will be made up of walking, sprinting, and Jumping, as well as a Zero-G propulsion/jumping movement <br> &bull; Other buttons used are an interaction button, a Menu button, an Inventory Button, and an Attack Button.|
| **Interaction System** <br> Overview: The Interaction System is made up of a script that allows players to interact with in-game objects. The system can pick up, equip, and examine items with UI elements. <br> Systems and Features: <br> &bull; Picking up Items that get added to a player's inventory viewable in a UI <br> &bull; Equipping Items viewable in hand <br> &bull; Examining and Reading certain items in a UI |
| **Player Manager and HUD** <br> Overview: Manages Resources and displays through UI <br> Systems and Features: <br> &bull; Inventory, Health, and Oxygen <br> &bull; Functions to add or subtract values and items <br> &bull; UI displayed on player Cam |
| **Player Combat System** <br> Overview: Manages the use of combat items <br> Systems and Features: <br> &bull; Calculate Force and Trajectory for throwable objects <br> &bull; Work with Player Manager to manage inventory and use of an item <br> &bull; Calculate hit detection and damage |

| **Enemy** <br> Objects: Enemy Model w/ Animations, Audio Source |
|------------------------------------------------------------------------------------|
| **Movement System** <br> Overview: Script to move the Enemy(s) around the level, controlled by the AI Pathfinding <br> Systems and Features: <br> &bull; Move Enemy around level <br> &bull; Will need animations down the road |
| **AI Pathfinding** <br> Overview: A script to control the Enemy's pathfinding using Unity's Navmesh <br> Systems and Features: <br> &bull; Have different states like Chasing, wandering, catching, loaded, unloaded <br> &bull; Collision interaction to change movements states (for example player comes within a radius of the enemy and the AI, so the state is changed to chasing) |
| **Enemy Manager** <br> Overview: A script to manage Enemy stats <br> Systems and Features: <br> &bull; Should there be multiple and not one invincible enemy, it will track health <br> &bull; Recognize attacks or distractions |

| **Dialogue System (If I have Time)** <br> Objects: UI |
|------------------------------------------------------------------------------------|
| Overview: Dialogue system using Unity UI <br> Systems and Features: <br> &bull; Dialogue tree <br> &bull; Dialogue options displayed through UI |

| **Custom Event Manager** |
|------------------------------------------------------------------------------------|
| Overview: Script to track player progress and flags <br> System's and Features: <br> &bull; Keeps track of checkpoints and scripted events <br> &bull; Certain events will need other flags to happen so this system will manage those |

| **Save/Load Functionality** | 
|-------------------------------------------------------------------------------------|
| Overview: Functionality accessible in Game and in Menu <br> Systems and Features: <br> &bull; Attached to physical objects in Game and through the Main Menu <br> &bull; Allows the player to save their game and load into a save file |

| **Optimization Manager** |
|-------------------------------------------------------------------------------------|
| Overview: Custom Object Loader/Unloader for optimizing performance <br> Systems and Features: <br> &bull; Loads portions of levels depending on player location <br> &bull; Will unload unnecessary Objects |

| **Sound Manager** |
|--------------------------------------------------------------------------------------|
| Overview: Sound Manager for environmental Sounds <br> Systems and Features: <br> &bull; Will manage background sounds and music <br> &bull; Manges timed Sounds (sounds that play based on timed or random intervals) |

| **Trigger Object** | 
|--------------------------------------------------------------------------------------|
| Overview: An environmental trigger for scripted events <br> Systems and Features: <br> &bull; An object with a script to trigger any sound, animation, or event <br> &bull; 3D collider for recognizing Player collision &bull; Serialized fields for trigger, constraints, and action |

**Assets and Objects**

- Interactables: Computer Terminals, Latches, Buttons
- Exterior: Sky Box, Space Station Exterior
- Interior: Corridors, Doors, Rooms, windows, Ports, Signs, Decoration, Corpses
- Items: Health Packs and Oxygen Canisters
- Combat items: Flares, throwable objects, stun gun
- Hiding places: Crates, Beds, Lockers, tight corridors

**Story Ideas and Features to match**

- The main Character is part of the maintenance crew that wakes up every few years on a century-long expedition
- Wakes up to broken systems and an anomaly
- Use tool belt as inventory
