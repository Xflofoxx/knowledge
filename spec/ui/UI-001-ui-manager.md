# UI-001 - UI Manager

> **Module**: UI  
> **Requirement**: UI-001 through UI-015  
> **Status**: To Do

## 1. Overview

The UI Manager handles all user interface elements including HUD, menus, panels, and interactive elements.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| UI-001 | HUD | Health, hunger, energy bars |
| UI-002 | Inventory Panel | Item display and management |
| UI-003 | Crafting Panel | Combination interface |
| UI-004 | Knowledge Tree UI | Visual discovery tree |
| UI-005 | Pause Menu | Pause options |
| UI-006 | Settings Menu | Graphics, audio, controls |
| UI-007 | Character Panel | Stats and skills |
| UI-008 | Map UI | World map display |
| UI-009 | Dialogue UI | NPC interactions |
| UI-010 | Quest UI | Quest display |
| UI-011 | Notification System | Pop-up messages |
| UI-012 | Tooltips | Item/info tooltips |
| UI-013 | Loading Screens | Scene transitions |
| UI-014 | Main Menu | Start, load, settings |
| UI-015 | Era Display | Current era indicator |

## 3. API

### 3.1 Properties

```csharp
public GameObject InventoryPanel { get; }
public GameObject CraftingPanel { get; }
public GameObject KnowledgeTreePanel { get; }
public bool IsAnyPanelOpen { get; }
```

### 3.2 Methods

```csharp
public void ShowPanel(PanelType type)
public void HidePanel(PanelType type)
public void ShowNotification(string message)
public void UpdateHUD()
public void ShowTooltip(string text, Vector2 position)
```

## 4. Acceptance Criteria

- [ ] All panels open/close correctly
- [ ] HUD updates in real-time
- [ ] Notifications display properly
- [ ] Tooltips show on hover
- [ ] Panel navigation works

## 5. Dependencies

| Module | Dependency Type |
|--------|-----------------|
| Player | Stats for HUD |
| Discovery | Crafting panel |
| Systems | Knowledge display |
| Core | Pause menu |
