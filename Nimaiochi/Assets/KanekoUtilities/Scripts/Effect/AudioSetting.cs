using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum AudioVolumeSettingType { Master, MasterBGM, MasterSE }
    public class AudioVolumeSetting
    {
        public AudioVolumeSettingType Type { get; private set; }
        RegisterFloatParameter volume;
        RegisterBoolParameter mute;

        public float Volume
        {
            get
            {
                return volume.GetValue() * (IsMute ? 0.0f : 1.0f);
            }
            set
            {
                volume.SetValue(value);
            }
        }
        public bool IsMute
        {
            get
            {
                return mute.GetValue();
            }
            set
            {
                mute.SetValue(value);
            }
        }

        public AudioVolumeSetting(AudioVolumeSettingType type, float defaultValue = 1.0f)
        {
            Type = type;
            volume = new RegisterFloatParameter(type.ToString() + "Volume", defaultValue);
            mute = new RegisterBoolParameter(type.ToString() + "Mute", false);
        }
    }

    public class MasterVolumeSettings
    {
        AudioVolumeSetting masterVolumeSetting;
        AudioVolumeSetting masterBGMVolumeSetting;
        AudioVolumeSetting masterSEVolumeSetting;

        public float MasterVolume
        {
            get
            {
                return masterVolumeSetting.Volume;
            }
            set
            {
                masterVolumeSetting.Volume = value;
            }
        }
        public float MasterBGMVolume
        {
            get
            {
                return masterBGMVolumeSetting.Volume * masterVolumeSetting.Volume;
            }
            set
            {
                masterBGMVolumeSetting.Volume = value;
            }
        }
        public float MasterSEVolume
        {
            get
            {
                return masterSEVolumeSetting.Volume * masterVolumeSetting.Volume;
            }
            set
            {
                masterSEVolumeSetting.Volume = value;
            }
        }

        public bool IsMuteMasterVolume
        {
            get
            {
                return masterVolumeSetting.IsMute;
            }
            set
            {
                masterVolumeSetting.IsMute = value;
            }
        }
        public bool IsMuteMasterBGMVolume
        {
            get
            {
                return masterBGMVolumeSetting.IsMute;
            }
            set
            {
                masterBGMVolumeSetting.IsMute = value;
            }
        }
        public bool IsMuteMasterSEVolume
        {
            get
            {
                return masterSEVolumeSetting.IsMute;
            }
            set
            {
                masterSEVolumeSetting.IsMute = value;
            }
        }

        public MasterVolumeSettings()
        {
            masterVolumeSetting = new AudioVolumeSetting(AudioVolumeSettingType.Master);
            masterBGMVolumeSetting = new AudioVolumeSetting(AudioVolumeSettingType.MasterBGM);
            masterSEVolumeSetting = new AudioVolumeSetting(AudioVolumeSettingType.MasterSE);
        }

        public float GetMasterVolume(AudioVolumeSettingType type)
        {
            switch (type)
            {
                case AudioVolumeSettingType.Master:
                    return MasterVolume;
                case AudioVolumeSettingType.MasterBGM:
                    return MasterBGMVolume;
                case AudioVolumeSettingType.MasterSE:
                    return MasterSEVolume;
            }

            return 0.0f;
        }
        public bool GetMuteMasterVolume(AudioVolumeSettingType type)
        {
            switch (type)
            {
                case AudioVolumeSettingType.Master:
                    return IsMuteMasterVolume;
                case AudioVolumeSettingType.MasterBGM:
                    return IsMuteMasterVolume;
                case AudioVolumeSettingType.MasterSE:
                    return IsMuteMasterVolume;
            }
            return false;
        }
        public void SetMasterVolume(AudioVolumeSettingType type, float value)
        {
            switch (type)
            {
                case AudioVolumeSettingType.Master:
                    masterVolumeSetting.Volume = value;
                    break;
                case AudioVolumeSettingType.MasterBGM:
                    masterBGMVolumeSetting.Volume = value;
                    break;
                case AudioVolumeSettingType.MasterSE:
                    masterSEVolumeSetting.Volume = value;
                    break;
            }
        }
        public void SetMute(AudioVolumeSettingType type, bool mute)
        {
            switch (type)
            {
                case AudioVolumeSettingType.Master:
                    masterVolumeSetting.IsMute = mute;
                    break;
                case AudioVolumeSettingType.MasterBGM:
                    masterBGMVolumeSetting.IsMute = mute;
                    break;
                case AudioVolumeSettingType.MasterSE:
                    masterSEVolumeSetting.IsMute = mute;
                    break;
            }
        }
    }
}
