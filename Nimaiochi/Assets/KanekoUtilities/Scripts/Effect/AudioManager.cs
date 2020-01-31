using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class AudioManager : SingletonMonobehaviour<AudioManager>
    {
        [SerializeField]
        BackgroundMusic backgroundMusic = null;

        [SerializeField]
        SoundEffectPool soundEffectPool = null;

        [SerializeField]
        string[] preloadAudioClipNames = null;

        const int maxSECount = 50;

        MasterVolumeSettings masterVolumeSettings;
        public bool Enable { get { return !masterVolumeSettings.IsMuteMasterVolume; } }
        public float MasterVolume { get { return masterVolumeSettings.MasterVolume; } }
        public float MasterBGMVolume { get { return masterVolumeSettings.MasterBGMVolume; } }
        public float MasterSEVolume { get { return masterVolumeSettings.MasterSEVolume; } }
        
        public bool IsPlayingBGM { get { return backgroundMusic.IsPlaying; } }
        
        LinkedList<SoundEffect> activeSoundEffectList = new LinkedList<SoundEffect>();
        Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();
        
        protected override void Awake()
        {
            base.Awake();

            masterVolumeSettings = new MasterVolumeSettings();
            foreach(var p in preloadAudioClipNames) GetAudioClip(p);
        }

        protected override void OnDestroy()
        {
            backgroundMusic.Stop();
            base.OnDestroy();
        }

        public void SetEnable(bool enable)
        {
            masterVolumeSettings.IsMuteMasterVolume = !enable;

            backgroundMusic.UpdateVolume();
        }
        public void SetMasterVolume(float volume)
        {
            masterVolumeSettings.MasterVolume = volume;
            backgroundMusic.UpdateVolume();
        }
        public void SetMasterBGMVolume(float volume)
        {
            masterVolumeSettings.MasterBGMVolume = volume;
            backgroundMusic.UpdateVolume();
        }
        public void SetMasterSEVolume(float volume)
        {
            masterVolumeSettings.MasterSEVolume = volume;
        }
        public void SetMasterVolumeMute   (bool isMute)
        {
            masterVolumeSettings.SetMute(AudioVolumeSettingType.Master,isMute);
            backgroundMusic.UpdateVolume();
        }
        public void SetMasterBGMVolumeMute(bool isMute)
        {
            masterVolumeSettings.SetMute(AudioVolumeSettingType.MasterBGM, isMute);
            backgroundMusic.UpdateVolume();
        }
        public void SetMasterSEVolumeMute (bool isMute)
        {
            masterVolumeSettings.SetMute(AudioVolumeSettingType.MasterSE, isMute);
        }

        public void PlayBGM(string bgmName, float volume = 1.0f, float pitch = 1.0f)
        {
            AudioClip clip = GetAudioClip(bgmName);

            if (clip == null) return;

            backgroundMusic.Play(clip, volume, pitch);
        }

        public void ReplayBGM(float volume = 1.0f, float pitch = 1.0f)
        {
            if (!backgroundMusic.IsPlaying) return;

            backgroundMusic.Replay(volume, pitch);
        }

        public void PauseBGM()
        {
            backgroundMusic.Pause();
        }

        public void UnPauseBGM()
        {
            backgroundMusic.UnPause();
        }

        public void StopBGM()
        {
            backgroundMusic.Stop();
        }

        public void PlayOneShot(string name, float volume = 1.0f, float pitch = 1.0f)
        {
            AudioClip clip = GetAudioClip(name);

            if (clip == null) return;

            volume = volume * MasterSEVolume;
            SoundEffect effect;
            if (activeSoundEffectList.Count > maxSECount)
            {
                effect = activeSoundEffectList.First.Value;
                soundEffectPool.ReturnInstance(effect);
                activeSoundEffectList.Remove(effect);
            }

            effect = soundEffectPool.GetInstance();
            activeSoundEffectList.AddLast(effect);
            effect.Play(clip, () =>
            {
                soundEffectPool.ReturnInstance(effect);
                activeSoundEffectList.Remove(effect);
            }, volume, pitch);
        }

        public void PlayOneShot(string name, Vector3 position, float volume = 1.0f)
        {
            AudioClip clip = GetAudioClip(name);

            if (clip == null) return;

            volume = volume * MasterSEVolume;
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }

        AudioClip GetAudioClip(string name)
        {
            AudioClip clip;

            if (clipDictionary.TryGetValue(name, out clip)) return clip;

            clip = MyAssetStore.Instance.GetAsset<AudioClip>(name, "Audios/");

            if (clip == null) return null;

            clipDictionary.Add(name, clip);
            return clip;
        }
    }
}