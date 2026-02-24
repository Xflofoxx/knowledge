# Knowledge

A 3D open-world RPG/simulation game about human evolution, from the Stone Age to the conquest of space.

## About the Game

**Knowledge** is a game based on discovery and evolution. The player explores a living world, collects resources, and discovers new tools and objects through a unique combination system. The central theme is **knowledge** - every discovery advances humanity through time.

### Core Features

- **Discovery System**: Combine objects to discover new tools and technologies
- **8 Eras**: From Stone Age to Space Age
- **Ecosystem**: 30+ animal species with realistic behaviors
- **Dynamic Weather**: Climate affects gameplay with natural disasters
- **NPC Interactions**: Reputation system with factions
- **Character Customization**: Create your character with various options

## Getting Started

### Prerequisites

- Unity 2021.3 LTS or later
- Visual Studio or VS Code
- .NET SDK

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Xflofoxx/knowledge.git
   ```

2. Open the project in Unity Hub

3. Open the project in Unity Editor

4. Build and run

## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/              # GameManager
│   ├── Player/            # PlayerController, CharacterEditor
│   ├── Discovery/         # DiscoverySystem
│   ├── Systems/           # KnowledgeSystem
│   ├── Environment/        # EcosystemManager, WeatherSystem
│   ├── AI/                # NPCManager
│   └── UI/                # UIManager
├── Tests/                 # Unit tests
├── Prefabs/               # Game objects
└── Scenes/                # Unity scenes
```

## Contributing

### Code Style

We follow C# coding standards and Unity best practices:

- Use **PascalCase** for classes, methods, properties
- Use **camelCase** for variables
- Add `[SerializeField]` for private fields editable in Inspector
- Use `readonly` and `const` where appropriate
- Add XML documentation for public APIs

See `spec/CODING_GUIDELINES.md` for detailed guidelines.

### Branch Naming

- `feature/description` - New features
- `fix/description` - Bug fixes
- `refactor/description` - Code refactoring
- `docs/description` - Documentation updates

### Commit Messages

Use clear, descriptive commit messages:

```
feat: add new discovery recipes
fix: resolve player death respawn issue
refactor: improve ecosystem performance
docs: update README
```

### Pull Request Process

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
6. Wait for review and address feedback

### Testing

Run unit tests in Unity:
- Window > General > Test Runner > Run All

Required test coverage:
- All public methods must have unit tests
- Integration tests for system interactions

## Built With

- [Unity](https://unity.com/) - Game Engine
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Programming Language
- [NUnit](https://nunit.org/) - Testing Framework

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Unity Technologies
- Open source community
- Contributors

## Contact

For questions or suggestions, please open an issue on GitHub.
