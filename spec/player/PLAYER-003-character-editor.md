# PLAYER-003 - Character Editor

> **Module**: Player  
> **Requirement**: PLAYER-021 through PLAYER-025  
> **Status**: To Do

## 1. Overview

Character Editor provides the UI and logic for customizing the player's appearance and background during character creation.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| PLAYER-021 | Gender Selection | Male, Female, Non-Binary |
| PLAYER-022 | Age Selection | 18-80 years |
| PLAYER-023 | Appearance Customization | Height, body type |
| PLAYER-024 | Background Selection | Starting background |
| PLAYER-025 | Preview | Live preview of character |

## 3. API

### 3.1 Properties

```csharp
public Gender SelectedGender { get; }
public int SelectedAge { get; }
public float Height { get; }
public float BodyWidth { get; }
public Background SelectedBackground { get; }
```

### 3.2 Methods

```csharp
public void SetGender(Gender gender)
public void SetAge(int age)
public void SetHeight(float height)
public void SetBodyWidth(float width)
public void SetBackground(Background background)
public CharacterData Finalize()
```

## 4. Acceptance Criteria

- [ ] Gender selection works
- [ ] Age slider works (18-80)
- [ ] Appearance affects character model
- [ ] Background gives starting bonuses
- [ ] Preview updates in real-time

## 5. Dependencies

| Module | Dependency Type |
|--------|-----------------|
| Player | CharacterData |
| UI | Editor UI |
