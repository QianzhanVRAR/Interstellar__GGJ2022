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

        private void Start()
        {

            playerModel = this.GetModel<PlayerModel>();
            this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "Name").text = playerModel.Name.Value;

            TMP_Text EnergyValue = this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "EnergyValue");
            EnergyValue.text = $"{ playerModel.HaveEnergy.Value }/{ playerModel.MaxEnergy.Value }";

        }

        public  void ToAduentue()
        {
            SceneManager.LoadScene("Aduentue");
        }
    }
}

