# Knowledge - Unit Tests

## Framework
**NUnit** - Standard per Unity

## Struttura Test

```
Assets/Tests/
├── GameManagerTests.cs
├── PlayerControllerTests.cs
├── DiscoverySystemTests.cs
├── KnowledgeSystemTests.cs
├── EcosystemManagerTests.cs
├── WeatherSystemTests.cs
├── NPCManagerTests.cs
└── UIManagerTests.cs
```

## Esecuzione Test

### Unity Editor
1. Window > General > Test Runner
2. Run All o Run selezionati

### Command Line
```bash
# Windows
& "C:\Program Files\Unity\Hub\Editor\[VERSION]\Editor\Unity.exe" -batchmode -runTests -projectPath [PATH] -testResults [RESULTS] -testPlatform [PLATFORM]
```

## Copertura Test

| Classe | Test Count | Metodi Testati |
|--------|------------|----------------|
| GameManager | 8 | Singleton, KP, Era, Pause |
| PlayerController | 12 | Stats, Eat, Drink, LevelUp |
| DiscoverySystem | 12 | Combine, Recipes, Discovery |
| KnowledgeSystem | 11 | Categories, Total, Check |
| EcosystemManager | 14 | Species, Population, Biodiversity |
| WeatherSystem | 15 | Effects, Types, Modifiers |
| NPCManager | 13 | Reputation, Factions, Friends |
| UIManager | 11 | Toggle, Display, Dialog |

**Totale: 96 test**

## Naming Convention
`MethodName_Scenario_ExpectedResult`

## Struttura Test
```csharp
[TestFixture]
public class ClassNameTests
{
    [SetUp]     // Prima di ogni test
    [TearDown]  // Dopo ogni test
    
    [Test]
    public void TestName()
    {
        // Arrange
        // Act
        // Assert
    }
}
```
