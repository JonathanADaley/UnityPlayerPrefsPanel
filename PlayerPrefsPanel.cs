/// <summary>
/// Player prefs panel
/// 
/// Copyright (c) 2014 Jonathan A Daley (Twitter @JonathanADaley)
/// 
///	Permission is hereby granted, free of charge, to any person obtaining a copy
///	of this software and associated documentation files (the "Software"), to deal
///	in the Software without restriction, including without limitation the rights
///	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
///	copies of the Software, and to permit persons to whom the Software is
///	furnished to do so, subject to the following conditions:
///	
///	The above copyright notice and this permission notice shall be included in
///	all copies or substantial portions of the Software.
///	
///	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
///	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
///	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
///	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
///	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
///	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
///	THE SOFTWARE.
/// </summary>

using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerPrefsPanel : EditorWindow
{
	#region Private Fields
	private string KeyNameName = "";
	private string KeyNameValue = "";
	private float halfWidthValue = 111.0f;
	private float fullWidthValue = 222.0f;
	private Vector2 scrollPosition;
	#endregion

	#region Get Extension to show up in Window menu code
	[MenuItem ("Window/PlayerPrefs Panel")]
	#endregion

	#region Code to create instance of Panel in Editor
	static void Init()
	{
		EditorWindow.GetWindow(typeof(PlayerPrefsPanel));
	}
	#endregion

	#region GUI code
	void OnGUI()
	{
//		Title
		GUILayout.Label("PlayerPrefs Panel beta, v0.01\nCheck & Set KeyName values; confirm KeyName existance,\n& reset PlayerPrefs.", EditorStyles.miniLabel);

//		Starting Scroll View
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(315), GUILayout.Height(300));

//		Checking KeyValues and KeyName Existence
		GUILayout.Label("Checking KeyValues and KeyName Existence", EditorStyles.boldLabel);
		GUILayout.Label("Enter KeyName:", EditorStyles.label);
		KeyNameName = EditorGUILayout.TextField("",KeyNameName,GUILayout.MaxWidth(fullWidthValue));
		if (GUILayout.Button("Check KeyValue",GUILayout.MaxWidth(halfWidthValue)))
		{
			CheckKeyValue(KeyNameName);
		}
		GUILayout.Label("The entered KeyName's KeyValue is checked for all possible\n" +
			"data types (Int, Float, String). The result is displayed in the\n" +
			"Console.\n\n" +
			"NOTE: If the Int and Float values are 0, and the String is empty,\n" +
			"it is not possible to know the data type of the KeyName's\n" +
			"KeyValue without looking at the Preference file.\n" +
			"Think Schrödinger's cat...", EditorStyles.miniLabel);

//		Setting KeyValues
		GUILayout.Label("Setting KeyName Values", EditorStyles.boldLabel);
		GUILayout.Label("Enter KeyValue:", EditorStyles.label);
		KeyNameValue = EditorGUILayout.TextField("",KeyNameValue,GUILayout.MaxWidth(fullWidthValue));
		if (GUILayout.Button("Set Int",GUILayout.MaxWidth(halfWidthValue)))
		{
			SetIntKey(KeyNameName,KeyNameValue);
		}
		if (GUILayout.Button("Set Float",GUILayout.MaxWidth(halfWidthValue)))
		{
			SetFloatKey(KeyNameName,KeyNameValue);
		}
		if (GUILayout.Button("Set String",GUILayout.MaxWidth(halfWidthValue)))
		{
			SetStringKey(KeyNameName,KeyNameValue);
		}
		GUILayout.Label("When clicked, each button above will attempt to set the\n" +
			"entered KeyName to the entered KeyValue in the data format\n" +
			"labeled on the button, if it is possible to do so.\n\n" +
			"Please note, for safety reasons you can ONLY set a\n" +
			"KeyName that has already been created within your\n" +
			"project's scripting environment.", EditorStyles.miniLabel);
		GUILayout.Label("Setting an existing KeyName with a new KeyValue DELETES\n" +
			"the previously set KeyValue.\n" +
			"This operation CANNOT be undone.", EditorStyles.miniLabel);

//		Resetting PlayerPrefs
		GUILayout.Label ("Reseting PlayerPrefs", EditorStyles.boldLabel);
		if (GUILayout.Button("Reset PlayerPrefs",GUILayout.MaxWidth(fullWidthValue)))
			ResetPlayerPrefs();
		GUILayout.Box ("Resetting PlayerPrefs will delete ALL data\nin PlayerPrefs.\nThis *CANNOT* be undone!");

////		Open Preference File is OS X
//		GUILayout.Label("Open Preferences File", EditorStyles.boldLabel);
//		GUILayout.Label("This currently ONLY works with the OS X version of Unity.", EditorStyles.miniLabel);

		GUILayout.EndScrollView();
	}
	#endregion

	#region Private Methods

	#region Checking for Keys
	private void CheckKeyValue(string aKeyNameName)
	{
		if (aKeyNameName == "")
		{
			Debug.LogError("Please enter a valid KeyName.\n\n");
		}
		else if (PlayerPrefs.HasKey(aKeyNameName))
		{
			int checkedIntValue = PlayerPrefs.GetInt(aKeyNameName);
			float checkedFloatValue = PlayerPrefs.GetFloat(aKeyNameName);
			string checkedStringValue = PlayerPrefs.GetString(aKeyNameName);

			if (checkedIntValue == 0 && checkedFloatValue == 0 && checkedStringValue == "")
			{
				Debug.LogWarning("KeyName '" + aKeyNameName + "' is either an Int or a Float\nwith a value 0, or it is an empty String.");
			}
			else if (checkedIntValue != 0 && checkedFloatValue == 0 && checkedStringValue == "")
			{
				// Int value
				Debug.Log("KeyName '" + aKeyNameName + "' has an Int value of: " + PlayerPrefs.GetInt(aKeyNameName).ToString() + "\n\n");
			}
			else if (checkedIntValue == 0 && checkedFloatValue != 0 && checkedStringValue == "")
			{
				// Float value
				Debug.Log("KeyName '" + aKeyNameName + "' has a Float value of: " + PlayerPrefs.GetFloat(aKeyNameName).ToString() + "\n\n");
			}
			else if (checkedIntValue == 0 && checkedFloatValue == 0 && checkedStringValue != "")
			{
				Debug.Log("KeyName '" + aKeyNameName + "' has a String value of: " + PlayerPrefs.GetString(aKeyNameName) + "\n\n");
			}
		}
		else
		{
			Debug.LogError("KeyName '" + aKeyNameName + "' does not exist.\nPlease check KeyName and try again.\n\n");
		}
	}
	#endregion

	#region Setting KeyValues
	private void SetIntKey(string aKeyNameName, string aKeyNameValue)
	{
		if (aKeyNameName == "")
		{
			Debug.LogError("Please enter a valid KeyName.\n");
		}
		else if (PlayerPrefs.HasKey(aKeyNameName))
		{
			int newValueInt;
			try
			{
				newValueInt = int.Parse(aKeyNameValue);

				Debug.LogWarning("KeyName already in use, updated to new KeyValue.\n" +
				                 "Previous KeyValue(s) below:\n\n");
				CheckKeyValue(aKeyNameName);
				PlayerPrefs.DeleteKey(aKeyNameName);
				PlayerPrefs.SetInt(aKeyNameName,newValueInt);
				Debug.LogWarning("KeyName '" + aKeyNameName + "' updated\nto KeyValue (as an Int): " + aKeyNameValue +"\n\n");
			}
			catch
			{
				Debug.LogError("Cannot convert new KeyValue to Float.\nPlease check KeyValue and try again.\n\n");
			}
		}
		else
		{
			Debug.LogError("KeyName does not exist. You may only set pre-existing\nKeyNames to new values. Please check KeyName and try again.\n\n");
		}
	}

	private void SetFloatKey(string aKeyNameName, string aKeyNameValue)
	{
		if (aKeyNameName == "")
		{
			Debug.LogError("Please enter a valid KeyName.\n\n");
		}
		else if (PlayerPrefs.HasKey(aKeyNameName))
		{
			double newValueDouble;
			float newValueFloat;
			try
			{
				newValueDouble = double.Parse(aKeyNameValue); // initially parse to a double in order to avoid rounding issues

				newValueFloat = (float)newValueDouble;
				Debug.LogWarning("KeyName already in use, updated to new KeyValue.\n" +
				                 "Previous KeyValue(s) below, followed by new KeyValue.\n\n");
				CheckKeyValue(aKeyNameName);
				PlayerPrefs.DeleteKey(aKeyNameName);
				PlayerPrefs.SetFloat(aKeyNameName,newValueFloat);
				Debug.LogWarning("KeyName '" + aKeyNameName + "' updated\nto KeyValue (as a Float): " + aKeyNameValue +"\n\n");
			}
			catch
			{
				Debug.LogError("Cannot convert new KeyValue to Int.\nPlease check KeyValue and try again.\n\n");
			}
		}
		else
		{
			Debug.LogError("KeyName does not exist. You may only set pre-existing\nKeyNames to new values. Please check KeyName and try again.\n\n");
		}
	}

	private void SetStringKey(string aKeyNameName, string aKeyNameValue)
	{
		if (aKeyNameName == "")
		{
			Debug.LogError("Please enter a valid KeyName.\n\n");
		}
		else if (PlayerPrefs.HasKey(aKeyNameName))
		{
			Debug.LogWarning("KeyName already in use, updated to new KeyValue.\n" +
			               "Previous KeyValue(s) below, followed by new KeyValue.\n\n");
			CheckKeyValue(aKeyNameName);
			PlayerPrefs.DeleteKey(aKeyNameName);
			PlayerPrefs.SetString(aKeyNameName,aKeyNameValue);
			Debug.LogWarning("KeyName '" + aKeyNameName + "' updated\nto KeyValue (as String): " + aKeyNameValue +"\n\n");
		}
		else
		{
			Debug.LogError("KeyName does not exist. You may only set pre-existing\nKeyNames to new values. Please check KeyName and try again.\n\n");
		}
	}
	#endregion

	private void ResetPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
		Debug.LogWarning("PlayerPrefs has been reset.\nALL PlayerPrefs data has been deleted.\n\n");
	}
	#endregion
}
