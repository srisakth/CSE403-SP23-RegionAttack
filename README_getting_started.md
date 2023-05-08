# Getting started
If you are a new developer on this team or if you have forked this project and want to start working on it, this file contains all neccesary information to do so. 

## Setup
This project uses Unity and all requirements you need on your machine to get strated stem from this framework. In the following all components that are needed are listed:
- [Unity Hub](https://unity.com/download): Install Unity Hub on your machine. If you are a student you can sign up for the [student plan](https://unity.com/products/unity-student) 
to get pro features for free.
- [Visual Studio](https://visualstudio.microsoft.com/downloads/): You need and IDE with Unity support for scripting. We recommend Visual Studio, but feel free to choose any other 
that meets the requirements.
- Unity Editor Version 2021.3.22f1: This is the version used for the project. If you choose another version complications cannot be ruled out.
- GitHub: For convenience install git on your local machine to easily interact with the repository. The command to use in the terminal varies depending on your machine, see also
[here](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

- If you want to build for iOS: You will need to install Xcode as well, which can be downloaded in the Appstore. Note however that in order to do so you need to work on a Mac.
Further you will need to install the iOS build support in Unity Hub.
- If you want to build for Android: You will need to install the 
[Android Studio](https://developer.android.com/studio?gclid=Cj0KCQjwu-KiBhCsARIsAPztUF3tI5ZMkR-qJYwDyOcMGLjgw4UNVCBeT1SYdJGsqf-ntpcNDqVp-GcaAolpEALw_wcB&gclsrc=aw.ds). 
Further you will need to install the Android build support in Unity Hub.

Once all these requirements are met you can clone the repository to your local machine and open the project in Unity Hub.

## Testing the system
There are two ways to test your changes. Number one, you push them back to GitHub. As soon as you do so a GitHub Action workflow will run all tests that are available in the 
project.
<br>
You can also run your tests locally. In the opened unity project open Window => General => Testrunner which will open the Testrunner. In this view you will see a 
hierarchy of tests. By double clicking on some element in this hierarchy all tests in test classes 
beneath it will be executed and either marked as red if failed or green if passed.

## Building the system
There are two potential ways to obtain the build for your target platform. Either you download the latest build from GitHub for your target platform. These builds 
are created by the GitHub Action [Build Project](https://github.com/oagenoagemono/CSE403-SP23-RegionAttack/actions/workflows/build.yml) and provide a 
builds for iOS, Android, and Windows. <br>
Or you can also make your own build in the opened unity project. To do so follow these steps
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
