using System;
using UnityEngine;

namespace KanekoUtilities
{
    public class BackgroundMusic : MyAudioSource
    {
        [SerializeField]
        float fadeInDuration = 1.0f;
        [SerializeField]
        float fadeOutDuration = 0.5f;
        float baseVolume;

        //Playする時の音量
        float RealVolume { get { return baseVolume * AudioManager.Instance.MasterBGMVolume; } }

        public void UpdateVolume()
        {
            Volume = RealVolume;
        }

        public override void Play(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
        {
            if (IsPlaying)
            {
                ChangeClip(clip, volume, pitch);
            }
            else
            {
                baseVolume = volume;
                Clip = clip;
                Pitch = pitch;
                FadeIn(fadeInDuration);
            }
        }

        public void Replay()
        {
            Replay(baseVolume, Pitch);
        }

        public void Replay(float volume = 1.0f, float pitch = 1.0f)
        {
            baseVolume = volume;
            FadeOut(fadeOutDuration, () =>
            {
                Source.Play();
                Pitch = pitch;
                FadeIn(fadeInDuration);
            });
        }

        void ChangeClip(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
        {
            if (clip == null || clip.Equals(Clip)) return;

            FadeOut(fadeOutDuration, () =>
            {
                Clip = clip;
                Pitch = pitch;
                baseVolume = volume;
                FadeIn(fadeInDuration);
            });
        }

        public override void Stop()
        {
            if (gameObject.activeInHierarchy)
            {
                FadeOut(fadeOutDuration, () =>
                {
                    Source.Stop();
                });
            }
            else
            {
                Source.Stop();
            }
        }

        public override void Pause()
        {
            FadeOut(fadeOutDuration, () =>
            {
                base.Pause();
            });
        }

        public override void UnPause()
        {
            base.UnPause();
            FadeIn(fadeInDuration);
        }

        /// <summary>
        /// 次第に聞こえるようにする
        /// </summary>
        public void FadeIn(float duration, Action callback = null)
        {
            if (!IsPlaying) Source.Play();

            Volume = 0.0f;

            ChangeVolume(0.0f, RealVolume, duration, callback);
        }

        /// <summary>
        /// 徐々に聞こえないようにする
        /// </summary>
        public void FadeOut(float duration, Action callback = null)
        {
            ChangeVolume(Volume, 0.0f, duration, callback);
        }

        void ChangeVolume(float startVolume, float endVolume, float duration, Action callback = null)
        {
            StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
            {
                Volume = Mathf.Lerp(startVolume, endVolume, t);
            }).OnCompleted(() =>
            {
                if (callback != null) callback.Invoke();
            }));
        }
    }
}
