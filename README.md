# VR Scriptable Framework using Unity DOTS
This repository is a Virtual Reality Framework using Scriptable Objects, the Unity Input Manager for Inputs and Interaction, an Event System as seen in [Quill18 video](https://www.youtube.com/watch?v=04wXkgfd9V8) and the DOTS from Unity3D. It aims to ease the use of Virtual Reality in a project, and to have a light tool for that, while integrating a cross-platform project, some basic VR features and being as fast as possible. 


If you have any in question on how to use this tool, I'll be glad to answer you, just send me an email to arnaudbriche1994@gmail.com ! :)


## Description
The repository you're currently is a Crossfplatform, Lightweight VR Framework giving you access to some basic features often used in VR (UI, Teleportation, Flying mode, Gaze, Inputs Management, ...). It's an alternative to Libraries like VRTK, that was way too big for me when I first used it.


The supported devices for now are :
- The HTC Vive
- The HTC Focus (No 3D Models for the controllers are provided for now)
- The Microsoft Mixed Reality Headset
- The Oculus Rift with Touch Controllers
- The Oculus Quest
- The Oculus GO
- The Gear VR
- A VR Simulator (only recommended for debug)


# Releases
The stable versions are placed in the Releases section of this repository. Multiple packages are available, with extensions depending on your use. The only one you absolutely need is the VRSF_Hybrid_Core package.


# Requirements
For Unity, you need to download the latest **2019.1 version or later**, as it's required to be able to use Unity DOTS.

## Libraries and Packages
To use this Framework, you gonna need the following stuffs :
- **The Entities Package** : You can find it in the Package Manager from Unity (in Unity, Tab Window > Package Manager, in the Packages Window click on Advanced > Show Preview Packages, and then : All Packages > Entities > Install).
- **VR Support** : In the Player Settings Window (Edit > Project Settings > Player), go to the last tab called XR Settings, set the Virtual Reality Supported toggle to true, and add the Oculus, OpenVR and None SDKs to the list.
- **Scripting Runtime Version** : This one is normally set by default in the last versions of Unity, but we never know :  still in the Player Settings Window, go to the Other Settings tab and set the Scripting Runtime version to .NET 4.x Equivalent.

## Optional Libraries
You still need to import some VR Packages, depending on your needs, to use this framework. Those are found in the Package Manager from Unity :
- **Oculus for Desktop** : If you want to use the Rift or Rift S Support
- **Oculus for Desktop** : If you want to use the Oculus Go, Gear VR or Oculus Quest Support
- **Windows Mixed Reality** : If you want to use the WMR Headset
- **OpenVR for Desktop** : If you want to use the HTC Vive or HTC Focus (**WARNING :** MODELS FOR FOCUS CONTROLLERS NOT PROVIDED)

### Oculus GO, Oculus Quest, Gear VR and HTC Focus Specifities
If you need to build for a mobile platform, you need as well to download the Android Building support (File > Build Settings > Android) and to switch the platform to Android.



Once all of that is done, **Restart your project so everything can be recompiled !**



## Basic Setup :

1. Import the different packages listed above
2. Relaunch the Editor to be sure that everything is correctly recompiled
3. Import the VRSF_DOTS_Core package
4. Import the other extension packages you need
5. Add SetupVR in your scene (Right click in Scene View > VRSF > Add SetupVR in Scene)
6. Go to Edit > ProjectSettings > Input and use the Preset button on the top right corner to set the Inputs to the preset included in the Core Package from VRSF
7. Go to Edit > Player > Project Settings > XR Settings and tick the Virtual Reality Supported checkbox
8. Add, in this order, the Oculus SDK, OpenVR SDK, and None (For the Simulator)
9. Set the Start Position of your CameraRig using the CameraRig object
10. You should be good to go !

If you want to add anything more in your scene (Movements, UI, Gaze, ...), just check the prefabs in the different Extension Packages, or check the different scenes in the VRSF.Samples folder of this repository :)


## Credits
This repository is based on multiple repositories found online, and that's why I would like to thanks the following persons for their work that helped me through the development of this project :
- The work of [Thorsten Jänichen](https://github.com/TJaenichen) and [Thomas Masquart](https://github.com/ThmsMsqrt), co-author of the Scriptable Framework used in this repository. Their work is as well based on the excellent Unite Talk 2017 from Ryan Hipple, [available here](https://youtu.be/raQ3iHhE_Kk), and on its [Scriptable Objects Github repository](https://github.com/roboryantron/Unite2017).
- The EventCallbacks Plugin from [Quill18](https://www.youtube.com/watch?v=04wXkgfd9V8) and the rewriting of it by [CrazyFrog55](https://github.com/crazyfox55) and [FuzzyHobo](https://github.com/FuzzyHobo). I made my own version available [here](https://github.com/Jamy4000/UnityCallbackAndEventTutorial).
- The Vive-Teleporter offered by [FlaFla2](https://github.com/Flafla2/Vive-Teleporter) for the calculation and display of the Parabole in the Curve Teleporter.


## Documentation
For more info about this VR framework, please send me a message, as the Wiki is still a work in progress.

For more info about the Scriptable Objects and the Framework created, please check the Github Repository given above as well as the Unite talk and Example project provided by Unity and Ryan Hipple.

For more info about the Event System we are using, please check the Github Repository and video given above as well as the example project I've created on my Github page.
