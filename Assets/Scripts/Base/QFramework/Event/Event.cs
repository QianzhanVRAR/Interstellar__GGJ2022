using System;


namespace QZVR
{
  
    /// <summary>
    /// 全局事件
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <typeparam name="T1">参数</typeparam>
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
        /// 触发
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
        /// 触发
        /// </summary>
        public static void Trigger( )
        {
            mOnEvent?.Invoke();
        }
    }
}

