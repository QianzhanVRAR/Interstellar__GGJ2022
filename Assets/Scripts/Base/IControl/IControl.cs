using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{

    public abstract class IControl : AbstractController 
{
        public float Priority = 0;
        public abstract void Cinit();

    }


}

