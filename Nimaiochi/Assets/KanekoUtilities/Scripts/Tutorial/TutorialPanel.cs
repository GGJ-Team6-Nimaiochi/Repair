using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public abstract class TutorialPanel : Panel
    {
        Coroutine tutorialAnimationCoroutine;

        void Start()
        {
            StartCoroutine(TutorialAnimation());
        }

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(TutorialAnimation());
        }

        protected abstract IEnumerator TutorialAnimation();
    }
}
