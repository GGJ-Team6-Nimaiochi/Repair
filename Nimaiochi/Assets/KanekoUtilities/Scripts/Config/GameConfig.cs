using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum ConfigValueName { UseVibration = 0, AudioEnable = 1 }

    public class GameConfig : Singleton<GameConfig>
    {
        public GameConfig()
        {

        }

        public void OnChangeValue(ConfigValueName name, bool value)
        {
            switch (name)
            {
                case ConfigValueName.UseVibration:
                    SetVibration(value);
                    break;
                case ConfigValueName.AudioEnable:
                    SetAudioEnable(value);
                    break;
            }
        }

        public bool GetValue(ConfigValueName name)
        {
            switch (name)
            {
                case ConfigValueName.UseVibration:
                    return MyVibrationManager.Instance.Enable;
                case ConfigValueName.AudioEnable:
                    return AudioManager.Instance.Enable;
            }

            return false;
        }

        void SetVibration(bool value)
        {
            if (value)
            {
                MyVibrationManager.Instance.Vibrate(VibrationType.ImpactMedium);
            }
            MyVibrationManager.Instance.SetEnable(value);
        }

        void SetAudioEnable(bool value)
        {
            AudioManager.Instance.SetEnable(value);
        }
    }
}