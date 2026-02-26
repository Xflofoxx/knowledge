# ASSET_G-044 - Enemy & Combat Assets

> **Module**: Asset  
> **Requirement**: ASSET_G-044 through ASSET_G-048  
> **Status**: To Do

## 1. Overview

Enemy sprites, combat indicators, and damage visuals.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-044 | Enemy Sprites | Basic enemy characters |
| ASSET_G-045 | Boss Sprites | Large enemy characters |
| ASSET_G-046 | Damage Indicators | Hit numbers, crit markers |
| ASSET_G-047 | Health Bars | Enemy HP display |
| ASSET_G-048 | Combat Indicators | Attack range, targeting |

## 3. Specifications

### 3.1 Basic Enemies (Stone Age)

| Enemy | Size | Color | Behavior |
|-------|------|-------|----------|
| Wolf | 32x32 | Gray/brown | Fast, pack |
| Bear | 48x48 | Brown | Slow, strong |
| Snake | 32x16 | Green | Fast, poison |
| Boar | 32x32 | Brown | Charges |
| Bat | 24x24 | Purple | Flying |

### 3.2 Enemy Animation

| Animation | Frames | FPS |
|-----------|--------|-----|
| Idle | 4 | 4 |
| Walk | 6 | 8 |
| Attack | 4-6 | 10 |
| Hit | 2 | - |
| Die | 6 | 6 |

### 3.3 Damage Indicators

| Type | Visual | Color |
|------|--------|-------|
| Normal Damage | Number pop | White |
| Critical | Number + "CRIT!" | Gold |
| Heal | +Number | Green |
| Miss | "MISS" | Gray |
| Block | "BLOCK" | Blue |

### 3.4 Targeting

| Element | Visual |
|---------|--------|
| Enemy Select | Red outline |
| Attack Range | Red circle |
| Movement Range | Blue circle |
| Safe Zone | Green outline |

## 4. Acceptance Criteria

- [ ] Enemies visually distinct
- [ ] Damage clear to read
- [ ] Targeting is obvious

## 5. Dependencies

- ASSET_G-011 (Character Assets)
- ASSET_G-012 (Animation Frames)
