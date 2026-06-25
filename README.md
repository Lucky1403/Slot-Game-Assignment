# 🎰 Slot Machine Game — Unity Assignment

A fully functional 2D slot machine game built in Unity as part of a hiring assignment. The game features smooth reel animations, a betting system, win/loss detection, sound effects, lever animation, and a WebGL build.

---

## 📋 Table of Contents

- [Game Overview](#game-overview)
- [How to Run WebGL Build](#how-to-run-webgl-build)
- [Game Instructions](#game-instructions)
- [Features](#features)
- [Bonus Features](#bonus-features)
- [Technical Architecture](#technical-architecture)
- [Folder Structure](#folder-structure)
- [Thought Process & Approach](#thought-process--approach)
- [Win Conditions & Payouts](#win-conditions--payouts)

---

## 🎮 Game Overview

This is a classic 3-reel slot machine game built entirely in Unity using C# and the Unity UI system. The player starts with 100 credits and places bets to spin the reels. Matching all 3 middle symbols results in a win.

The game uses all provided assets including symbols (Seven, Cherry, Bell, BAR), machine body layers, lever sprites, button sprites, and popup panels.

---

## 🌐 How to Run WebGL Build

### Option 1 — Run Locally
1. Navigate to the `/Build/WebGL/` folder in this repository
2. You need a local server to run WebGL builds (browsers block local file access)
3. If you have Python installed, open terminal in the WebGL folder and run:
   ```bash
   # Python 3
   python -m http.server 8080
   ```
4. Open your browser and go to `http://localhost:8080`
5. The game will load in your browser

### Option 2 — Direct Browser
1. Clone or download this repository
2. Open the `/Build/WebGL/index.html` file using a local server as described above
3. Recommended browsers: **Chrome** or **Firefox** (latest version)


---

## 🕹️ Game Instructions

1. Game launches with 100 credits
2. **Place Bet** — Click the `Place BET` button to toggle the bet panel
   - Use `◀` and `▶` arrows to adjust bet (10 / 20 / 30 / 40 / 50)
3. **Spin** — Click the **lever** to spin the reels
4. **Wait** — Reels spin and stop one by one (left to right)
5. **Result** — A popup appears showing win or loss
6. **Continue** — Click the ✕ button on the popup to spin again
7. **Out of Credits** — A Play Again popup appears with YES/NO options

---

## ✅ Features

### Core Features
- ✅ **3-Reel Slot Machine** with 4 unique symbols (Seven, Cherry, Bell, BAR)
- ✅ **Smooth Reel Animations** — symbols flicker rapidly and gradually slow down before stopping
- ✅ **Staggered Reel Stops** — Reel 1 stops first, then Reel 2, then Reel 3 (classic feel)
- ✅ **Winning Logic** — Player wins when all 3 middle symbols match
- ✅ **Randomized Outcomes** — Unity's `Random.Range` used for fair RNG on every spin
- ✅ **Payout System** — Different payouts per symbol.
- ✅ **Credits System** — Starts at 100, deducts bet per spin, adds winnings on win
- ✅ **Win Popup** — Shown on wins with symbol name and amount won
- ✅ **Loss Popup** — Shown on losses with "Try Again" message
- ✅ **Play Again Popup** — Shown when credits run out, YES refills credits, NO ends game

### Visual Features
- ✅ **Layered Scene** — Background → Machine body → Symbols → Reel frames → UI
- ✅ **Lever Animation** — Lever pulls down smoothly with bounce spring-back effect
- ✅ **Win Flash Animation** — Middle row symbols flash gold on a normal win
- ✅ **Jackpot Flash Animation** — All 9 symbols flash rapidly on 777 jackpot
- ✅ **Loss Shake Animation** — Middle symbols shake on a loss
- ✅ **Win Line Highlight** — Highlighted bar across middle row fades in/out on wins

### Audio Features
- ✅ **Lever Pull Sound** — Plays when lever is pulled
- ✅ **Spin Loop Sound** — Looping sound while reels are spinning
- ✅ **Reel Stop Sounds** — Click sound plays as each reel stops (3 times)
- ✅ **Win Sound** — Plays on a normal win
- ✅ **Jackpot Sound** — Special sound on 777 jackpot
- ✅ **Loss Sound** — Plays when no win

---

## 🌟 Bonus Features

### Betting System
- Adjustable bet from **10 to 50** in steps of 10
- Arrow buttons automatically show **disabled sprite** at min/max bet limits
- Bet buttons locked during spin to prevent mid-spin changes
- **Last Win display** shows most recent win amount

### Bet Panel Toggle
- Collapsible bet panel with **smooth slide animation**
- Toggle button always visible on screen
- Panel slides in from below when opened, slides out when closed

### Smart UI Behavior
- Spin button (Lever) **disabled during spinning** — cannot double-spin
- Spin button (Lever) **only re-enabled when popup is closed** — forces player to acknowledge result
- Bet buttons **locked during spin** — prevents bet change mid-animation

---

## 🏗️ Technical Architecture

### Scripts Overview

| Script | Responsibility |
|---|---|
| `SlotManager.cs` | Main controller — coordinates all systems, handles spin sequence |
| `ReelController.cs` | Controls one reel's 3 symbols, handles spin and slowdown animation |
| `WinChecker.cs` | Static class — checks 3 results, returns payout amount |
| `UIManager.cs` | All UI updates — credits, popups, button states |
| `BetManager.cs` | Bet amount control, last win display, bet button states |
| `SoundManager.cs` | Singleton — manages all audio clips and playback |
| `WinAnimator.cs` | Win/loss visual animations — flash, shake, win line fade |
| `LeverAnimator.cs` | Lever pull-down and spring-back animation using scale lerp |
| `BetPanelToggle.cs` | Smooth slide toggle for the bet panel |

### Key Design Decisions

**UI-Based Architecture**
All visuals are built using Unity's Canvas UI system. This is ideal for 2D slot games because it gives precise control over layering, anchoring, and screen scaling.

**Coroutine-Based Spin Sequence**
The entire spin flow (lever → reels → stop sounds → win check → animation → popup) is handled by a single `SpinSequence` coroutine in `SlotManager`. This makes the timing easy to follow and modify.

**Cached Color Restoration**
`WinAnimator` caches the original color of every symbol image at `Start()`. After animations, it restores exact original colors — preventing any alpha or tint drift across multiple spins.

**Singleton SoundManager**
`SoundManager` uses a singleton pattern so any script can call `SoundManager.Instance.PlayWin()` without needing a direct reference. Two AudioSources are used — one for one-shot SFX, one for the looping spin sound.

---

## 📁 Folder Structure

```
Assets/
├── Animations/
├── Prefabs/             # Reusable prefabs
├── Scenes/
│   └── MainScene.unity  # Main game scene
├── Scripts/
│   ├── SlotManager.cs
│   ├── ReelController.cs
│   ├── WinChecker.cs
│   ├── UIManager.cs
│   ├── BetManager.cs
│   ├── SoundManager.cs
│   ├── WinAnimator.cs
│   ├── LeverAnimator.cs
│   └── BetPanelToggle.cs
├── Sounds/              # Audio clips (SFX)
├── UI/             # All game sprites and UI assets
└── TextMesh Pro/        # TMP resources

Build/
└── WebGL/               # WebGL build output
    ├── index.html
    ├── Build/
    └── TemplateData/
```

---

## 💡 Thought Process & Approach

### 1. Understanding the Assets First
Before writing any code, I analyzed all provided assets carefully:
- `slot-machine1` — machine body with empty white reel windows (main background layer)
- `slot-machine2/3` — isolated lever in up/down positions (for lever animation)
- `slot-machine5` — just the 3 reel window frames (overlay on top of symbols)
- `slot-symbol1/2/3/4` — the 4 playable symbols
- Button sprite sheets — sliced into individual states (normal, hover, pressed, disabled)

This analysis shaped the entire layering strategy before a single GameObject was placed.

### 2. Layering Strategy
The scene uses a deliberate layer order:
```
bg_gradient → slot-machine1 → Symbols (9 images) → slot-machine5 frames → Lever → UI
```
This creates the illusion of symbols sitting inside the machine windows without any masking complexity.

### 3. Simplest Effective Animation
Rather than using Unity's Animator or DOTween, I chose coroutine-based animation with `yield return null`. This keeps all logic in C# without dependencies, will work perfectly in WebGL, and gives full control over timing and easing.

### 4. Separation of Concerns
Each script has one clear job. `SlotManager` orchestrates but doesn't do the work itself — it delegates to `ReelController`, `WinChecker`, `UIManager`, `BetManager`, `SoundManager`, `WinAnimator`, and `LeverAnimator`. This makes the code easy to read, test, and extend.

### 5. WebGL Compatibility
All choices were made with WebGL in mind:
- No external dependencies or plugins
- No `Application.Quit()` calls
- Standard Unity UI and AudioSource only
- Canvas Scaler set to Scale With Screen Size for responsive layout

---

## 💰 Win Conditions & Payouts

| Combination | Payout | 
|---|---|
| 7️⃣ 7️⃣ 7️⃣ | 500 (JACKPOT) |
| 🍒 🍒 🍒 | 200 |
| 🔔 🔔 🔔 | 150 |
| BAR BAR BAR | 100 |
| Any mismatch | 0 |

---

## 🛠️ Built With

- **Engine:** Unity 2022.3.60f1 LTS
- **Language:** C#
- **UI System:** Unity Canvas (Screen Space Overlay)
- **Audio:** Unity AudioSource
- **Build Target:** WebGL
- **Version Control:** Git + GitHub

---
