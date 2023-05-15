# CSE403-SP23-RegionAttack
## Documentations
- [User Documentation](https://docs.google.com/document/d/1DH6M84srZDcjmnG6moEJAbBe9ikrCvG0c3ezxBntuCw/edit#)
- [Developer Documentation](https://docs.google.com/document/d/11R98xWkRZ4r5YVof91nbMWEalpB5O88g-nbGZ-bJyGc/edit?usp=sharing)

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
- UC3: Learn the game - A tutroial mode for the game is currently being implemented and will be deployed soon.
- UC4: Play with a friend online - Currently further implementing the online mode is blocked by server setup.
- UC5: Add a friend online - Same progress as for UC4.
- UC6: Find a random Opponent - Same progress as for UC5.

# Getting started
If you are a new developer on this team or if you have forked this project and want to start working on it, this file contains all neccesary information to do so. 

## Setup
This project uses Unity and all requirements you need on your machine to get started stem from this framework. In the following all components that are needed are listed:
- [Unity Hub](https://unity.com/download): Install Unity Hub on your machine. If you are a student you can sign up for the [student plan](https://unity.com/products/unity-student) 
to get pro features for free.
- [Visual Studio](https://visualstudio.microsoft.com/downloads/): You need and IDE with Unity support for scripting. We recommend Visual Studio, but feel free to choose any other 
that meets the requirements.
- Unity Editor Version 2021.3.22f1: This is the version used for the project. If you choose another version complications cannot be ruled out.
  - If you want to build for iOS: You will need to install Xcode as well, which can be downloaded in the App Store. Note however that in order to do so you need to work on a Mac.
Further you will need to install the iOS build support in Unity Hub.
  - If you want to build for Android: You will need to install the 
[Android Studio](https://developer.android.com/studio?gclid=Cj0KCQjwu-KiBhCsARIsAPztUF3tI5ZMkR-qJYwDyOcMGLjgw4UNVCBeT1SYdJGsqf-ntpcNDqVp-GcaAolpEALw_wcB&gclsrc=aw.ds). 
Further you will need to install the Android build support in Unity Hub.
- GitHub: For convenience install git on your local machine to easily interact with the repository. The command to use in the terminal varies depending on your machine, see also
[here](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

Once all these requirements are met you can clone the repository to your local machine and open the project in Unity Hub.

## Testing the system
There are two ways to test your changes: 
1. Push them back to GitHub. As soon as you do so a GitHub Action workflow will run all tests that are available in the 
project.
2. You can run your tests locally. In the opened unity project open Window => General => Testrunner which will open the Testrunner. In this view you will see a 
hierarchy of tests. By double clicking on some element in this hierarchy all tests in test classes 
beneath it will be executed and either marked as red if failed or green if passed.

## Building the system
There are two potential ways to obtain the build for your target platform. 
1. Download the latest build from GitHub for your target platform. These builds 
are created by the GitHub Action [Build Project](https://github.com/RegionAttack/CSE403-SP23-RegionAttack/actions/workflows/build.yml) and provide 
builds for iOS, Android, and Windows.
2. You can also make your own build in the opened unity project. To do so follow these steps
  - Select File => Build Settings
  - Under Scenes in Build make sure that Scenes/GameBoard is selected
  - Under Platform select the platform you want to build for and click change platform. If you haven't installed the build support in the setup step you will be prompted to do so
  - Click Build and built to your target folder - !!If you want to run it on an android device don't do this but immediately go to the section "Running the system" from here on!!
  - Refer to the section "Running the system" to run your build

## Running the system
For just making a test run of the system, in order to observe local changes quickly, you can switch to playmode in unity which demonstrates the current behaviour of the game. 
<br>
<br>
In order to run a build (for creating a build refer to the section "Building the system") follow the steps for the respective platform:
- If you have built for either Windows, Mac, or Linux, make sure that you are on the specified platform and open the respective executable file. 
- If you have built for iOS follow these steps:
  - Open the Unity-iPhone.xcodeProj in Xcode
  - If never done, add your Apple ID in Xcode under Xcode => Preferences
  - Connect your iPhone with you Mac
  - In Unity-iPhone => General => Identity => BundleIdentifier, make sure you use a unique name for your Bundle Identifier. You can for example add -yourname to the predefined one
  - Go to Signing & Capabilities, check the automatically managing signing, and select your account as a team
  - Make sure your iPhone is choosen as a destination and click run
  - The build will now be deployed to your phone
  - In case you get and Untrusted Developer Warning on your iPhone, you can enable it in Settings => General => VPN & Device Management => Select your profile to trust
- If you have built for Android
  - Plug in your device & set it to charging mode
  - Make sure USB debugging is on
  - In Unity choose Build and Run at the end of the build process. See the section "Building the system". 
  - A window appears that prompts you to save a .apk file
  - After finishing the build, the app will run on your device
