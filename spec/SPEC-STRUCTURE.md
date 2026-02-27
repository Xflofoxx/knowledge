# Project Structure Specification

## 1. Overview

This document defines the Unity project structure for Knowledge RPG. The structure follows Unity best practices and supports the modular architecture defined in `SPEC.md`.

---

## 2. Root Structure

```
Knowledge/
├── Assets/
│   ├── Scripts/
│   ├── Editor/
│   ├── Resources/
│   ├── Plugins/
│   ├── ThirdParty/
│   ├── Art/
│   ├── Audio/
│   └── Prefabs/
├── Packages/
├── ProjectSettings/
├── Tests/
└── docs/
```

---

## 3. Assets/Scripts Structure

```
Assets/Scripts/
├── Core/
│   ├── GameManager.cs
│   ├── SaveLoadSystem.cs
│   ├── SettingsManager.cs
│   ├── EventSystem.cs
│   └── AchievementSystem.cs
├── Player/
│   ├── PlayerController.cs
│   ├── CharacterData.cs
│   ├── CharacterEditor.cs
│   ├── InventorySystem.cs
│   ├── EquipmentSystem.cs
│   ├── CombatSystem.cs
│   ├── AnimationSystem.cs
│   └── SurvivalStats.cs
├── Discovery/
│   ├── DiscoverySystem.cs
│   ├── CraftingSystem.cs
│   ├── RecipeSystem.cs
│   ├── ResourceSystem.cs
│   └── KnowledgeTreeUI.cs
├── Environment/
│   ├── WeatherSystem.cs
│   ├── EcosystemManager.cs
│   ├── DayNightCycle.cs
│   ├── WorldMap.cs
│   ├── ZoneSystem.cs
│   ├── TerrainSystem.cs
│   ├── BuildingSystem.cs
│   └── SoundSystem.cs
├── Systems/
│   ├── KnowledgeSystem.cs
│   ├── ProgressionSystem.cs
│   ├── SkillSystem.cs
│   ├── ItemDatabase.cs
│   └── CraftingDatabase.cs
├── AI/
│   ├── NPCManager.cs
│   ├── DialogueSystem.cs
│   ├── QuestSystem.cs
│   ├── EnemySystem.cs
│   └── ShopSystem.cs
├── UI/
│   ├── UIManager.cs
│   ├── MainMenu.cs
│   ├── HUDSystem.cs
│   ├── PauseMenu.cs
│   ├── NotificationSystem.cs
│   └── LoadingScreens.cs
└── Utils/
    ├── Extensions/
    ├── Helpers/
    └── Constants/
```

---

## 4. Assets/Art Structure

```
Assets/Art/
├── Characters/
│   ├── Player/
│   ├── NPCs/
│   └── Enemies/
├── Environment/
│   ├── Tiles/
│   ├── Buildings/
│   └── Props/
├── Items/
│   ├── Icons/
│   ├── Equipment/
│   └── Materials/
├── Effects/
│   ├── Particles/
│   └── Shaders/
└── UI/
    ├── Icons/
    ├── Fonts/
    └── Sprites/
```

---

## 5. Assets/Prefabs Structure

```
Assets/Prefabs/
├── Characters/
│   ├── Player.prefab
│   └── NPCs/
├── Environment/
│   ├── Buildings/
│   ├── Tiles/
│   └── Props/
├── UI/
│   ├── Panels/
│   └── Components/
└── Systems/
```

---

## 6. Assets/Resources Structure

```
Assets/Resources/
├── Data/
│   ├── Items/
│   ├── Recipes/
│   ├── Dialogues/
│   └── Quests/
├── Localization/
│   └── Languages/
└── Config/
```

---

## 7. Assets/Editor Structure

```
Assets/Editor/
├── CustomEditors/
├── PropertyDrawers/
├── EditorWindows/
├── Gizmos/
└── MenuItems/
```

---

## 8. Scene Structure

```
Assets/Scenes/
├── Boot/
│   └── Boot.unity
├── MainMenu/
│   └── MainMenu.unity
├── CharacterCreation/
│   └── CharacterCreation.unity
├── WorldMap/
│   └── WorldMap.unity
├── Game/
│   ├── StoneAge/
│   ├── BronzeAge/
│   ├── IronAge/
│   ├── Medieval/
│   ├── Renaissance/
│   ├── Industrial/
│   ├── Modern/
│   └── Space/
└── System/
    ├── Settings.unity
    └── Loading.unity
```

---

## 9. Tests Structure

```
Tests/
├── Editor/
│   ├── Core/
│   ├── Player/
│   ├── Discovery/
│   ├── Environment/
│   ├── Systems/
│   ├── AI/
│   └── UI/
└── PlayMode/
    ├── Integration/
    └── EndToEnd/
```

---

## 10. Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| Scripts | PascalCase | `PlayerController.cs` |
| Folders | PascalCase | `Assets/Scripts/Player/` |
| Scenes | PascalCase | `StoneAge.unity` |
| Prefabs | PascalCase | `Player.prefab` |
| Assets | PascalCase | `player_idle.anim` |
| ScriptableObjects | PascalCase | `ItemDatabase.asset` |

---

## 11. Script Organization

### Monobehaviour Scripts

```
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    private void Awake() { }
    private void Start() { }
    private void Update() { }
    private void FixedUpdate() { }

    public void PublicMethod() { }
    private void PrivateMethod() { }
}
```

### ScriptableObject Data

```
[CreateAssetMenu(fileName = "NewItem", menuName = "Knowledge/Items")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public int maxStack = 99;
}
```

---

## 12. Module Dependencies

```
Core
├── → All Modules (game manager orchestrates everything)
├── Player
│   └── → Core, Systems
├── Discovery
│   └── → Core, Systems
├── Environment
│   └── → Core
├── Systems
│   └── → Core
├── AI
│   └── → Core, Player, Systems
└── UI
    └── → Core, Player, Systems
```

---

## 13. Data Flow

```
User Input → PlayerController → EventSystem → Relevant System → EventSystem → UI/Game State
```

---

## 14. File Templates

### New Script Template

```
using UnityEngine;

namespace Knowledge.{Module}
{
    public class {ClassName} : MonoBehaviour
    {
        // Serialized fields
        // Private fields
        // Public properties

        private void Awake() { }
        private void Start() { }
        private void OnEnable() { }
        private void OnDisable() { }
    }
}
```

---

## 15. Notes

- Keep scripts focused on single responsibility
- Use ScriptableObjects for data-driven design
- Prefer composition over inheritance
- Use events for loose coupling
- All public methods should have XML documentation
