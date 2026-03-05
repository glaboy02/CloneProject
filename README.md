# Unity Practice Project

A collection of small classic arcade game clones built in **Unity 6** as a hands-on learning project to get comfortable with the Unity Engine and C# game development.

---

## Games

| Game | Description |
|---|---|
| **Pong** | Classic two-player paddle game |
| **Snake** | Grow your snake by eating food, avoid the walls and yourself |
| **Asteroids** | Shoot down incoming asteroids before they reach you |
| **Flappy Bird** | Navigate through obstacles by tapping to flap |
| **Tic-Tac-Toe** | Classic two-player strategy game |

---

## Tech Stack

- **Engine:** Unity 6 (6000.3)
- **Render Pipeline:** Universal Render Pipeline (URP)
- **Input System:** Unity Input System (`com.unity.inputsystem`)
- **Navigation:** Unity AI Navigation
- **Post Processing:** Unity Post Processing Stack

---

## Project Structure

```
Assets/
├── Scenes/              # One scene per game + main menu
├── Scripts/
│   ├── Asteroids Scripts/
│   ├── Flappy Bird Scripts/
│   ├── Pong Scripts/
│   ├── Snake Scripts/
│   ├── TicTacToe Scripts/
│   ├── Core/            # GameManager
│   ├── Save Manager/    # Persistent data
│   └── UI/              # Menu handling
├── Prefabs/
├── Materials/
└── Models/
```

---

## Getting Started

1. Clone the repository
2. Open the project in **Unity 6 (6000.3)** or later
3. Open `Assets/Scenes/MenuScene.unity`
4. Press **Play** and pick a game from the menu

---

## Purpose

This project is purely for learning purposes — practicing Unity workflows, scene management, physics, input handling, and UI across a variety of small game genres.
