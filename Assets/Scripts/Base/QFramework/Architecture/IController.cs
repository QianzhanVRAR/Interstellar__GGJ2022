
using UnityEngine;

namespace QZVR {
    public interface IController :IBelongToArchitecture, ICanGetUtility, ICanGetSystem, ICanGetModel, ICanSendCommand, ICanRegisterEvent {

    }

    public class AbstractController : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return InterstellarApp.Instance;
        }
    }

   
}


