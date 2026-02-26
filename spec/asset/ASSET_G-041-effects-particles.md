# ASSET_G-041 - Effects & Particles

> **Module**: Asset  
> **Requirement**: ASSET_G-041 through ASSET_G-050  
> **Status**: To Do

## 1. Overview

Visual effects, particles, and animation assets.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-041 | Combat Effects | Hit, slash, impact |
| ASSET_G-042 | Gathering Effects | Mining, chopping, gathering |
| ASSET_G-043 | Magic Effects | Discovery glow, KP gain |
| ASSET_G-044 | Weather Particles | Rain, snow, leaves |
| ASSET_G-045 | UI Transitions | Panel fade, slide |
| ASSET_G-046 | Damage Numbers | Floating damage text |
| ASSET_G-047 | Achievement Effects | Unlock celebrations |
| ASSET_G-048 | Level Up Effects | XP gain animations |
| ASSET_G-049 | Ambient Particles | Dust, fireflies |
| ASSET_G-050 | Discovery Effects | New item discovery |

## 3. Specifications

### 3.1 Combat Effects

| Effect | Duration | Color | Size |
|--------|----------|-------|------|
| Hit | 0.3s | White flash | 32x32 |
| Slash | 0.4s | Yellow arc | 48x48 |
| Block | 0.2s | Blue flash | 32x32 |
| Critical | 0.5s | Red + gold | 48x48 |
| Heal | 0.6s | Green + particles | 32x32 |

### 3.2 Gathering Effects

| Effect | Duration | Color | Size |
|--------|----------|-------|------|
| Mine | 0.5s | Gray particles | 32x32 |
| Chop | 0.4s | Brown particles | 32x32 |
| Gather | 0.3s | Item glow | 32x32 |
| Success | 0.5s | Golden sparkle | 48x48 |

### 3.3 Particle Guidelines

| Property | Value |
|----------|-------|
| Max Particles | 50 per effect |
| Lifetime | 0.5-2.0 seconds |
| Size Range | 4-16 px |
| Blend Mode | Additive |
| Color Fade | Alpha fade out |

### 3.4 Weather Particles

| Weather | Particle | Color | Density |
|---------|----------|-------|----------|
| Rain | Drop | #87CEEB | 100/sec |
| Snow | Flake | #FFFFFF | 50/sec |
| Storm | Bolt | #FFFF00 | Random |
| Fog | Cloud | #AAAAAA | 20/sec |

### 3.5 UI Transition Effects

| Transition | Duration | Easing |
|------------|----------|--------|
| Panel Open | 0.3s | Ease-out |
| Panel Close | 0.2s | Ease-in |
| Button Hover | 0.1s | Linear |
| Notification | 0.5s | Ease-out-back |

## 4. Acceptance Criteria

- [ ] Effects don't impact performance
- [ ] Consistent visual language
- [ ] Clear feedback to player
- [ ] Proper layering

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
- ASSET_G-002 (UI Assets)
