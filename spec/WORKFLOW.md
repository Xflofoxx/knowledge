# Workflow - Knowledge

> **Version**: 1.0.0  
> **Status**: Implemented

This document describes the development workflow for the Knowledge Unity project.

---

## 1. Development Cycle

### 1.1 Feature Development

1. **Create branch**: `feature/feature-name`
2. **Write tests**: Define expected behavior
3. **Implement**: Write code to pass tests
4. **Run tests**: pass
5. Verify all tests **Commit**: Use conventional commits
6. **Push**: Push to remote
7. **Pull Request**: Merge to main

### 1.2 Bug Fix

1. **Create branch**: `fix/bug-description`
2. **Reproduce**: Write test that fails
3. **Fix**: Implement solution
4. **Verify**: Test passes
5. **Commit**: Use conventional commits

---

## 2. Branch Naming

| Type | Example |
|------|---------|
| Feature | `feature/knowledge-tree` |
| Fix | `fix/health-depletion` |
| Refactor | `refactor/player-controller` |
| Test | `test/add-discovery-tests` |

---

## 3. Commit Messages

```
feat: add player sprint functionality
fix: resolve health depletion bug
docs: update API documentation
test: add unit tests for discovery system
refactor: simplify knowledge tree logic
chore: update Unity version
```

---

## 4. Code Review Checklist

Before merging:

- [ ] Code follows CODING_STYLE.md
- [ ] All tests pass
- [ ] No compilation errors
- [ ] No debug logs in production
- [ ] Proper error handling
- [ ] Code is tested

---

## 5. Testing Workflow

### 5.1 Local Testing

```bash
# Run Unity tests via command line
Unity.exe -projectPath "C:/Svituppo/Varie/knowledge" -runTests -testPlatform editmode
```

### 5.2 Test Output

Save test results to `spec/` folder:
- `spec/test-results.xml` - NUnit format
- `spec/test-final.log` - Combined output

---

## 6. Project Structure

```
knowledge/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/           # GameManager, etc.
│   │   ├── Player/         # PlayerController, etc.
│   │   ├── Systems/        # KnowledgeSystem, etc.
│   │   ├── UI/             # UIManager, etc.
│   │   ├── Environment/    # WeatherSystem, etc.
│   │   └── Discovery/      # DiscoverySystem, etc.
│   ├── Tests/              # NUnit tests
│   ├── Scenes/             # Unity scenes
│   └── Prefabs/            # Prefabs
├── ProjectSettings/        # Unity settings
└── spec/                   # Specifications and test results
```

---

## 7. Key Commands

### 7.1 Unity Editor

| Action | Command |
|--------|---------|
| Run Edit Mode Tests | Window > General > Test Runner > Run All |
| Build Project | File > Build Settings > Build |

### 7.2 Command Line Tests

```bash
# Run edit mode tests
"C:/Program Files/Unity/Hub/Editor/6000.3.7f1/Editor/Unity.exe" -projectPath "C:/Sviluppo/Varie/knowledge" -runTests -testPlatform editmode -testResults "spec/test-results.xml" -logFile "spec/test-final.log"
```
