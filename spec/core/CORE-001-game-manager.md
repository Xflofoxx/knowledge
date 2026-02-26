# CORE-001 - Game Manager

> **Module**: Core  
> **Requirement**: CORE-001 through CORE-010  
> **Status**: To Do

## 1. Overview

The Game Manager is the central controller for the entire game. It manages game state, time progression, knowledge points, and global game settings.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| CORE-001 | Game State Management | Pause/Resume game |
| CORE-002 | Time Progression | Control game time scale |
| CORE-003 | Knowledge Points | Track and manage KP |
| CORE-004 | Save/Load | Persist game state |
| CORE-005 | Scene Management | Handle scene transitions |
| CORE-006 | Settings Management | Store player preferences |
| CORE-007 | Event System | Global game events |
| CORE-008 | Achievement System | Track player achievements |
| CORE-009 | Day/Night Cycle | Global lighting control |
| CORE-010 | Multiplayer Support | (Future) Multiplayer state |

## 3. API

### 3.1 Properties

```csharp
public bool IsGamePaused { get; }
public float TimeScale { get; set; }
public int TotalKnowledgePoints { get; }
public GameState CurrentState { get; }
```

### 3.2 Methods

```csharp
public void PauseGame()
public void ResumeGame()
public void AddKnowledgePoints(int amount)
public void SetTimeScale(float scale)
public void SaveGame(string path)
public void LoadGame(string path)
```

## 4. Acceptance Criteria

- [ ] Game can be paused and resumed
- [ ] Time scale can be adjusted (0.1x to 2x)
- [ ] Knowledge points accumulate correctly
- [ ] Save/Load preserves all game state
- [ ] All game events fire correctly

## 5. Dependencies

| Module | Dependency Type |
|--------|-----------------|
| UI | Display pause menu |
| Systems | KP calculations |
| Player | Time-based stats |
