# Transport Tycoon

A 3D transport management simulation game built in Unity 6. Build road networks, manage a fleet of vehicles, trade commodities between facilities, and grow your transportation empire — all while keeping your budget in the black.

> Developed as a university coursework project for the ELTE Szoftvertechnológia course (Group 06, 2026).

---

## Gameplay

- **Build** roads, bus stops, traffic lights, and production facilities (farms, factories, mines, lumber mills) on a 100×100 tile map
- **Buy vehicles** — trucks, minivans, buses, and cars — and assign them routes between locations
- **Trade commodities** such as Iron, Steel, Coal, Wood, Milk, Eggs, and Cheese between cities and production facilities
- **Transport passengers** between cities using buses and cars
- **Manage your budget** — every purchase costs money, and vehicles degrade over time
- **Save and load** your game at any point

---

## Getting Started

### Requirements

- **Unity 6000.3.7f1** (or compatible)
- Universal Render Pipeline (URP) — included via Package Manager
- .NET / C# 9+

### Running the Project

1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd szoftech-csoport-of-aces
   ```
2. Open the project in **Unity Hub** using Unity 6000.3.7f1.
3. Open the `MainMenu` scene from `Assets/Scenes/MainMenu.unity`.
4. Press **Play** in the Unity Editor, or build the project via **File → Build Settings**.

---

## Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenu.unity          # Start screen
│   ├── GameScreen.unity        # Main gameplay scene
│   └── GameMenu.unity          # Pause / game-over screen
├── Scripts/
│   ├── Model/                  # Core game logic (Unity-independent)
│   │   ├── Game.cs             # Central game controller & time management
│   │   ├── Player.cs           # Player state (money, owned assets)
│   │   ├── Map.cs              # 100×100 tile grid
│   │   ├── Entities/           # City, Crossroad, base GameEntity
│   │   ├── Facilities/         # Farm, Factory, Mine, LumberMill, Road, BusStop...
│   │   ├── Vehicles/           # Bus, Car, Truck, Minivan + movement & routing
│   │   ├── Resources/          # Commodity and Food resource types
│   │   └── Algorithms/         # PathFinder, Prices
│   ├── ViewModel/              # Unity-side: UI binding, input, camera
│   │   ├── GameViewModel.cs    # Central UI controller & game-mode state
│   │   ├── Binding/            # 3D visual objects for all map entities
│   │   ├── GameScreen/         # UI handlers (menu bar, minimap, builder selector…)
│   │   └── ObjectDataDisplay/  # Info panels for selected objects
│   └── Persistence/            # Save / load (JSON via Newtonsoft.Json)
│       ├── DataAccess.cs
│       ├── Save.cs / Load.cs
│       └── EntityFactory.cs
├── Test/                       # Edit-mode unit tests (14 test files)
└── TestsPlayMode/              # Play-mode integration tests (10 test files)
```

---

## Architecture

The project follows an **MVVM (Model-View-ViewModel)** pattern:

| Layer | Responsibility |
|---|---|
| **Model** | All game rules and state — completely decoupled from Unity |
| **ViewModel** | Bridges the model to Unity: handles input, scene events, and UI updates |
| **View** | Unity scenes, prefabs, and 3D visual scripts |
| **Persistence** | Serializes/deserializes game state to disk using txt |

Key design patterns used:
- **Singleton** — `Game`, `GameViewModel`
- **Factory** — `EntityFactory` for reconstructing entities on load
- **Interface segregation** — `IBuildable`, `ITradeable`, `IUpdatable`, `IDataAccess`
- **Custom exceptions** — `NotEnoughMoneyException`, `FieldOverrideException`, `VehicleConditionException`, etc.

---

## Technologies

| Tool / Package | Version | Purpose |
|---|---|---|
| Unity | 6000.3.7f1 | Game engine |
| Universal Render Pipeline | 17.3.0 | 3D rendering |
| Unity Input System | 1.18.0 | Input handling |
| Unity AI Navigation | 2.0.9 | Pathfinding (NavMesh) |
| Unity Test Framework | 1.6.0 | Unit & integration testing |
| Unity Code Coverage | 1.3.0 | Coverage reporting |
| Moq + Castle.Core | 4.20.72 / 5.1.1 | Mocking in tests |

---

## Testing

The project has two test suites:

**Edit-mode unit tests** (`Assets/Test/`) — test game logic in isolation:
- `PlayerTestScript`, `MapTestScript`, `VehicleTestScript`, `GameTestScript`
- `CityTestScript`, `BusStopTestScript`, `ProdFacilityTestScript`
- `AlgorithmTestScript`, `ResourceTestScript`, `ExceptionTestScript`, and more

**Play-mode integration tests** (`Assets/TestsPlayMode/`) — test UI and scene behaviour:
- `MainMenuTest`, `MenuBarTest`, `BuilderSelectorTest`
- `VehicleRouteTest`, `GameOverDisplayTest`, `ErrorDisplayTest`
- Object data display tests for vehicles, facilities, cities, and traffic lights

Run tests via **Window → General → Test Runner** in the Unity Editor.

---

## CI/CD (GitLab ONLY - NOT ON GitHub)

A GitLab CI/CD pipeline (`.gitlab-ci.yml`) runs automatically on each push:
- Builds the project
- Executes the test suite
- Generates a code coverage report

---

## Team

**Szoftech Csoport — of Aces** | ELTE Group 06, 2026
