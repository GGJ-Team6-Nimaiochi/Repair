using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    //PlayOneShotで呼べるパーティクルの条件
    /*
     大前提
        ・瞬間的なエフェクトであること（爆発など）
     Unityでの設定
        ・ParticleSystemのEmissionのチェックを外す(外さなかった場合は初回のみ２倍の量が放出される)
        ・ParticleSystemのPlayOnAwakeをtrueにする(trueにしなかった場合はパーティクルにスケールや回転がかかっていても無視される)

    */

    /// <summary>
    /// パーティクルの生成をPlay又はPlayOneShotを呼ぶことで実現できる
    /// </summary>
    public class ParticleManager : SingletonMonobehaviour<ParticleManager>
    {
        Dictionary<string, ObjectPool> particleDictionary = new Dictionary<string, ObjectPool>();
        Dictionary<string, MomentParticle> momentParticleDictionary = new Dictionary<string, MomentParticle>();

        [SerializeField]
        string[] preloadParticleNames = null;
        [SerializeField]
        string[] preloadMomentParticleNames = null;

        protected override void Awake()
        {
            base.Awake();
            foreach(var p in preloadParticleNames) GetMomentParticle(p);
            foreach(var p in preloadMomentParticleNames) GetMomentParticle(p);
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置する
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 localPosition, Quaternion localRotation, Transform parent)
        {
            ObjectPool pooler = GetParticlePooler(name);
            if (pooler == null) return null;

            PoolMonoBehaviour particle = pooler.GetInstance();

            particle.transform.parent = parent;
            particle.transform.localPosition = localPosition;
            particle.transform.localRotation = localRotation;
            particle.gameObject.SetActive(true);

            return particle;
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置する
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 position, Quaternion rotation)
        {
            ObjectPool pooler = GetParticlePooler(name);
            if (pooler == null) return null;

            PoolMonoBehaviour particle = pooler.GetInstance();

            particle.transform.parent = pooler.transform;
            particle.transform.position = position;
            particle.transform.rotation = rotation;
            particle.gameObject.SetActive(true);

            return particle;
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置する
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 localPosition, Transform parent)
        {
            return Play(name, localPosition, Quaternion.identity, parent);
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置し、limitLife秒後にactiveをfalseにする
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 localPosition, Quaternion localRotation, float limitLife, Transform parent)
        {
            PoolMonoBehaviour particle = Play(name, localPosition, localRotation, parent);

            KKUtilities.Delay(limitLife, () => particle.gameObject.SetActive(false), this);

            return particle;
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置し、limitLife秒後にactiveをfalseにする
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 localPosition, float limitLife, Transform parent)
        {
            return Play(name, localPosition, Quaternion.identity, limitLife, parent);
        }
        
        /// <summary>
        /// 指定された地点にパーティクルを配置し、limitLife秒後にactiveをfalseにする
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 position, Quaternion rotation, float limitLife)
        {
            PoolMonoBehaviour particle = Play(name, position, rotation);

            KKUtilities.Delay(limitLife, () => particle.gameObject.SetActive(false), this);

            return particle;
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置する
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 position)
        {
            return Play(name, position, Quaternion.identity);
        }

        /// <summary>
        /// 指定された地点にパーティクルを配置し、limitLife秒後にactiveをfalseにする
        /// </summary>
        public PoolMonoBehaviour Play(string name, Vector3 position, float limitLife)
        {
            return Play(name, position, Quaternion.identity, limitLife);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position)
        {
            PlayOneShot(name, position, Quaternion.identity, transform);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position, Quaternion rotation)
        {
            PlayOneShot(name, position, rotation, transform);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position, Transform parent)
        {
            PlayOneShot(name, position, Quaternion.identity, parent);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position, Quaternion rotation, Transform parent)
        {
            MomentParticle particle = GetMomentParticle(name);

            if (particle == null) return;

            particle.transform.SetPositionAndRotation(position, rotation);
            particle.transform.SetParent(parent);
            particle.Play();

            KKUtilities.Delay(particle.ParticleSystem.main.duration, () => particle.transform.SetParent(transform), this);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position, Color color)
        {
            PlayOneShot(name, position, Quaternion.identity, color, transform);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position,Quaternion rotation,  Color color)
        {
            PlayOneShot(name, position, rotation, color, transform);
        }

        /// <summary>
        /// 指定された地点に瞬間的なパーティクルを再生する
        /// </summary>
        public void PlayOneShot(string name, Vector3 position, Quaternion rotation, Color color, Transform parent)
        {
            MomentParticle particle = GetMomentParticle(name);

            if (particle == null) return;

            particle.transform.SetPositionAndRotation(position, rotation);
            particle.transform.SetParent(parent);

            var main = particle.ParticleSystem.main;
            var defaultColor = main.startColor;
            main.startColor = color;
            particle.Play();
            main.startColor = defaultColor;

            KKUtilities.Delay(main.duration, () => particle.transform.SetParent(transform), this);
        }

        ObjectPool GetParticlePooler(string name)
        {
            ObjectPool pooler;
            if (particleDictionary.TryGetValue(name, out pooler))
            {
                return pooler;
            }

            ObjectPool poolerPrefab = MyAssetStore.Instance.GetAsset<ObjectPool>(name, "Particles/");

            if (poolerPrefab == null) return null;

            pooler = Instantiate(poolerPrefab, transform);

            //登録する
            particleDictionary.Add(name, pooler);

            return pooler;
        }

        MomentParticle GetMomentParticle(string name)
        {
            MomentParticle particle;

            if (momentParticleDictionary.TryGetValue(name, out particle))
            {
                return particle;
            }

            MomentParticle particlePrefab = MyAssetStore.Instance.GetAsset<MomentParticle>(name, "Particles/");

            if (particlePrefab == null) return null;

            particle = Instantiate(particlePrefab, transform);
            
            momentParticleDictionary.Add(name, particle);

            return particle;
        }
    }
}