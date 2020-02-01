using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnvironmentSetting : ScriptableObject
{
    public bool IsAvailable = false;

    //light
    public Color AmbientColor = Color.gray;
    [Range(0.0f, 10.0f)]
    public float AmbientIntensity = 1.0f;

    //fog
    [HideInInspector]
    public bool FogEnable = false;
    [HideInInspector]
    public Color FogColor = Color.gray;
    [HideInInspector]
    public FogMode Mode = FogMode.Linear;
    
    [HideInInspector]
    public float Density = 0.01f;
    [HideInInspector]
    public float Start = 0.0f;
    [HideInInspector]
    public float End = 300.0f;
    
    public virtual void Apply()
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientSkyColor = AmbientColor;
        RenderSettings.ambientIntensity = AmbientIntensity;

        RenderSettings.fog = FogEnable;
        
        if (!FogEnable) return;

        RenderSettings.fogMode = Mode;
        RenderSettings.fogColor = FogColor;

        if (Mode == FogMode.Linear)
        {
            RenderSettings.fogStartDistance = Start;
            RenderSettings.fogEndDistance = End;
        }
        else
        {
            RenderSettings.fogDensity = Density;
        }
    }
    
    void OnValidate()
    {
        if (!IsAvailable) return;
        Apply();
    }
    
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnvironmentSetting))]
public class EnvironmentSettingDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EnvironmentSetting settings = target as EnvironmentSetting;

        bool isChanged = false;
        settings.FogEnable = DrawBoolValue("Fog", settings.FogEnable, ref isChanged);
        
        if (settings.FogEnable)
        {
            settings.FogColor = DrawColorValue("FogColor", settings.FogColor, ref isChanged);
            FogMode oldMode = settings.Mode;
            settings.Mode = (FogMode)EditorGUILayout.EnumPopup("Mode", settings.Mode);
            if (oldMode != settings.Mode) isChanged = true;

            if (settings.Mode == FogMode.Linear)
            {
                settings.Start = DrawFloatValue("Start", settings.Start, ref isChanged);
                settings.End = DrawFloatValue("End", settings.End, ref isChanged);
            }
            else
            {
                settings.Density = DrawSlider("Density", settings.Density, 0.0f, 1.0f, ref isChanged);
            }
        }

        if (isChanged)
        {
            if (!settings.IsAvailable) return;
            settings.Apply();
        }
        
        EditorUtility.SetDirty(settings);
    }

    float DrawFloatValue(string label,float value, ref bool isChanged)
    {
        var old = value;
        value = EditorGUILayout.FloatField(label, value);
        if(!isChanged) isChanged = old != value;
        return value;
    }
    float DrawSlider(string label, float value,float min, float max, ref bool isChanged)
    {
        var old = value;
        value = EditorGUILayout.Slider(label, value, min, max);
        if (!isChanged) isChanged = old != value;
        return value;
    }
    bool DrawBoolValue(string label, bool value, ref bool isChanged)
    {
        var old = value;
        value = EditorGUILayout.Toggle(label, value);
        if (!isChanged) isChanged = old != value;
        return value;
    }
    Color DrawColorValue(string label, Color value, ref bool isChanged)
    {
        var old = value;
        value = EditorGUILayout.ColorField(label, value);
        if (!isChanged) isChanged = old != value;
        return value;
    }
}
#endif
