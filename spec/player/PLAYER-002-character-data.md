# PLAYER-002 - Character Data

> **Module**: Player  
> **Requirement**: PLAYER-016 through PLAYER-020  
> **Status**: To Do

## 1. Overview

Character Data manages the persistent data of the player character including attributes, skills, and progression.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| PLAYER-016 | Character Creation | Create new character |
| PLAYER-017 | Attribute Points | Distribute points |
| PLAYER-018 | Skill System | Track skills |
| PLAYER-019 | Level Progression | XP and leveling |
| PLAYER-020 | Character Stats | Base stats (STR, DEX, INT) |

## 3. API

### 3.1 Properties

```csharp
public string Name { get; set; }
public Gender Gender { get; set; }
public int Age { get; set; }
public int Level { get; }
public int AvailablePoints { get; }
```

### 3.2 Methods

```csharp
public void CreateCharacter(string name, Gender gender, int age)
public void AddAttributePoint(Attribute type)
public void AddExperience(int amount)
public CharacterData Serialize()
public void Deserialize(CharacterData data)
```

## 4. Acceptance Criteria

- [ ] Character can be created with name, gender, age
- [ ] Attributes can be distributed
- [ ] Experience leads to level up
- [ ] Data serializes/deserializes correctly

## 5. Dependencies

| Module | Dependency Type |
|--------|-----------------|
| Player | PlayerController |
| Core | Save/Load |
