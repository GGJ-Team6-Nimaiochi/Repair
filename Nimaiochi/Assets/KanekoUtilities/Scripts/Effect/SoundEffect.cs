using System;
using UnityEngine;

namespace KanekoUtilities
{
    public class SoundEffect : MyAudioSource
    {
        Coroutine callBackCoroutine;
        
        public void Play(AudioClip clip, Action callback, float volume = 1.0f, float pitch = 1.0f)
        {
            Play(clip, volume, pitch);

            this.WaitUntil(() => !IsPlaying, callback);
        }

        //loopするSEを鳴らしたい場合は止める条件を設定する
        public void LoopPlay(AudioClip clip, float limitLife, Action callback, float volume = 1.0f, float pitch = 1.0f)
        {
            Source.loop = true;
            Play(clip, volume, pitch);

            KKUtilities.Delay(limitLife, () =>
            {
                Source.Stop();
                if (callback != null) callback.Invoke();
            }, this);
        }
        
        public void LoopPlay(AudioClip clip, Func<bool> predicate, Action callback, float volume = 1.0f, float pitch = 1.0f)
        {
            Source.loop = true;
            Play(clip, volume, pitch);

            this.WaitUntil(predicate, callback);
        }
    }
}