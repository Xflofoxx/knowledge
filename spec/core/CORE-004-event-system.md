# CORE-004 - Event System

> **Module**: Core  
> **Requirement**: CORE-021 through CORE-025  
> **Status**: To Do

## 1. Overview

Global event system for communication between game systems.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| CORE-021 | Event Dispatcher | Central event bus |
| CORE-022 | Game Events | OnGamePaused, OnGameResumed, etc. |
| CORE-023 | Player Events | OnHealthChanged, OnItemCollected |
| CORE-024 | Discovery Events | OnElementDiscovered, OnRecipeUnlocked |
| CORE-025 | Custom Events | Allow systems to create custom events |

## 3. Acceptance Criteria

- [ ] Events fire correctly
- [ ] Listeners receive events
- [ ] No memory leaks from event handling
