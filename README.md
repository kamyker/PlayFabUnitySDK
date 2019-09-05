# UnitySDK README

## Fork with Package Manager and Assembly Definitions support

Read: [Original README](https://github.com/PlayFab/UnitySDK/blob/master/README.md)

Just go here: [EditorExtensions plugin](https://github.com/kamyker/PlayFabUnityEditorExtensions).

If you really don't need EditorExtensions sdk may (?) work without it:
   Open `<project_root>/Packages/manifest.json` and add
```
"com.playfab.editorextensions": "https://github.com/kamyker/PlayFabUnitySDK.git",
"com.playfab.shared": "https://github.com/kamyker/PlayFabUnityShared.git"
```
![project](https://raw.githubusercontent.com/kamyker/PlayFabUnityEditorExtensions/master/_repoAssets/img/EdEx_Project.png "Title")


This sdk is generated with https://github.com/kamyker/PlayFabSDKGenerator unity-package-manager.bat
