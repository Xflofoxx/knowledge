# ASSET_G-042 - Advanced Effects

> **Module**: Asset  
> **Requirement**: ASSET_G-042 through ASSET_G-050  
> **Status**: To Do

## 1. Overview

Advanced visual effects for weather, magic, and transitions.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| ASSET_G-042 | Weather Overlays | Rain, snow, fog layers |
| ASSET_G-043 | Magic Effects | Spells, buffs, particles |
| ASSET_G-044 | Quest Indicators | Quest markers, objectives |
| ASSET_G-045 | Discovery Animations | New item reveal |
| ASSET_G-046 | Level Up Effects | XP, level celebration |
| ASSET_G-047 | Transition Effects | Scene fades |
| ASSET_G-048 | Ambient Particles | Fireflies, dust motes |
| ASSET_G-049 | Water Effects | Ripples, splashes |
| ASSET_G-050 | UI Animations | Panel transitions |

## 3. Specifications

### 3.1 Weather Overlays

| Weather | Particle Rate | Color | Opacity |
|---------|---------------|-------|---------|
| Rain | 100/sec | #87CEEB | 60% |
| Heavy Rain | 200/sec | #5CACEE | 80% |
| Snow | 50/sec | #FFFFFF | 50% |
| Fog | - | #CCCCCC | 30% |
| Storm Lightning | 0.5/sec | #FFFF00 | 100% |

### 3.2 Magic Effects

| Effect | Duration | Color | Size |
|--------|----------|-------|------|
| Heal | 0.6s | #00FF00 | 32x32 |
| Fire | 1.0s | #FF4500 | 48x48 |
| Ice | 0.8s | #00FFFF | 48x48 |
| Lightning | 0.3s | #FFFF00 | 64x64 |
| Buff Glow | 2.0s | #FFD700 | 32x32 |
| Debuff | 2.0s | #800080 | 32x32 |

### 3.3 Quest Indicators

| Type | Visual | Animation |
|------|--------|-----------|
| Quest Available | Exclamation mark | Bounce |
| Quest Active | Checkmark | Pulse |
| Quest Objective | Dot | None |
| NPC Quest Giver | Marker | Float |
| Objective Complete | Green check | Fade |

### 3.4 Transition Effects

| Type | Duration | Easing |
|------|----------|--------|
| Scene Fade | 1.0s | Linear |
| Warp Effect | 1.5s | Ease-in-out |
| Era Transition | 2.0s | Ease-in-out |
| Discovery Popup | 0.5s | Back-out |

### 3.5 Ambient Particles

| Type | Count | Color | Motion |
|------|-------|-------|--------|
| Fireflies | 10-20 | #FFFF00 | Random drift |
| Dust Motes | 5-10 | #F5F5DC | Slow rise |
| Sparks | 3-5 | #FF4500 | Fall |
| Water Spray | 10-15 | #87CEEB | Splash |

## 4. Acceptance Criteria

- [ ] Weather doesn't block visibility
- [ ] Magic effects are readable
- [ ] Transitions are smooth
- [ ] Performance optimized

## 5. Dependencies

- ASSET_G-001 (Art Style Guide)
- ASSET_G-041 (Effects & Particles)
