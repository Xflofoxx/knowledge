# ASSET_G-031 - Item & Icon Assets

> **Module**: Asset  
> **Requirement**: ASSET_G-031 through ASSET_G-040  
> **Status**: To Do

## 1. Overview

Item icons, equipment graphics, and inventory assets.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-031 | Item Icons | Inventory item icons |
| ASSET_G-032 | Equipment Icons | Armor, weapons |
| ASSET_G-033 | Tool Icons | Gathering tools |
| ASSET_G-034 | Material Icons | Raw resources |
| ASSET_G-035 | Food Icons | Consumable items |
| ASSET_G-036 | Crafted Icons | Crafted items |
| ASSET_G-037 | Discovery Icons | New discoveries |
| ASSET_G-038 | Quest Icons | Quest markers |
| ASSET_G-039 | Map Icons | Minimap markers |
| ASSET_G-040 | Rarity Borders | Item quality borders |

## 3. Specifications

### 3.1 Icon Standards

| Size | Usage |
|------|-------|
| 16x16 | Tooltips, small UI |
| 32x32 | Inventory grid |
| 48x48 | Shop, large icons |
| 64x64 | Featured items |

### 3.2 Item Categories

#### Materials (Stone Age)
| Item | Icon Style |
|------|------------|
| Wood | Brown log |
| Stone | Gray rock |
| Flint | Dark shard |
| Bone | White bone |
| Hide | Tan leather |
| Berry | Red berries |

#### Tools
| Item | Icon Style |
|------|------------|
| Pickaxe | Stone head |
| Axe | Flint blade |
| Spear | Bone tip |
| Hammer | Stone head |

#### Food
| Item | Icon Style |
|------|------------|
| Berry | Red cluster |
| Meat | Brown chunk |
| Water | Blue flask |

### 3.3 Rarity Colors

| Rarity | Border Color | Glow |
|--------|---------------|------|
| Common | #808080 | None |
| Uncommon | #1EFF00 | Subtle |
| Rare | #0070DD | Medium |
| Epic | #A335EE | Strong |
| Legendary | #FF8000 | Pulsing |

### 3.4 Icon Style Guidelines

- **Shape**: Rounded square, 4px radius
- **Background**: Semi-transparent dark
- **Border**: 2px, rarity color
- **Highlight**: Subtle inner glow

## 4. Acceptance Criteria

- [ ] Icons readable at 16x16
- [ ] Consistent style across all icons
- [ ] Clear item identification
- [ ] Proper rarity indication

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
- ASSET_G-002 (UI Assets)
