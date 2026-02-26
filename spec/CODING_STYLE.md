# Knowledge Style Guide

> **Version**: 1.0.0  
> **Status**: Mandatory

This style guide is mandatory for all Knowledge codebase. It ensures consistency, readability, and maintainability.

## 1. General Principles

| Principle | Description |
|-----------|-------------|
| Clarity over Cleverness | Write code that is easy to understand |
| Consistency | Follow established patterns in the codebase |
| Minimalism | Avoid unnecessary complexity or abstraction |
| Self-Documenting | Use clear names; add comments only for non-obvious logic |

## 2. C# Conventions (Unity)

### 2.1 Naming Conventions

| Type | Convention | Example |
|------|------------|---------|
| Classes/Interfaces | PascalCase | `PlayerController`, `DiscoverySystem` |
| Methods | PascalCase | `GetPlayer()`, `InitializeGame()` |
| Properties | PascalCase | `MaxHealth`, `CurrentSpeed` |
| Private fields | camelCase | `moveSpeed`, `currentHealth` |
| Constants | UPPER_SNAKE_CASE | `MAX_HEALTH`, `DEFAULT_SPEED` |
| Enum values | PascalCase | `AnimalDiet.Herbivore` |
| Namespaces | PascalCase | `Knowledge.Game`, `Knowledge.Environment` |

### 2.2 Unity-Specific Patterns

```csharp
namespace Knowledge.Game
{
    public class PlayerController : MonoBehaviour
    {
        #region Constants
        private const float DefaultMaxHealth = 100f;
        private const float DefaultMoveSpeed = 5f;
        #endregion

        [Header("Movement")]
        [SerializeField] private float moveSpeed = DefaultMoveSpeed;

        public float MoveSpeed => moveSpeed;

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}
```

### 2.3 Property Patterns

```csharp
// Read-only property with private setter
public float Health { get; private set; }

// Property with public setter for testing
public int NatureKnowledgeField { get => natureKnowledge; set => natureKnowledge = value; }

// Auto-property
public bool IsGamePaused => gamePaused;
```

## 3. Unity Best Practices

### 3.1 MonoBehaviour

```csharp
// Use RequireComponent for dependencies
[RequireComponent(typeof(CharacterController))]
public sealed class PlayerController : MonoBehaviour
{
    // Cache references in Awake
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
}
```

### 3.2 Serialization

```csharp
[Header("Character Settings")]
[SerializeField] private int age = 25;
[Range(18, 100)]
[SerializeField] private float height = 1f;
```

### 3.3 Singletons

```csharp
public static KnowledgeSystem Instance { get; private set; }

private void Awake()
{
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
}
```

## 4. Error Handling

```csharp
#if UNITY_EDITOR
Debug.Log("Debug info");
#endif

if (player == null)
{
    Debug.LogWarning("Player not found!");
    return;
}
```

## 5. Performance

- Cache component references in `Awake()` or `Start()`
- Avoid `GetComponent` in `Update()`
- Use struct for small data types
- Use object pooling for frequently created/destroyed objects

## 6. Testing

### 6.1 Test File Naming

```
Assets/Scripts/Player/PlayerController.cs → Assets/Tests/Player/PlayerControllerTests.cs
```

### 6.2 Test Structure

```csharp
using NUnit.Framework;

[TestFixture]
public class PlayerControllerTests
{
    [Test]
    public void TakeDamage_ValidAmount_DecreasesHealth()
    {
        // Arrange
        var controller = new PlayerController();
        
        // Act
        controller.TakeDamage(10f);
        
        // Assert
        Assert.Less(controller.Health, 100f);
    }
}
```

### 6.3 Making Code Testable

```csharp
// Add public setters for private fields needed by tests
public int NatureKnowledgeField { get => natureKnowledge; set => natureKnowledge = value; }

// Add properties for private fields
public bool IsGamePaused => gamePaused;
```

## 7. File Organization

Order in each file:
1. Namespace
2. Using statements
3. Class declaration
4. Constants
5. Fields (serialized, private)
6. Properties
7. Unity lifecycle methods (Awake, Start, Update, etc.)
8. Public methods
9. Private methods

## 8. Git Commits

```
# Use conventional commits
feat: add player sprint functionality
fix: resolve health depletion bug
docs: update API documentation
test: add unit tests for discovery system
refactor: simplify knowledge tree logic

# Avoid
fixed stuff
update
asdf
```

## 9. Code Review Checklist

- [ ] Code follows naming conventions
- [ ] Properties have correct access modifiers
- [ ] No direct field access from tests (use properties)
- [ ] Error handling is appropriate
- [ ] No hardcoded secrets
- [ ] Code is tested
- [ ] Performance considerations addressed

## 10. Quality Gates

Before any code is merged:

- [ ] All Unity tests pass
- [ ] No compilation errors
- [ ] Code follows style guide
- [ ] No debug logs in production code
