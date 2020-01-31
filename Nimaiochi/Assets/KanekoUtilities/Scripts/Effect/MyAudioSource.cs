using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    /// <summary>
    /// AudioSourceのラッパ
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MyAudioSource : PoolMonoBehaviour
    {
        AudioSource source;
        public AudioSource Source
        {
            get
            {
                if (source != null) return source;

                source = GetComponent<AudioSource>();

                return source;
            }
        }
        public AudioClip Clip
        {
            get
            {
                return Source.clip;
            }
            set
            {
                Source.clip = value;
            }
        }
        public virtual float Volume
        {
            get
            {
                return Source.volume;
            }
            set
            {
                Source.volume = value;
            }
        }
        public virtual float Pitch
        {
            get
            {
                return Source.pitch;
            }
            set
            {
                Source.pitch = value;
            }
        }
        public int Priority
        {
            get
            {
                return Source.priority;
            }
            set
            {
                Source.priority = value;
            }
        }
        public bool IsPlaying { get { return Source.isPlaying; } }

        public virtual void Play(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
        {
            Clip = clip;
            Volume = volume;
            Pitch = pitch;
            Source.Play();
        }

        public virtual void PlayOneShot(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
        {
            float defaultPitch = Pitch;
            Pitch = pitch;
            Source.PlayOneShot(clip, volume);
            Pitch = defaultPitch;
        }

        public virtual void Pause()
        {
            Source.Pause();
        }

        public virtual void UnPause()
        {
            Source.UnPause();
        }

        public virtual void Restart()
        {
            //Source.UnPause
        }

        public virtual void Stop()
        {
            Source.Stop();
        }
    }
}