using System;
using System.Collections;

namespace KanekoUtilities
{
    public class MyCoroutine : IEnumerator
    {
        protected IEnumerator logic;
        Action onCompleted;
        public bool IsDone { get; private set; }

        public MyCoroutine(IEnumerator logic)
        {
            this.logic = logic;
        }

        public bool MoveNext()
        {
            Update();
            if(IsDone)
            {
                onCompleted.SafeInvoke();
                onCompleted = null;
            }
            return !IsDone;
        }
        public void Reset()
        {
            logic.Reset();
        }
        public object Current
        {
            get
            {
                return logic.Current;
            }
        }

        void Update()
        {
            IsDone = !logic.MoveNext();
        }

        public MyCoroutine OnCompleted(Action onCompleted)
        {
            this.onCompleted += onCompleted;
            return this;
        }

        public void CallCompletedSelf()
        {
            onCompleted.SafeInvoke();
            onCompleted = null;
        }
    }
}