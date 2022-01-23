using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public interface ICommand : IBelongToArchitecture,ICanSetArchitecture,ICanGetSystem ,ICanGetModel ,ICanGetUtility,ICanSendEvent ,ICanSendCommand
    {
        void Execute();
    }
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;

        public IArchitecture GetArchitecture()
        {
            return mArchitecture;
        }

        public void SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;

        }

        void ICommand.Execute()
        {
            OnExecute();
        }

        protected abstract void OnExecute();
    }
}

