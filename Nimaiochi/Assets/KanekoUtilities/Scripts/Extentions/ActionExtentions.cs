using System;

namespace KanekoUtilities
{
    public static class ActionExtentions
    {
        public static void SafeInvoke(this Action action)
        {
            if (action != null) action.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> action, T param)
        {
            if (action != null) action.Invoke(param);
        }

        public static void SafeInvoke<T, U>(this Action<T, U> action, T param1, U param2)
        {
            if (action != null) action.Invoke(param1, param2);
        }

        public static void SafeInvoke<T, U, V>(this Action<T, U, V> action, T param1, U param2, V param3)
        {
            if (action != null) action.Invoke(param1, param2, param3);
        }

        public static void SafeInvoke(this MyUnityEvent action)
        {
            if(action != null) action.Invoke();
        }

        public static void SafeInvoke<T>(this MyUnityEvent<T> action, T param)
        {
            if(action != null) action.Invoke(param);
        }

        public static void SafeInvoke<T, U>(this MyUnityEvent<T, U> action, T param1, U param2)
        {
            if(action != null) action.Invoke(param1, param2);
        }
    }
}