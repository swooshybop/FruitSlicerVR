# FruitSlicerVR

**FruitSlicerVR** is a VR mini-game inspired by Fruit Ninja, built using Unity and the XR Interaction Toolkit. 
This project was created as part of my learning journey in VR development and includes a basic fruit-slicing mechanic, score tracking, and a countdown timer.

---

## Current Features

- **Fruit Spawning System**  
  Random fruits appear during gameplay at timed intervals.

- **Slicing Mechanic**  
  Instead of using a mesh slicer, the game uses pre-split fruit prefabs. Each fruit has:
  - A full fruit prefab
  - A left-half prefab
  - A right-half prefab  
  When sliced, the full fruit is replaced with its corresponding sliced halves.

- **Score Tracking**  
  Points are awarded for slicing fruits correctly.

- **Countdown Timer**  
  The game runs for a limited time.

---

## Built With

- Unity **2021.3.45f1**
- XR Interaction Toolkit (for VR setup and interaction)
- Tested using Meta Quest 3

---

## Future Plans

- Add **sound effects** for slicing and background music
- Implement **level progression** with difficulty scaling
- Add **prefab switching in-game** (e.g., change knife or fruit types)
- Include a **gameplay video** to show progress

---
