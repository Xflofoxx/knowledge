# Test Specification - Knowledge

> **Version**: 1.0.0  
> **Status**: Implemented

This document describes the test strategy and implementation for the Knowledge Unity project.

---

## 1. Test Framework

| Component | Technology |
|-----------|------------|
| Test Framework | NUnit (Unity Test Framework) |
| Unity Version | 6000.3.7f1 |
| Test Location | `Assets/Tests/` |

---

## 2. Test Structure

### 2.1 Directory Structure

```
Assets/
├── Scripts/
│   ├── Core/
│   │   └── GameManager.cs
│   ├── Player/
│   │   └── PlayerController.cs
│   └── ...
└── Tests/
    ├── Core/
    │   └── GameManagerTests.cs
    ├── Player/
    │   └── PlayerControllerTests.cs
    └── ...
```

### 2.2 Test Class Template

```csharp
using NUnit.Framework;

[TestFixture]
public class ClassNameTests
{
    [SetUp]
    public void Setup()
    {
        // Initialize test dependencies
    }

    [Test]
    public void MethodName_Scenario_ExpectedResult()
    {
        // Arrange
        // Act
        // Assert
    }
}
```

---

## 3. Test Classes

### 3.1 GameManagerTests

| Test | Description |
|------|-------------|
| AddKnowledgePoints_ValidAmount_IncreasesKP | Test KP addition |
| PauseGame_SetsIsPaused | Test pause functionality |
| SetTimeScale_ChangesTimeScale | Test time manipulation |

### 3.2 PlayerControllerTests

| Test | Description |
|------|-------------|
| TakeDamage_ValidAmount_DecreasesHealth | Test damage system |
| Heal_ValidAmount_IncreasesHealth | Test healing system |
| Move_ValidInput_UpdatesPosition | Test movement |

### 3.3 KnowledgeSystemTests

| Test | Description |
|------|-------------|
| Discover_NewDiscovery_AddsToKnownList | Test discovery |
| GetTotalKnowledge_ReturnsSum | Test KP calculation |

### 3.4 WeatherSystemTests

| Test | Description |
|------|-------------|
| SetWeather_ValidWeather_ChangesCurrent | Test weather change |
| GetCurrentWeather_ReturnsCorrect | Test weather retrieval |

### 3.5 UIManagerTests

| Test | Description |
|------|-------------|
| ShowPanel_ValidPanel_ShowsPanel | Test UI show |
| HidePanel_ValidPanel_HidesPanel | Test UI hide |

### 3.6 DiscoverySystemTests

| Test | Description |
|------|-------------|
| Discover_NewElement_AddsToList | Test element discovery |
| IsDiscovered_ExistingElement_ReturnsTrue | Test discovery check |

### 3.7 EcosystemManagerTests

| Test | Description |
|------|-------------|
| Initialize_ValidConfig_CreatesEcosystem | Test ecosystem init |
| UpdateEcosystem_ValidInput_UpdatesState | Test ecosystem update |

### 3.8 CharacterEditorTests

| Test | Description |
|------|-------------|
| SetGender_ValidGender_UpdatesCharacter | Test gender selection |
| SetAge_ValidAge_UpdatesCharacter | Test age selection |

### 3.9 CharacterDataTests

| Test | Description |
|------|-------------|
| CreateCharacter_ValidData_ReturnsCharacter | Test character creation |
| Serialize_ValidCharacter_ReturnsData | Test serialization |

### 3.10 NPCManagerTests

| Test | Description |
|------|-------------|
| SpawnNPC_ValidData_CreatesNPC | Test NPC spawning |
| RemoveNPC_Existing_RemovesNPC | Test NPC removal |

---

## 4. Making Code Testable

### 4.1 Add Public Properties with Setters

```csharp
// Original (not testable)
private int natureKnowledge;

// Testable version
public int NatureKnowledgeField { get => natureKnowledge; set => natureKnowledge = value; }
```

### 4.2 Add Properties for Private Fields

```csharp
// Original
private bool gamePaused;

// Testable version
public bool IsGamePaused => gamePaused;
```

### 4.3 Rename Duplicate Constants

```csharp
// Original (duplicate name conflict)
private const float SprintSpeed = 8f;
public float SprintSpeed { get; private set; }

// Fixed
private const float DefaultSprintSpeed = 8f;
public float SprintSpeed { get; private set; }
```

---

## 5. Running Tests

### 5.1 Unity Editor

1. Open Unity Editor
2. Go to **Window > General > Test Runner**
3. Click **Run All** or select specific tests

### 5.2 Command Line

```bash
Unity.exe -projectPath "C:/Sviluppo/Varie/knowledge" -runTests -testPlatform editmode -testResults "spec/test-results.xml" -logFile "spec/test.log"
```

---

## 6. Test Results

| File | Description |
|------|-------------|
| `spec/test-results.xml` | NUnit XML results |
| `spec/test-final.log` | Combined test output |
| `spec/unity-compile.log` | Compilation log |

---

## 7. Success Criteria

| Metric | Target |
|--------|--------|
| Test Coverage | > 80% for core systems |
| Compilation | 0 errors |
| Edit Mode Tests | 100% pass |
| Play Mode Tests | 100% pass |
