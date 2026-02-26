# ASSET_G-011 - Character Assets

> **Module**: Asset  
> **Requirement**: ASSET_G-011 through ASSET_G-020  
> **Status**: To Do

## 1. Overview

Character sprites and animations for player and NPCs.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-011 | Player Sprites | Idle, walk, run, jump |
| ASSET_G-012 | Player Animations | Combat, gather, interact |
| ASSET_G-013 | NPC Base Sprites | Generic NPC templates |
| ASSET_G-014 | NPC Animations | Idle, walk, talk |
| ASSET_G-015 | Enemy Sprites | Basic enemy characters |
| ASSET_G-016 | Enemy Animations | Attack, hit, death |
| ASSET_G-017 | Character Customization | Hair, clothes options |
| ASSET_G-018 | Portrait Set | Character portraits for UI |
| ASSET_G-019 | Death/Respawn | Death and spawn effects |
| ASSET_G-020 | Shadow Graphics | Character shadows |

## 3. Specifications

### 3.1 Player Character

| Animation | Frames | Size | Notes |
|-----------|--------|------|-------|
| Idle | 4 | 32x48 | Breathing motion |
| Walk | 8 | 32x48 | Side steps |
| Run | 8 | 32x48 | Faster animation |
| Jump | 2 | 32x48 | Up/down |
| Attack | 6 | 48x48 | Weapon swing |
| Gather | 8 | 32x48 | Mining/chopping |
| Hit | 2 | 32x48 | Knockback |
| Die | 6 | 32x48 | Fall down |

### 3.2 Character Style

- **Style**: SD/Chibi (super-deformed)
- **Head Size**: 16x16 px (half total height)
- **Proportions**: Large head, small body
- **Eyes**: Expressive, simple shapes

### 3.3 Color Variations

| Variation | Colors |
|-----------|--------|
| Skin Tone | #F5D0A9, #E8B88A, #D4A574, #8D5524 |
| Hair | #2C1810, #8B4513, #DAA520, #F5F5F5 |
| Clothes | Earth tones, era-appropriate |

### 3.4 NPC Types

| Type | Base Color | Use Case |
|------|------------|----------|
| Villager | Brown/Green | Stone Age NPCs |
| Merchant | Blue/Gold | Shop keepers |
| Elder | Gray/White | Quest givers |
| Guard | Red/Silver | Security |

## 4. Acceptance Criteria

- [ ] Smooth animations (8-12 FPS)
- [ ] Consistent proportions
- [ ] Clear silhouette
- [ ] Proper hitbox alignment

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
