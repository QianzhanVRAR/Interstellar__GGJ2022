using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace QZVR
{
    public class MainUI : IControl
    {
        PlayerModel playerModel;

        public override void Cinit()
        {
       

        }


        public  void ToAduentue()
        {
           // if (this .GetModel <BattleshipModel>().AllBattleshipsCount==0)
            //{
               // SceneManager.LoadScene("Aduentue");
           // }
            SceneManager.LoadScene("Aduentue");
        }
    }
}

