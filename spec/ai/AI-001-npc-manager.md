# AI-001 - NPC Manager

> **Module**: AI  
> **Requirement**: AI-001 through AI-010  
> **Status**: To Do

## 1. Overview

The NPC Manager handles all non-player characters including their behaviors, dialogue, quests, and routines.

## 2. Requirements

| ID | Requirement | Description |
|----|-------------|-------------|
| AI-001 | NPC Spawning | Spawn NPCs in zones |
| AI-002 | NPC Behavior | AI routines and schedules |
| AI-003 | Dialogue System | Conversation with NPCs |
| AI-004 | Quest Givers | NPCs give quests |
| AI-005 | Quest Tracking | Track active quests |
| AI-006 | NPC Schedules | Daily routines |
| AI-007 | Merchant NPCs | Buy/sell items |
| AI-008 | Reputation | NPC attitudes toward player |
| AI-009 | Quest Rewards | KP and items for quests |
| AI-010 | Quest Categories | Main, side, daily quests |

## 3. API

### 3.1 Properties

```csharp
public List<NPC> ActiveNPCs { get; }
public List<Quest> ActiveQuests { get; }
public Dictionary<NPC, NPCAttitude> NPCAttitudes { get; }
```

### 3.2 Methods

```csharp
public void SpawnNPC(NPCData data, Vector3 position)
public void RemoveNPC(NPC npc)
public void StartDialogue(NPC npc)
public Quest[] GetAvailableQuests(NPC npc)
public void AcceptQuest(Quest quest)
public void CompleteQuest(Quest quest)
```

## 4. Acceptance Criteria

- [ ] NPCs spawn in correct locations
- [ ] NPCs follow daily schedules
- [ ] Dialogue system works
- [ ] Quests can be accepted/completed
- [ ] Merchant NPCs buy/sell items

## 5. Dependencies

| Module | Dependency Type |
|--------|-----------------|
| Player | Interactions |
| UI | Dialogue UI, quest UI |
| Systems | Quest rewards |
