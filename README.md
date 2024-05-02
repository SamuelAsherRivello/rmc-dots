[![npm package](https://img.shields.io/npm/v/com.rmc.rmc-dots)](https://www.npmjs.com/package/com.rmc.rmc-dots)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

# RMC DOTS Library 

This repo contains the RMC DOTS Library. Its ready to be imported into **your** Unity Project

Or if you want a complete, empty Unity Project with all Unity ECS packages already added and the RMC DOTS Library package already added, see [https://github.com/SamuelAsherRivello/rmc-dots-examples/](https://github.com/SamuelAsherRivello/rmc-dots-examples/). This is the **fastest** way to create a new Unity Dots Project.

## Setup Instructions

### Unity Project 

1. Download the latest LTS version of Unity
1. Open the Unity Hub
1. Create a new Unity Project. Any template with URP is acceptable
1. Open the Unity Project
1. Open the Unity Package Manager Window. NOTE: Some of the following may already be imported. If so, leave it as-is.
1. Add **Unity** Packages...
    1. Add Package for "com.unity.inputsystem"
    1. Add Package for "com.unity.ugui"
    1. Add Package for "com.unity.textmeshpro"
1. Add **Unity** Packages for ECS...
    1. Add Package for "com.unity.render-pipelines.universal"
    1. Add Package for "com.unity.entities.graphics"
    1. Add Package for "com.unity.physics"
1. Add **RMC** Packages...
    1. Add Package By Git for "https://github.com/SamuelAsherRivello/rmc-readme.git"
    1. Add Package By Git for "https://github.com/SamuelAsherRivello/rmc-dots.git"

### (Optional) RMC DOTS Library Examples
1. Follow the steps above
1. To import the RMC Dots Library examples, see [ReadMe](./Unity/Assets/ReadMe.txt)

<img src="https://media.githubusercontent.com/media/SamuelAsherRivello/rmc-dots/486cbed228c68b64493305ee12d7faa39a2fbc98/RMC%20DOTS/Documentation/Images/rmc-dots-examples-screenshot.jpg" width = "400px" />


Created By
=============

- Samuel Asher Rivello 
- Over 25 years XP with game development (2024)
- Over 11 years XP with Unity (2024)

Contact
=============

- Twitter - <a href="https://twitter.com/srivello/">@srivello</a>
- Resume & Portfolio - <a href="http://www.SamuelAsherRivello.com">SamuelAsherRivello.com</a>
- Git - <a href="https://github.com/SamuelAsherRivello/">Github.com/SamuelAsherRivello</a>
- LinkedIn - <a href="https://Linkedin.com/in/SamuelAsherRivello">Linkedin.com/in/SamuelAsherRivello</a> <--- Say Hello! :)








<BR>
<BR>
<BR>

> [!WARNING]  
> Due to an issue with NPM.js, currently you must install the package below with the [Or Via Git URL](#or-via-git-url) technique.

<BR>
<BR>
<BR>

<img width = "400" src="https://raw.githubusercontent.com/SamuelAsherRivello/rmc-core/main/RMC%20Core/Documentation~/com.rmc_namespace_logo.png" />

# RMC DOTS

- [How To Use](#how-to-use)
- [Install](#install)
  - [Via NPM](#via-npm)
  - [Or Via Git URL](#or-via-git-url)
- [Optional](#optional)
  - [Tests](#tests)
  - [Samples](#samples)
- [Configuration](#configuration)

<!-- toc -->

## How to use

This is the **DOTS** library for Unity Development by Rivello Multimedia Consulting.

It includes functionality for audio, custom data types, reusable Unity UI elements, visual transitions, and more.

Import the package into your new or existing Unity Project. Enjoy!

## Install

You can either install [Via NPM](#via-npm) or [Via Git URL](#or-via-git-url). The result will be the same.

### Via NPM

You can either use the Unity Package Manager Window (UPM) or directly edit the manifest file. The result will be the same.

**UPM**

To use the [Package Manager Window](https://docs.unity3d.com/Manual/upm-ui.html), first add a [Scoped Registry](https://docs.unity3d.com/2023.1/Documentation/Manual/upm-scoped.html), then click on the interface menu ( `Status Bar → (+) Icon → Add Package By Name ...` ).

**Manifest File**

Or to edit the `Packages/manifest.json` directly with your favorite text editor, add a scoped registry then the following line(s) to dependencies block:

```json
{
  "scopedRegistries": [
    {
      "name": "npmjs",
      "url": "https://registry.npmjs.org/",
      "scopes": [
        "com.rmc"
      ]
    }
  ],
  "dependencies": {
    "com.rmc.rmc-dots": "1.4.3" //Use the latest "version" in the https://github.com/SamuelAsherRivello/rmc-dots/blob/main/package.json
  }
}
```
Package should now appear in package manager.


### Or Via Git URL

You can either use the Unity Package Manager (UPM) Window or directly edit the manifest file. The result will be the same.

**UPM**

To use the [Package Manager Window](https://docs.unity3d.com/Manual/upm-ui.html) click on the interface menu ( `Status Bar → (+) Icon → Add Package From Git Url ...` ).

**Manifest File**

Or to edit the `Packages/manifest.json` directly with your favorite text editor, add following line(s) to the dependencies block:
```json
{
  "dependencies": {
      "com.rmc.rmc-dots": "https://github.com/SamuelAsherRivello/rmc-dots.git"
  }
}
```

## Optional

### Tests

The package can optionally be set as *testable*.
In practice this means that tests in the package will be visible in the [Unity Test Runner](https://docs.unity3d.com/2017.4/Documentation/Manual/testing-editortestsrunner.html).

Open `Packages/manifest.json` with your favorite text editor. Add following line **after** the dependencies block:
```json
{
  "dependencies": {
  },
  "testables": [ "com.rmc.rmc-dots" ]
}
```

### Samples

Some packages include optional samples with clear use cases. To import and run the samples:

1. Open Unity 
1. Complete the package installation (See above)
1. Open the [Package Manager Window](https://docs.unity3d.com/Manual/upm-ui.html)
1. Select this package 
1. Select samples
1. Import

## Configuration

* `Unity Target` - [Standalone MAC/PC](https://support.unity.com/hc/en-us/articles/206336795-What-platforms-are-supported-by-Unity-)
* `Unity Version` - Any [Unity Editor](https://unity.com/download) 2021.x or higher
* `Unity Rendering` - Any [Unity Render Pipeline](https://docs.unity3d.com/Manual/universal-render-pipeline.html)
* `Unity Aspect Ratio` - Any [Unity Game View](https://docs.unity3d.com/Manual/GameView.html)


Created By
=============

- Samuel Asher Rivello 
- Over 23 years XP with game development (2023)
- Over 10 years XP with Unity (2023)

Contact
=============

- Twitter - <a href="https://twitter.com/srivello/">@srivello</a>
- Resume & Portfolio - <a href="http://www.SamuelAsherRivello.com">SamuelAsherRivello.com</a>
- Source Code on Git - <a href="https://github.com/SamuelAsherRivello/">Github.com/SamuelAsherRivello</a>
- LinkedIn - <a href="https://Linkedin.com/in/SamuelAsherRivello">Linkedin.com/in/SamuelAsherRivello</a> <--- Say Hello! :)

License
=============

Provided as-is under MIT License | Copyright © 2024 Rivello Multimedia Consulting, LLC




