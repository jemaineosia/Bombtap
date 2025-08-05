# Tapbomb - Game Design Document (MVP)

## 1. Overview
**Genre:** 2D Endless Dodge / Survival  
**Core Loop:** Pilot a ship to dodge falling bombs. Tap certain bombs for special effects.  
**Goal:** Survive as long as possible before HP or rising tide ends the game.

---

## 2. Gameplay Loop
1. **Spawn Phase:** Bombs drop from random X-positions at the top of the screen.  
2. **Interact Phase:** Player moves the ship and taps bombs to trigger effects—some taps help, others hinder.  
3. **Progression Phase:** The tide level rises over time, reducing safe space. Bomb variety and spawn rate increase.  
4. **End Phase:** Game ends when HP hits zero or tide reaches the ship.  
5. **Post-Game:** Display survival time, score, combos, and medals. Offer retry or continue options.

---

## 3. Core Mechanics
- **Movement:** Smooth left/right motion via keyboard (PC) or accelerometer (mobile).  
- **HP System:** Ship starts with 3 HP. Each bomb collision −1 HP.  
- **Tide:** A rising Y-position that gradually reduces dodge space; reaching ship level = Game Over.  
- **Scoring:**  
  - +1 point per second survived  
  - **Normal Bomb:** +5 points  
  - **Super Bomb:** +20 points  
  - Additional bonuses via combos.

---

## 4. Bomb Types & Power-Ups

### Bomb Types
- **Normal Bomb**  
  - Falls straight down at medium speed.  
  - **Tap:** Destroy → +5 points.

- **Smoke Bomb**  
  - Falls slowly; on tap releases a smoke cloud that obscures view.  
  - **Do Not Tap:** Cloud hides other bombs.

- **Scatter Bomb**  
  - Falls; on tap splits into three mini-bombs that scatter diagonally.  
  - **Mini-Bombs Untappable:** Deal 0.5 HP damage on hit.

- **Super Bomb**  
  - Falls slowly; on tap destroys **all** on‑screen bombs.  
  - **Tap:** Clear screen → +20 points.

### Power-Ups (Rare Drops)
- **Shield**  
  - 3s invulnerability; ship flashes silver.  
- **Time Freeze**  
  - Pauses bomb movement & spawning for 2s (tide continues).  
- **Health Pack**  
  - Restores 1 HP (up to max HP).

---

## 5. Controls & Input
- **PC:**  
  - Move Ship: ← / → arrows (or A/D)  
  - Tap Bombs: Mouse click

- **Mobile:**  
  - Move Ship: Tilt left/right via accelerometer  
  - Tap Bombs: Touch screen

_Calibration option_ to re-center tilt during gameplay.

---

## 6. UI & Feedback
- **Top Bar:** Timer (mm:ss), HP indicators (♥ ♥ ♥).  
- **Tide Indicator:** Rising graphic along screen edge.  
- **Visuals:** Bomb shadows, particle effects on destruction, screen shake on damage.  
- **Audio:** Distinct SFX per bomb type, rising-pitch tide alarm when ship is at risk.

---

## 7. Win & Lose Scenarios
- **Lose:**  
  - HP reaches 0  
  - Tide Y-position ≥ ship Y-position

- **Post-Game Screen:**  
  - Survival time, total points, best combo, medal awards (Bronze/Silver/Gold).  
  - Options: Retry, Watch Ad to Continue (1× restore).

---

## 8. Difficulty Progression
- **Time-Based Spawn Curves:**  
  - 0–30s: Normal bombs only, spawn every 1.0s  
  - 30–60s: +Smoke bombs, spawn every 0.9s  
  - 60–120s: +Scatter bombs, spawn every 0.75s  
  - 120s+: +Super bombs, start at 0.6s and decrease by 0.01s every 10s

- **Tide Acceleration:** Linear early, accelerates after 60s for tougher late-game.  
- **Dynamic Adjustment:** If player maintains long combo, spawn rate and tide speed +5%.

---

## 9. Additional Gameplay Elements
- **Combo System:** Consecutive correct taps build a multiplier (×1.1, ×1.2…). Resets on damage or wrong tap.  
- **Daily Challenges:** E.g., "Survive 45s without Super Bombs", "Tap 10 Normal Bombs" for coin rewards.  
- **Achievements & Leaderboards:** Track longest survival, highest score, most Super Bombs tapped.  
- **Tutorial:** First-run interactive guide covering movement, tapping, and bomb warnings.  
- **Visual/Audi Feedback:** Ship flash, splash when tide rises each segment.

---

## 10. MVP Scope & Roadmap
- **PC MVP Features:**  
  1. Ship movement & collision  
  2. Normal bombs & basic tap  
  3. Survival timer & HP system  
  4. Rising tide mechanic  
  5. Basic UI (timer, HP, tide indicator)  
  6. Basic sound SFX

- **Post-MVP / Mobile Transition:**  
  1. Accelerometer controls  
  2. Smoke, Scatter & Super bombs  
  3. Power-ups & tap logic  
  4. UI scaling & calibration  
  5. Polish visuals & audio

---

## 11. Technical Considerations
- **Engine:** Unity 2D, C#  
- **Scene Setup:** Single endless scene with object pooling for bombs/power-ups  
- **Bomb Spawner:** Spawn rate curve modulated by elapsed time  
- **Tide Controller:** Y-axis lerp or curve based on timer  
- **Input Manager:** Abstracts keyboard/mouse vs. accelerometer/touch  
- **Performance:** Target 60 FPS, optimize pooling and SFX triggers

---

## 12. Metrics & Tuning
- **Session Length:** Aim for average 2–5 min  
- **Conversion Points:** Monitor % of players reaching 60s, 120s  
- **Balance Tests:** Adjust spawn curves, tide speed, power-up frequency for “just-right” difficulty

---

## 13. Retention & Monetization (Optional)
- **Cosmetic Skins:** Unlockable ship trails, bomb designs via in-game currency  
- **Ad-Based Continue:** Watch ad to restore 1 HP once per game  
- **In-App Purchases:**  
  - Coin packs for power-ups/skins  
  - “Remove Ads” premium unlock

---

*End of Document*

