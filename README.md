# Knowledge - Game Project

A 3D RPG/simulation game about discovery, evolution from Stone Age to Space Age.

## Language
**C#** with Unity 3D

## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/
│   │   └── GameManager.cs         # Main game controller
│   ├── Player/
│   │   └── PlayerController.cs    # Player movement & stats
│   ├── Discovery/
│   │   └── DiscoverySystem.cs     # Crafting/combination system
│   ├── Systems/
│   │   └── KnowledgeSystem.cs     # Knowledge Points (KP) system
│   ├── Environment/
│   │   ├── EcosystemManager.cs    # Biodiversity & animals
│   │   └── WeatherSystem.cs       # Climate & disasters
│   ├── AI/
│   │   └── NPCManager.cs          # NPC interactions
│   └── UI/
│       └── UIManager.cs           # HUD & menus
├── Prefabs/                        # Game objects
├── Scenes/                         # Unity scenes
├── Models/                         # 3D models
├── Textures/                      # Textures
└── Materials/                     # Materials
```

## How to Use

1. Open Unity Hub
2. Create new Unity project (3D)
3. Copy `Assets/Scripts` folder to your project
4. Create empty GameObject and attach `GameManager`
5. Configure player and add `PlayerController`

## Features Implemented

- Player movement with stats (health, energy, hunger, thirst)
- Discovery system with combination recipes
- Knowledge Points (KP) progression
- Ecosystem with 30+ animal species across 8 eras
- Weather system with dynamic climate
- Natural disasters (lightning, landslide, flood, fire)
- NPC reputation system
- Full UI with HUD, inventory, crafting, pause menu
