UnityPlayerPrefsPanel
=====================

This is a Unity 3D editor extension for checking and setting KeyName values in PlayerPrefs, confirming the existence of a KeyName, as well as resetting all PlayerPrefs.  Currently, you an only set the value of KeyNames that already exist. In order to use this extension, place it inside a folder entitled Editor, inside any Unity project.

This editor extension has been tested with Unity 3D v4.3. I do not guarantee that it will work in earlier or later versions of Unity 3D. This code is released under the MIT License (see LICENSE file).

One thing to note, when checking for a KeyName's KeyValue the KeyName is checked for all possible data types (Int, Float, String). The result is displayed in the Console. NOTE: If the Int and Float values are 0, and the String is empty, it is not possible to know the data type of the KeyName's KeyValue without looking at the Preference file. Think Schrödinger's cat...
