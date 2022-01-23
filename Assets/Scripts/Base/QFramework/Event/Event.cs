using System;


namespace QZVR
{
  
    /// <summary>
    /// ȫ���¼�
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    /// <typeparam name="T1">����</typeparam>
    public class Event<T,T1> where T:Event <T, T1>
    {
        private static Action<T1> mOnEvent;
        

        public static void Register(Action<T1> onEvent)
        {
            mOnEvent += onEvent;
        }
        public static void UnRegister(Action<T1> onEvent)
        {
            mOnEvent -= onEvent;
        }

        /// <summary>
        /// ����
        /// </summary>
        public static void Trigger(T1 v)
        {
            mOnEvent?.Invoke(v);
        }
    }
    public class Event<T> where T : Event<T>
    {
        private static Action mOnEvent;


        public static void Register(Action onEvent)
        {
            mOnEvent += onEvent;
        }
        public static void UnRegister(Action onEvent)
        {
            mOnEvent -= onEvent;
        }

        /// <summary>
        /// ����
        /// </summary>
        public static void Trigger( )
        {
            mOnEvent?.Invoke();
        }
    }
}

