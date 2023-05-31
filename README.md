# CSE403-SP23-RegionAttack
## Documentations
- [User Documentation](https://docs.google.com/document/d/1DH6M84srZDcjmnG6moEJAbBe9ikrCvG0c3ezxBntuCw/edit#): Find all relevant inforamtion for getting started with working on this project.
- [Developer Documentation](https://docs.google.com/document/d/11R98xWkRZ4r5YVof91nbMWEalpB5O88g-nbGZ-bJyGc/edit?usp=sharing): Find all relevant inforamtion to use this project.

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

## Project progress
Here you will find a list of usecases for our application. Alongside with them you will find the progress on each of them
- UC1: Play with friend in-person - Is fully working. Once the system is running two players can play a local game on the device it is running on.
- UC2: Play against Computer - Is fully working. Once the system is running a single player can play against a computer version. Note however that this computer player is following very simple rules.
- UC3: Learn the game - Is fully working. A tutorial mode for the game is available as another game mode.
- UC4: Play with a friend online - Currently further implementing the online mode is blocked by server setup.
- UC5: Add a friend online - Same progress as for UC4.
- UC6: Find a random Opponent - Same progress as for UC5.

Use Cases 4-6  were not developed. development of the server for these use cases was hindered, and any progress made exists on branchs starting with 'sri'. Most progress made is on branch "sri2-db".
