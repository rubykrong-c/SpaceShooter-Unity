# Архитектура проекта

## Общий обзор
- **Dependency Injection:** Проект собран на Zenject. `ApplicationInstaller` отвечает за глобальные сервисы и фабрики состояний, сцены (MainMenu, Gameplay и пр.) имеют собственные инсталлеры, в которых подключаются UI, контроллеры и данные.
- **Состояния:** `ApplicationStatesController` запускает верхнеуровневый конечный автомат (`LoadingState → MainMenuState → GamePlayState`). Внутри геймплея работает отдельная FSM (`Code/Gameplay/Core/FSM`), которая реагирует на сигналы (`GameplayStateMachineStatesSignals`).
- **Сигналы:** `SignalBus` используется для обмена событиями между подсистемами: `ApplicationSignals` (запуск/выход), `MainMenuStateSignals`, `GameplaySignals` (победа, поражение, выход, смерть корабля).

## Точки входа и последовательности
### Запуск приложения
1. Unity сцена поднимает `ApplicationInstaller`.
2. `ApplicationStatesController.Initialize()` ставит стартовое состояние `LoadingState`.
3. После загрузки ресурсов вызывается `MainMenuState`, где `MainMenuScreenPresenter` запрашивает у `LevelProgressService` текущий уровень и конфигурирует UI.
4. Нажатие на Play через сигнал переводит FSM в `GamePlayState`.

### Цикл геймплея
1. `GamePlayState` загружает сцену Gameplay и исполняет `GameplayInstaller`.
2. Инсталлер биндит контроллеры (`GameplayController`, `ShipController`, `LevelLoaderController`, `AsteroidSpawner`, `LevelAsteroidCounter`, `InputHandler`, презентеры UI и т.д.).
3. Состояние `PregameplayState` вызывает `LevelLoaderController.StartLevel()`, который:
   - Берёт данные у `LevelProgressService`.
   - Передаёт их в `LevelAsteroidCounter` и запускает асинхронный спавн астероидов через `AsteroidSpawner`.
4. В бою:
   - `ShipController` создаёт `ShipView`, подписывается на ввод (`InputHandler`), управляет HP (`ShipModel`) и стреляет через `ShipWeaponController`/`BulletSpawner`.
   - Астероиды конфигурируются через `IAsteroidMovementResolver`, движения реализованы стратегиями (`LinearDownMovement`, `SinusMovement`, `LinearForwardMovement`).
   - `CollisionHandler` обрабатывает столкновения и вызывает `Despawn` у соответствующих спавнеров.
   - `LevelAsteroidCounter` считает уничтоженные астероиды и, когда их не остаётся, шлёт `GameplaySignals.OnCurrentLevelCompleted_Debug`.
5. `GameplayController` реагирует на сигналы победы/поражения:
   - При победе вызывает `ILevelProgressWriter.CompleteCurrentLevelAndGenerateNext()` и переводит FSM в состояние завершения.
   - При поражении или выходе инициирует возврат к предыдущему состоянию приложения.

## Важные модули
### Сохранения и прогресс
- **SaveSystemInstaller** — связывает сервис сохранений.
- **LevelProgressService** — единственная точка работы с уровневым прогрессом: читает/создаёт сейвы, хранит массив `LevelData`, генерирует новые уровни через `ILevelParamsGenerator` и отдаёт данные презентерам/контроллерам.

### Генерация уровней
- **GeneratorLevels** (`ILevelParamsGenerator`) генерирует параметры (rate, набор sub-level’ов) до первого запуска и при открытии новых уровней.
- **LevelAsteroidCounter** отслеживает победное условие, не привязываясь к UI/контроллерам.

### Геймплей
- **LevelLoaderController** — управляет асинхронным спавном астероидов по параметрам уровня, умеет стартовать/останавливать волну и просит `AsteroidSpawner` уничтожить объект.
- **AsteroidSpawner / BulletSpawner** — обёртки над `PoolingSystem`. Настраивают материалы, скорость, движения, подписывают пул-бихейворы на обработчик столкновений.
- **CollisionHandler** — центральное место, которое решает, что делать при попадании пули, выходе за экран и т.д., не завязывая снаряды/астероиды на уровневый контроллер.
- **ShipController** — рулит кораблём: создаёт `ShipView`, ограничивает движение границами камеры (`CameraContainer`), синхронизирует HP и оружие.

### UI (MVP)
- Каждый экран реализован через пару Presenter/View. Например, `GameplayCoreScreenPresenter` слушает `ShipModel.HpChanged`, чтобы обновлять текст HP, и отправляет сигналы победы/поражения/выхода.
- В MainMenu Presenter запрашивает у `LevelProgressService` структуру уровней и отмечает текущий, а нажатие Play инициирует запуск геймплея.

## Примечания
- **Camera & Input**: `CameraController` предоставляет `CameraContainer`, `InputHandler` ретранслирует события Unity UI в контроллер корабля.
- **Расширяемость:** благодаря стратегиям движений, отдельному счётчику победы и сигналам можно добавлять новые типы астероидов, условия или UI без изменения базовых контроллеров.
