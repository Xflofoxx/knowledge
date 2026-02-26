# ASSET_G-001 - Art Style Guide

> **Module**: Asset  
> **Status**: To Do

## 1. Overview

This document defines the visual art style for Knowledge game, ensuring consistency across all assets.

## 2. Overall Art Direction

| Aspect | Style |
|--------|-------|
| Perspective | Isometric 2.5D |
| Mood | Warm, inviting, adventurous |
| Detail Level | Stylized, not realistic |
| Animation | Smooth, game-friendly framerate |

---

## 3. Color Palette

### 3.1 Primary Colors

| Color Name | Hex | Usage |
|------------|-----|-------|
| Forest Green | #2D5A27 | Nature, vegetation |
| Earth Brown | #8B5A2B | Ground, wood, UI accents |
| Sky Blue | #87CEEB | Sky, water |
| Warm Gold | #DAA520 | Highlights, achievements, KP |

### 3.2 UI Colors

| Color Name | Hex | Usage |
|------------|-----|-------|
| Panel Dark | #1A1A2E | UI backgrounds |
| Panel Light | #2D2D44 | UI panels |
| Text Primary | #F5F5F5 | Main text |
| Text Secondary | #A0A0A0 | Secondary text |
| Accent Blue | #4A90D9 | Buttons, highlights |
| Health Red | #E74C3C | Health bars |
| Energy Yellow | #F1C40F | Energy bars |
| Hunger Orange | #E67E22 | Hunger bars |
| Thirst Cyan | #3498DB | Thirst bars |

### 3.3 Era-Specific Colors

| Era | Primary | Secondary | Accent |
|-----|---------|-----------|--------|
| Stone Age | #8B4513 | #A0522D | #D2691E |
| Bronze Age | #CD7F32 | #B8860B | #FFD700 |
| Iron Age | #708090 | #2F4F4F | #C0C0C0 |
| Medieval | #4A0E0E | #800020 | #FFD700 |
| Renaissance | #4B0082 | #8B008B | #E6E6FA |
| Industrial | #2C2C2C | #4A4A4A | #FF6B00 |
| Modern | #1E90FF | #4169E1 | #00CED1 |
| Space | #0A0A23 | #191970 | #7B68EE |

---

## 4. Visual Style

### 4.1 Isometric Tile Style

- **Tile Size**: 64x64 pixels base
- **Grid**: Diamond/rhombus orientation
- **Height**: Variable (16-32px per level)
- **Outline**: Subtle 1px dark outline for definition

### 4.2 Character Style

- **Proportions**: Chibi/SD (super-deformed) - large heads
- **Height**: 2-3 tiles tall
- **Features**: Expressive, simple shapes
- **Palette**: Warm, saturated colors

### 4.3 UI Style

- **Theme**: Fantasy parchment meets modern flat
- **Borders**: Ornate but readable
- **Icons**: Clear, readable at small sizes
- **Fonts**: Readable serif or clean sans-serif

---

## 5. Asset Requirements Summary

| Category | Style | Format |
|----------|-------|--------|
| Tiles | Isometric | PNG with transparency |
| Characters | SD/Chibi | Sprite sheets |
| UI Elements | Vector-like | PNG or Vector |
| Icons | Flat, colored | PNG |
| Backgrounds | Layered parallax | PNG layers |
| Effects | Particle-friendly | Sprite sheets |

---

## 6. File Naming Convention

```
{type}_{name}_{variant}_{size}.png

Examples:
tile_grass_01_64x64.png
char_player_idle_0_32x48.png
icon_health_16x16.png
ui_panel_main_256x128.png
```
