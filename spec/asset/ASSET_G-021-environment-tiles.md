# ASSET_G-021 - Environment Tiles

> **Module**: Asset  
> **Requirement**: ASSET_G-021 through ASSET_G-030  
> **Status**: To Do

## 1. Overview

Isometric environment tiles for terrain, buildings, and decorations.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-021 | Terrain Tiles | Grass, dirt, stone, water |
| ASSET_G-022 | Path Tiles | Dirt paths, roads |
| ASSET_G-023 | Water Tiles | Rivers, lakes, ocean |
| ASSET_G-024 | Wall Tiles | Walls, fences |
| ASSET_G-025 | Building Tiles | Structures, houses |
| ASSET_G-026 | Decoration Tiles | Trees, rocks, plants |
| ASSET_G-027 | Resource Nodes | Gatherable items |
| ASSET_G-028 | Era Transitions | Gate visuals |
| ASSET_G-029 | Weather Overlays | Rain, snow particles |
| ASSET_G-030 | Lighting Tiles | Day/night variations |

## 3. Specifications

### 3.1 Tile Standards

| Property | Value |
|----------|-------|
| Base Size | 64x32 px (isometric) |
| Grid | Diamond orientation |
| Depth | 16 px per height level |
| Outline | 1px #1A1A1A |

### 3.2 Terrain Tiles (Stone Age - Priority)

| Tile | Variants | Notes |
|------|----------|-------|
| Grass | 4 | Different grass patterns |
| Dirt | 3 | Bare ground |
| Stone Floor | 3 | Cave floors |
| Forest Edge | 2 | Grass-to-forest |
| Water Edge | 4 | Animated water |

### 3.3 Vegetation (Stone Age)

| Element | Size | Animation |
|---------|------|-----------|
| Small Tree | 32x64 | Static |
| Large Tree | 64x96 | Slight sway |
| Bush | 32x32 | Static |
| Grass Patch | 32x32 | Slight sway |
| Flower | 16x16 | Static |

### 3.4 Resource Nodes

| Resource | Visual | Size |
|----------|--------|------|
| Wood | Brown log | 32x32 |
| Stone | Gray rock | 32x32 |
| Berry Bush | Red berries | 32x32 |
| Flint | Dark gray | 16x16 |
| Bone | White | 16x16 |

### 3.5 Era Progression Assets

| Era | Building Style | Primary Colors |
|-----|----------------|----------------|
| Stone Age | Thatch, caves | Brown, tan |
| Bronze Age | Wood, mudbrick | Brown, ochre |
| Iron Age | Stone, timber | Gray, brown |

## 4. Acceptance Criteria

- [ ] Tiles connect seamlessly
- [ ] Variants blend naturally
- [ ] Consistent lighting direction
- [ ] Proper collision mapping

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
