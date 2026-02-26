# ASSET_G-022 - Advanced Environment Tiles

> **Module**: Asset  
> **Requirement**: ASSET_G-022 through ASSET_G-030  
> **Status**: To Do

## 1. Overview

Advanced environment tiles for water, vegetation, and transitions.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-022 | Water Animation Frames | Animated water tiles |
| ASSET_G-023 | Vegetation Tiles | Trees, bushes, grass |
| ASSET_G-024 | Transition Tiles | Biome edges |
| ASSET_G-025 | Cliff/Rock Tiles | Elevation changes |
| ASSET_G-026 | Path Variants | Dirt, stone, cobble |
| ASSET_G-027 | Bridge Tiles | Wooden, stone bridges |
| ASSET_G-028 | Fence/Wall Variants | Different styles |
| ASSET_G-029 | Door/Gate Tiles | Open/closed states |
| ASSET_G-030 | Roof Tiles | Different roof styles |

## 3. Specifications

### 3.1 Water Tiles

| State | Frames | Animation Speed |
|-------|--------|-----------------|
| River Edge | 4 | 4 FPS |
| Lake Edge | 4 | 4 FPS |
| Waterfall | 4 | 6 FPS |
| Shallow Water | 2 | 2 FPS |

### 3.2 Vegetation

| Element | Variants | Size | Animation |
|---------|----------|------|-----------|
| Tree Small | 3 | 32x64 | None |
| Tree Medium | 4 | 48x96 | Slight sway |
| Tree Large | 3 | 64x128 | Sway |
| Bush | 4 | 32x32 | None |
| Grass Patch | 4 | 32x32 | Sway |
| Flower | 3 | 16x16 | None |
| Vine | 3 | 16x32 | None |

### 3.3 Path Tiles

| Type | Variants | Size |
|------|----------|------|
| Dirt Path | 4 | 64x32 |
| Stone Path | 4 | 64x32 |
| Cobble | 4 | 64x32 |
| Wood Planks | 3 | 64x32 |

### 3.4 Elevation Tiles

| Type | Height Levels | Variants |
|------|---------------|----------|
| Cliff Straight | 1-3 | 4 each |
| Cliff Corner | 1-3 | 4 each |
| Ramp | 1-2 | 4 |
| Stairs | 1-3 | 4 |

## 4. Acceptance Criteria

- [ ] Water animates smoothly
- [ ] Tiles connect seamlessly
- [ ] All variants blend naturally
- [ ] Proper collision data

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
- ASSET_G-021 (Environment Tiles)
