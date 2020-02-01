using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    //瞬間的なパーティクル(でEmitterを生成しないでParticleSystem.Emitを使用し再生する)
    public class MomentParticle : MonoBehaviour
    {
        //ParticleSystem.Emitでどれだけ出すか
        [SerializeField]
        int burstCount = 30;

        public int BurstCount { get { return burstCount; } }

        [SerializeField]
        ParticleSystem m_particleSystem = null;

        public ParticleSystem ParticleSystem
        {
            get
            {
                if (m_particleSystem != null) return m_particleSystem;

                m_particleSystem = GetComponent<ParticleSystem>();

                return m_particleSystem;
            }
        }

        MomentParticle[] particles;

        MomentParticle[] Particles
        {
            get
            {
                if (particles != null) return particles;

                particles = GetComponentsInChildren<MomentParticle>();

                return particles;
            }
        }

        public void Play()
        {
            for (int i = 0; i < Particles.Length; i++)
            {
                Particles[i].ParticleSystem.Emit(Particles[i].BurstCount);
            }
        }
    }
}
