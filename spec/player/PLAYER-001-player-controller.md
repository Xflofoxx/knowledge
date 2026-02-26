# PLAYER-001 - Player Controller

> **Module**: Player  
> **Requirement**: PLAYER-001 through PLAYER-015  
> **Status**: To Do

## 1. Overview

The Player Controller manages the player character's movement, stats, inventory, and interactions with the game world.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| PLAYER-001 | Movement | WASD/Arrow key movement |
| PLAYER-002 | Sprint | Shift to sprint |
| PLAYER-003 | Jump | Space to jump |
| PLAYER-004 | Health System | Take damage, heal |
| PLAYER-005 | Hunger/Thirst | Survival stats |
| PLAYER-006 | Energy | Stamina for actions |
| PLAYER-007 | Happiness | Mood stat |
| PLAYER-008 | Inventory | Store items |
| PLAYER-009 | Equipment | Equip items |
| PLAYER-010 | Combat | Attack/defend |
| PLAYER-011 | Interaction | Interact with objects |
| PLAYER-012 | Animation | Movement animations |
| PLAYER-013 | Character Customization | Appearance |
| PLAYER-014 | Stats/Attributes | RPG stats |
| PLAYER-015 | Social Status | Reputation system |

## 3. API

### 3.1 Properties

```csharp
public float Health { get; private set; }
public float MaxHealth { get; }
public float MoveSpeed { get; }
public float SprintSpeed { get; }
public Inventory Inventory { get; }
```

### 3.2 Methods

```csharp
public void Move(Vector3 direction)
public void Sprint(bool enable)
public void Jump()
public void TakeDamage(float amount)
public void Heal(float amount)
public void ConsumeItem(Item item)
```

## 4. Acceptance Criteria

- [ ] Player moves smoothly with WASD
- [ ] Sprint increases movement speed
- [ ] Health decreases when taking damage
- [ ] Hunger/thirst drain over time
- [ ] Inventory displays and stores items
- [ ] Equipment affects stats

## 5. Dependencies

| Module | Dependency Type |
|--------|-----------------|
| Core | Game state, time |
| Systems | Inventory, stats |
| UI | HUD display |
| Environment | Terrain collision |
