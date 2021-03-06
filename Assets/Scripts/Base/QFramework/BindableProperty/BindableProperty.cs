using System;

namespace QZVR 
{
    [System.Serializable]
    public class BindableProperty<T> where T : IEquatable<T>
    {
        public  T mValue;

        public T Value
        {
            get => mValue;
            set
            {
                if (mValue==null ||!mValue.Equals(value))
                {
                    mValue = value;
                    mOnValueChanged?.Invoke(value);
                }
            }
        }

        private Action<T> mOnValueChanged = (v) => { }; // -+

        public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged) // +
        {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged) // +
        {
            mOnValueChanged -= onValueChanged;
        }
    }

    public class BindablePropertyUnRegister<T> : IUnRegister where T : IEquatable<T> // +
    {
        public BindableProperty<T> BindableProperty { get; set; }

        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChanged);
        }
    }

}


