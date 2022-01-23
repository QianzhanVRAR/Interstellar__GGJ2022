
namespace QZVR
{
    public interface ISystem :IBelongToArchitecture ,ICanSetArchitecture,ICanGetModel ,ICanGetUtility ,ICanGetSystem, ICanRegisterEvent, ICanSendEvent
    {
        void Init();
    }

    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecturel;

        public IArchitecture GetArchitecture()
        {
            return mArchitecturel;
        }


        public void SetArchitecture(IArchitecture architecture)
        {
            mArchitecturel = architecture;
        }

        public void Init()
        {
            OnInit();
        }
        protected abstract void OnInit();

    }
}

