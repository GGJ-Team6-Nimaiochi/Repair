using UnityEngine;

namespace KanekoUtilities
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField]
        NumberText fpsText = null;
        
        int frameCount;
        const float updateIntervalTime = 0.5f;
        float elapasedTime;

        void Start()
        {
            frameCount = 0;
        }

        void Update()
        {
            frameCount++;
            elapasedTime += Time.unscaledDeltaTime;

            if (elapasedTime >= updateIntervalTime)
            {
                fpsText.SetValue((int)(frameCount / elapasedTime));

                elapasedTime = 0.0f;
                frameCount = 0;
            }
        }
    }
}