# UnitySDK README

## Fork with Package Manager and Assembly Definitions support

Read: [Original README](https://github.com/PlayFab/UnitySDK/blob/master/README.md)

The easist way to get started is to use our [EditorExtensions plugin](https://github.com/kamyker/PlayFabUnityEditorExtensions). This plugin provides a clean UI for configuring the SDK as well as automatically downloading, installing and upgrading the PlayFab SDK.

## Setup:
  
  1. Open `<project_root>/Packages/manifest.json` and add
```
"com.playfab.editorextensions": "https://github.com/kamyker/PlayFabUnitySDK.git",
"com.playfab.shared": "https://github.com/kamyker/PlayFabUnityShared.git"
```
to `dependencies` 

  2. Follow [Original README](https://github.com/PlayFab/UnitySDK/blob/master/README.md)

This is how project looks like with this fork:

![project](https://raw.githubusercontent.com/kamyker/PlayFabUnityEditorExtensions/master/_repoAssets/img/EdEx_Project.png "Title")
