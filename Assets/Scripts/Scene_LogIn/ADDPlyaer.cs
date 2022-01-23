using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace QZVR
{
    public class ADDPlyaer : IControl
    {
        public string InPutString { get; set; }
        public Button Login;

        private void Start()
        {
            Login.onClick.AddListener(() => { 
                this.SendCommand(new PlayerRegistrComplete() { name = InPutString});
                SceneManager.LoadScene(1);
            });
        }

        public override void Cinit()
        {
           
        }


    }
}