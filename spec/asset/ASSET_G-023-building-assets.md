# ASSET_G-023 - Building Assets

> **Module**: Asset  
> **Requirement**: ASSET_G-023 through ASSET_G-027  
> **Status**: To Do

## 1. Overview

Building and structure assets for all eras.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-023 | Stone Age Buildings | Thatch hut, cave |
| ASSET_G-024 | Bronze Age Buildings | Wood frame, mudbrick |
| ASSET_G-025 | Interactive Objects | Chests, doors, signs |
| ASSET_G-026 | Furniture Tiles | Beds, tables, chairs |
| ASSET_G-027 | Era Progression Buildings | Progressive unlockables |

## 3. Specifications

### 3.1 Stone Age Buildings

| Building | Size | Tiles | Features |
|----------|------|-------|----------|
| Thatch Hut | 4x4 | 16 | Enterable |
| Cave Entrance | 2x2 | 4 | Enterable |
| Fire Pit | 1x1 | 1 | Animated |

### 3.2 Bronze Age Buildings

| Building | Size | Tiles | Features |
|----------|------|-------|----------|
| Wood House | 4x4 | 16 | Enterable, storage |
| Market Stall | 2x2 | 4 | Merchant spot |
| Well | 1x1 | 1 | Water source |

### 3.3 Interactive Objects

| Object | States | Animation |
|--------|--------|-----------|
| Chest | Closed, Open | Lid opens |
| Door | Closed, Open, Locked | Swing open |
| Sign | - | Static |
| Lantern | On, Off | Light toggle |

### 3.4 Furniture

| Item | Variants | Size |
|------|----------|------|
| Bed | Straw, Wooden | 2x1 |
| Table | Low, High | 1x1 |
| Chair | Simple, Ornate | 1x1 |
| Storage | Chest, Box | 1x1 |

## 4. Acceptance Criteria

- [ ] Buildings are enterable
- [ ] Doors animate correctly
- [ ] Consistent scale

## 5. Dependencies

- ASSET_G-021 (Environment Tiles)
- ASSET_G-022 (Advanced Environment)
