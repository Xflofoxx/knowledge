# CI Configuration - Knowledge

> **Version**: 1.0.0  
> **Status**: Implemented

This document describes the CI pipeline for running Unity tests.

---

## 1. Unity Installation

| Component | Path |
|-----------|------|
| Unity Editor | `C:/Program Files/Unity/Hub/Editor/6000.3.7f1/Editor/Unity.exe` |
| Version | Unity 6000.3.7f1 |

---

## 2. Test Execution Command

### 2.1 Edit Mode Tests

```bash
Unity.exe -projectPath "<path>" -runTests -testPlatform editmode -testResults "<path>/results.xml" -logFile "<path>/editmode.log"
```

### 2.2 Play Mode Tests

```bash
Unity.exe -projectPath "<path>" -runTests -testPlatform playmode -testResults "<path>/results.xml" -logFile "<path>/playmode.log"
```

### 2.3 Full Command Example

```bash
"C:/Program Files/Unity/Hub/Editor/6000.3.7f1/Editor/Unity.exe" \
  -projectPath "C:/Sviluppo/Varie/knowledge" \
  -runTests \
  -testPlatform editmode \
  -testResults "C:/Sviluppo/Varie/knowledge/spec/test-results.xml" \
  -logFile "C:/Sviluppo/Varie/knowledge/spec/unity-test.log"
```

---

## 3. Test Categories

### 3.1 Edit Mode Tests

Located in `Assets/Tests/`:

| Test Class | Purpose |
|------------|---------|
| GameManagerTests | Game state management |
| PlayerControllerTests | Player movement and stats |
| KnowledgeSystemTests | Knowledge/discovery system |
| WeatherSystemTests | Weather effects |
| UIManagerTests | UI panels and elements |
| DiscoverySystemTests | Discovery mechanics |
| EcosystemManagerTests | Ecosystem simulation |
| CharacterEditorTests | Character creation |
| CharacterDataTests | Character data |
| NPCManagerTests | NPC management |

---

## 4. Common Issues

### 4.1 Compilation Errors

Many test failures are caused by compilation errors in source code:

- Private fields accessed by tests → Add public properties with setters
- Readonly fields being assigned → Make settable
- Missing `using` statements → Add required namespaces
- Duplicate property names → Rename constants

### 4.2 Cached Assemblies

Unity caches compiled assemblies in `Library/ScriptAssemblies`. If tests fail despite code fixes:

1. Delete `Library/ScriptAssemblies/` folder
2. Run clean build
3. Re-run tests

---

## 5. CI Output

Test results are saved to:

- `spec/test-results.xml` - NUnit XML format
- `spec/unity-test.log` - Unity editor log
- `spec/test-final.log` - Combined test output

---

## 6. Success Criteria

| Metric | Target |
|--------|--------|
| Compilation | 0 errors |
| Edit Mode Tests | All pass |
| Play Mode Tests | All pass |
