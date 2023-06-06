# ITR's I2 Translator Mod

## Overview
The ITR's I2 Translator Mod provides an easy way for fans to create translations for their favorite games.
It (should) work for all Unity games that use [I2 Localization](http://www.inter-illusion.com/tools/i2-localization).
It supports both MelonLoader and BepInEx

## Installation and Setup
To install and set up the mod, follow these steps:
1. Copy the latest release's DLL file into your plugin or mod folder, depending on whether you're using MelonLoader or BepInEx.
2. When you run the game for the first time, the `%AppData%/../LocalLow/[CompanyName]/[GameName]/ItrsLocalization` folder will open. This folder contains all the current localization terms.

#### Using a community translation
3. Your community should have provided csv files to replace the ones in the aforementioned folder with.
   - If they come in a zip file, you must first unzip them.
4. In game, make sure the selected langauge is the same as the one replaced by the csv files
   - This is usually English

#### Creating your own translations
Step 3 and for can be done automatically with: `python clean_localization [folder] [language name]`
3. Delete all rows except "Key" and the language you wish to replace in the localization document.
4. Copy the language column and rename the copy to 'Reference'
5. Modify the unmodified column. If you load the game you should see your changes
6. When sharing to your community you can delete the 'Reference' column, but it's good to keep a copy of the version that has a reference for easier translation

## Dependencies and Requirements
To set up the project for development, follow these steps:
1. Create a "Dependencies" folder.
2. Place the following dependencies in the "Dependencies" folder:
   - I2Localization
   - Il2CppI2Localization
   - Il2CppInterop.Runtime
   - Il2Cppmscorlib
   - UnityEngine.CoreModule
   - I2Localization (Only needed for BepInEx)
   - Il2CppI2Localization (Only needed for MelonLoader) 
   - MelonLoader (Only needed for MelonLoader)
3. Select either BepInEx or MelonLoader as target
