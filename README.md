# CSE403-SP23-RegionAttack

## Idea


Region Attack is a math-based, strategic two-player game. 

### Goal

The goal of the game is to cover a larger connected region than your opponent. The game ends after a certain amount of time, after which the player with the largest enclosed region wins.

### Rules

The players' regions are built by placing randomly generated numbers from the player’s pool numbers onto the game board. Numbers can be placed next to either its multiple or divisor that is placed by the player. Primes can additionally be placed anywhere in the player’s half, as long as the multiple/divisor rule is not violated. One can overwrite the opponent's numbers by placing larger numbers on top and one can overwrite your own numbers while obeying the placing rules. When one player completely encloses a region all the opponent’s numbers are removed from the inside. 

## Vision

The game can teach kids basic mathematical concepts while having fun, and can keep the minds of seniors minds active. We believe that logical thinking and mathematical skills are useful, and this game can be a useful tool to train these skills. We plan to develop the game in predefined steps: a two-player version on one phone, one player against the computer version, and online matching of players.

## Repository Setup

The repository itself is a Unity project based on Unity version 2021.3.22. There is one directory for the weekly status reports called `reports`, and the rest are standard Unity project files. Most directories should not be altered as much to avoid confusion. For most of this project, everything that will/should be altered will reside in the `Assets` directory, where C# scripts will be kept in `Scripts` and prefabs will be kept in `Prefabs`.

To use git with Unity, the local repository may need to have [Git LFS](https://git-lfs.com/) installed and initialized. Additionally, there must be extra precautions made if modifying the included `.gitignore` and `.gitattributes` files as it may cause git to track unnecessary files or not use git LFS for large files.