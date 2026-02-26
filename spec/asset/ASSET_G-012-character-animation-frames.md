# ASSET_G-012 - Character Animation Frames

> **Module**: Asset  
> **Requirement**: ASSET_G-012 through ASSET_G-020  
> **Status**: To Do

## 1. Overview

Detailed character animation frames and variants.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-012 | Player Idle Frames | 4-frame breathing loop |
| ASSET_G-013 | Player Walk Cycles | 8-frame directional |
| ASSET_G-014 | Player Run Cycles | 8-frame directional |
| ASSET_G-015 | Player Combat Animations | Attack, defend, dodge |
| ASSET_G-016 | Player Gathering Animations | Mine, chop, gather |
| ASSET_G-017 | NPC Idle Variations | Different idle poses |
| ASSET_G-018 | NPC Walking Animation | 4-6 frame walk cycle |
| ASSET_G-019 | Enemy Attack Animations | 4-6 frame attacks |
| ASSET_G-020 | Death/Respawn Animations | 4-8 frame sequences |

## 3. Specifications

### 3.1 Player Animation Specs

| Animation | Frames | FPS | Size |
|-----------|--------|-----|------|
| Idle | 4 | 4 | 32x48 |
| Walk | 8 | 8 | 32x48 |
| Run | 8 | 12 | 32x48 |
| Jump | 2 | - | 32x48 |
| Attack 1 | 6 | 10 | 48x48 |
| Attack 2 | 6 | 10 | 48x48 |
| Defend | 4 | 8 | 32x48 |
| Dodge | 6 | 12 | 32x48 |
| Hit | 2 | - | 32x48 |
| Die | 6 | 6 | 32x48 |

### 3.2 Animation Directions

- **4-directional**: Right, Left, Up (Back), Down (Front)
- **Or**: 8-directional with mirrored animations

### 3.3 NPC Animations

| Type | Frames | Usage |
|------|--------|-------|
| Idle 1 | 4 | Generic idle |
| Idle 2 | 2 | Standing look around |
| Idle 3 | 4 | Working |
| Walk | 6 | Walking |
| Talk | 4 | Talking gesture |
| Interact | 6 | Using item |

### 3.4 Enemy Animations

| Enemy Type | Idle | Walk | Attack | Hit | Die |
|------------|------|------|--------|-----|-----|
| Basic | 4 | 6 | 4 | 2 | 6 |
| Fast | 4 | 8 | 4 | 2 | 4 |
| Heavy | 4 | 4 | 8 | 3 | 8 |
| Boss | 6 | 8 | 10 | 4 | 12 |

## 4. Acceptance Criteria

- [ ] Smooth animation loops
- [ ] Consistent frame timing
- [ ] Proper hitbox alignment
- [ ] All directions covered

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
- ASSET_G-011 (Character Assets)
