# Linee Guida di Scrittura del Codice - Knowledge

## 1. Convenzioni di Nomenclatura

### 1.1 Classi e Struct
- **PascalCase**: `PlayerController`, `DiscoverySystem`
- Nomi descriptivi che rispecchiano la responsabilità
- Un sostantivo o frase nominale

### 1.2 Metodi
- **PascalCase**: `GetPlayer()`, `InitializeGame()`
- Verbi che descrivono l'azione: `CalculateDamage()`, `UpdatePosition()`
- Proprietà: `public float Health { get; private set; }`

### 1.3 Variabili
- **camelCase**: `moveSpeed`, `currentHealth`
- Preferire nomi descrittivi a sigle
- Costanti: `MAX_HEALTH = 100f`

### 1.4 Namespace
- **PascalCase**: `Knowledge.Game`, `Knowledge.Environment`
- Struttura gerarchica: `Project.Module.SubModule`

---

## 2. Struttura del File

```csharp
using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public class ExampleClass : MonoBehaviour
    {
        // region Fields
        [Header("Settings")]
        public float exampleField;
        
        private float _privateField;
        
        // region Properties
        public float PublicProperty { get; private set; }
        
        // region Unity Methods
        private void Awake() { }
        private void Start() { }
        private void Update() { }
        
        // region Public Methods
        public void PublicMethod() { }
        
        // region Private Methods
        private void PrivateMethod() { }
    }
}
```

---

## 3. Regole di Codifica

### 3.1 Visibilità
- Usare `private` esplicitamente quando possibile
- Esporre tramite proprietà, non campi pubblici
- `[SerializeField]` per campi privati editabili nell'editor

### 3.2 Modificatori
- Metodi virtuali: `protected virtual`
- Override: `protected override`
- Statici: `public static`

### 3.3 Costanti
```csharp
public const int MAX_LEVEL = 100;
private const float BASE_SPEED = 5f;
```

### 3.4 Enums
```csharp
public enum AnimalDiet { Herbivore, Carnivore, Omnivore, Detritivore }
```

---

## 4. Commenti e Documentazione

### 4.1 Regole
- Commentare il "perché", non il "cosa"
- Documentare API pubbliche con summary
- Mantenere commenti aggiornati

### 4.2 Formato
```csharp
/// <summary>
/// Initializes the game manager and sets up all subsystems.
/// </summary>
public void Initialize() { }
```

---

## 5. Struttura delle Classi

### 5.1 Singleton
```csharp
public static ClassName Instance { get; private set; }

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

### 5.2 MonoBehaviour
- Un solo `Awake()` per gerarchia
- Referenze tramite `GetComponent<T>()`
- Usare `RequireComponent` quando necessario

---

## 6. Gestione Errori

### 6.1 Debug
```csharp
#if UNITY_EDITOR
Debug.Log("Debug info");
#endif
```

### 6.2 Validazione
```csharp
if (player == null)
{
    Debug.LogWarning("Player not found!");
    return;
}
```

---

## 7. Performance

### 7.1 Caching
- Cache delle referenze in `Awake()` o `Start()`
- Evitare `GetComponent` in `Update()`

### 7.2 Coroutine
- Preferire coroutine per operazioni temporizzate
- Usare `yield return null` invece di `WaitForSeconds` quando possibile

---

## 8. Testing

### 8.1 Unit Tests
- Testare metodi pubblici
- Mockare dipendenze esterne
- Naming: `MethodName_Scenario_ExpectedResult`

### 8.2 Struttura Test
```csharp
[TestFixture]
public class GameManagerTests
{
    [Test]
    public void AddKnowledgePoints_ValidAmount_IncreasesKP()
    {
        // Arrange
        var gm = new GameManager();
        
        // Act
        gm.AddKnowledgePoints(10);
        
        // Assert
        Assert.AreEqual(10, gm.TotalKnowledgePoints);
    }
}
```

---

## 9. Ordine di lettura dei File

1. Namespace
2. Using
3. Class/Struct declaration
4. Constants
5. Fields (serialized, private)
6. Properties
7. Events
8. Unity lifecycle methods (Awake, Start, Update, etc.)
9. Public methods
10. Private methods

---

## 10. Best Practices

- **SOLID Principles**: Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion
- **DRY**: Don't Repeat Yourself
- **YAGNI**: You Aren't Gonna Need It
- Preferire la composizione all'ereditarietà
- Usare ScriptableObjects per dati condivisi
- Serializzare solo ciò che serve
