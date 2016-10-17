using System.IO.IsolatedStorage;
using System;

namespace MyAppSettings.Models
{
public class Settings
{
    IsolatedStorageSettings settings;

    // The isolated storage key names of the app settings
    const string UserNameSettingKeyName = "UserNameSetting";
    const string PlaySoundSettingKeyName = "PlaySoundSetting";
        
    const string UserNameSettingDefault = "user";
    const bool PlaySoundSettingDefault = true;
        
    public Settings()
    {
        // Get the settings for the application from isolated storage
        settings = IsolatedStorageSettings.ApplicationSettings;
    }
                
    /// <summary>
    /// Save the settings.
    /// </summary>
    public void Save()
    {
        settings.Save();
    }

    public string UserNameSetting
    {
        get
        {
            return GetValueOrDefault<string>(UserNameSettingKeyName, UserNameSettingDefault);
        }
        set
        {
            if (SetValue(UserNameSettingKeyName, value))
            {
                Save();
            }
        }
    }

    public bool PlaySoundSetting
    {
        get
        {
            return GetValueOrDefault<bool>(PlaySoundSettingKeyName, PlaySoundSettingDefault);
        }
        set
        {
            if (SetValue(PlaySoundSettingKeyName, value))
            {
                Save();
            }
        }
    }

    /// <summary>
    /// Update a setting value for our application. If the setting does not
    /// exist, then add the setting.
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool SetValue(string Key, Object value)
    {
        bool valueChanged = false;

        // If the key exists
        if (settings.Contains(Key))
        {
            // If the value has changed
            if (settings[Key] != value)
            {
                // Store the new value
                settings[Key] = value;
                valueChanged = true;
            }
        }
        // Otherwise create the key.
        else
        {
            settings.Add(Key, value);
            valueChanged = true;
        }
        return valueChanged;
    }

    /// <summary>
    /// Get the current value of the setting, or if it is not found, set the 
    /// setting to the default setting.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public T GetValueOrDefault<T>(string Key, T defaultValue)
    {
        T value;

        // If the key exists, retrieve the value.
        if (settings.Contains(Key))
        {
            value = (T)settings[Key];
        }
        // Otherwise, use the default value.
        else
        {
            value = defaultValue;
        }
        return value;
    }


}
}
