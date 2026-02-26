# ASSET_G-002 - UI Assets

> **Module**: Asset  
> **Requirement**: ASSET_G-002 through ASSET_G-010  
> **Status**: To Do

## 1. Overview

UI assets required for the initial beta including HUD, menus, and icons.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-002 | Main Menu Background | Title screen background |
| ASSET_G-003 | HUD Elements | Health, hunger, energy bars |
| ASSET_G-004 | Icon Set | Game icons (items, actions) |
| ASSET_G-005 | Button Graphics | Menu buttons |
| ASSET_G-006 | Panel Graphics | UI panel backgrounds |
| ASSET_G-007 | Font Assets | Game typography |
| ASSET_G-008 | Notification Graphics | Pop-up backgrounds |
| ASSET_G-009 | Progress Bars | Loading, health bars |
| ASSET_G-010 | Cursor Graphics | Custom cursors |

## 3. Specifications

### 3.1 HUD Elements

| Element | Size | Format |
|---------|------|--------|
| Health Bar | 200x20 px | PNG with transparency |
| Hunger Bar | 200x20 px | PNG with transparency |
| Energy Bar | 200x20 px | PNG with transparency |
| Happiness Icon | 32x32 px | PNG with transparency |
| KP Counter | 64x32 px | PNG with transparency |
| Era Badge | 48x48 px | PNG with transparency |

### 3.2 Icon Set (Priority Items)

| Icon | Size | Description |
|------|------|-------------|
| Health Potion | 32x32 | Red potion |
| Food | 32x32 | Apple/bread |
| Water | 32x32 | Blue flask |
| Wood | 32x32 | Brown log |
| Stone | 32x32 | Gray rock |
| KP Crystal | 32x32 | Golden crystal |

### 3.3 Button Style

- **Normal**: 200x48 px, rounded corners 8px
- **Hover**: Brightness +20%
- **Pressed**: Scale 95%, brightness -10%
- **Disabled**: Opacity 50%

### 3.4 Color Scheme

- Primary: #DAA520 (Warm Gold)
- Background: #1A1A2E (Panel Dark)
- Text: #F5F5F5 (Text Primary)
- Accent: #4A90D9 (Accent Blue)

## 4. Acceptance Criteria

- [ ] All icons readable at 32x32
- [ ] Buttons have hover/pressed states
- [ ] Consistent visual style
- [ ] Proper transparency

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
