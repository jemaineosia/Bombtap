# Game Design Document (GDD) – Tapbomb

## Game Title: **Tapbomb**

---

## 🔥 Core Gameplay
Tapbomb is a 2D mobile reflex game where players control a ship to dodge and destroy falling bombs using precise tapping. The gameplay intensifies over time as bomb variety, speed, and density increase.

---

## 🎯 Objective
Survive as long as possible while dodging falling bombs and using tap abilities strategically.

---

## 🕹 Player Controls
- Tap on bombs to destroy them.
- Drag to move the ship left or right.
- Tap Super Bomb to activate an AOE explosion within range.

---

## 🚢 Player
- Represented by a small ship at the bottom center.
- The ship floats up and down with the tide level.
- HP: 3 (Game ends when HP reaches 0).

---

## 💣 Bomb Types (Falling From Top)
### 1. **Standard Bomb**
- Falls at normal speed.
- Destroyed by tap.

### 2. **Speed Bomb**
- Falls faster than normal bombs.
- Requires quicker reaction.

### 3. **Smoke Bomb**
- Leaves behind a smoke cloud temporarily obscuring vision.

### 4. **Armor Bomb**
- Requires two taps to destroy.

### 5. **Super Bomb**
- Falls slowly.
- On tap, detonates nearby bombs in a radius.
- Dangerous if nearby bombs are close to the ship.

---

## 🌊 Tide System
- Tide level gradually rises every 20 seconds, reducing safe space.
- Returns to normal after a few seconds.
- Ship follows the tide up/down.

---

## ⚠️ Game Phases
### 1. **Normal Phase**
- Standard bomb drop speed.
- Bombs are sparse.

### 2. **Progression Phase**
- Tide rises temporarily.
- Bomb drop speed and spawn rate increase.
- More bomb variety introduced.

### 3. **Storm Surge Phase**
- Heavy bombfall with reduced visibility (more smoke bombs).
- Super bombs appear.

### 4. **End Phase**
- Game ends when HP hits zero.

---

## 🧠 Bomb Spawning Logic (Updated: Lane System)
- Bombs now spawn using **lane-based system**.
- Screen divided into multiple vertical lanes (e.g. 5 lanes).
- Each bomb falls from a random or pre-defined lane.
- Ensures spatial challenge and strategy as bombs can stack in a lane.

**Spawn Interval**: Decreases as the game progresses.
**Spawn Variety**: Random with weighted probabilities (early game favors standard bombs, late game favors complex bombs).

---

## 🎮 UI Elements
- Score (Survival Time).
- HP Indicator (3 hearts).
- Bomb Counter (Optional for super bombs).
- Tide Alert Notification.

---

## 📈 Progression and Difficulty
- Bombs increase in speed and complexity over time.
- New bomb types introduced gradually.
- Tide levels increase with time.

---

## 📦 Assets
- Ship sprite
- Bomb sprites (5 types)
- Tide background animation
- Smoke overlay for smoke bomb
- UI assets (buttons, health bar, timer)

---

## 💡 Suggestions for Future
- Power-ups (Slow motion, Shield)
- Boss Bombs (Periodic challenges)
- Upgradeable ship skins
- Daily challenges and scoreboards

---

Let me know if you'd like wireframes or flowcharts added next.

