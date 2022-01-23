

namespace QZVR
{
    public interface  IModel :IBelongToArchitecture ,ICanSetArchitecture  ,ICanGetUtility ,ICanSendEvent,ICanGetModel
    {
        SQLiteHelper SQLit{ get; set; }
        void Init();
    }
    public abstract class AbstractModel : IModel
    {
        
        private IArchitecture mArchitecturel =null ;

        public string DatabasePrefixPath { get ; set ; }
       public SQLiteHelper SQLit { get ; set ; }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecturel;
        }


        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecturel = architecture;
        }
        void IModel.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();

       
    }

}

