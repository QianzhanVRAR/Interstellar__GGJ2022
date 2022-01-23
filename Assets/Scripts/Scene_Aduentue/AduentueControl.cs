using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace QZVR
{
    public class AduentueControl : IControl
    {
        public TMP_Text Name; 
        PlayerModel playerModel;
        private void Awake()
        {
            playerModel = this.GetModel<PlayerModel>();
            Debug.Log(playerModel.Name.Value);
            Name.text = playerModel.Name.Value ;
            playerModel.Name.RegisterOnValueChanged(
                (data) =>
                {
                    Name.text = data;
                }
                ).UnRegisterWhenGameObjectDestroyed(gameObject); 
        }
       
        public override void Cinit()
        {
           
        }
        private void Start()
        {
            TMP_Text EnergyValue = this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "EnergyValue");
            EnergyValue.text = $"{ playerModel.HaveEnergy.Value }/{ playerModel.MaxEnergy.Value }";


            this.GetModel<PlayerModel>().HaveEnergy.RegisterOnValueChanged(
          (data) => {

              EnergyValue.text = data.ToString()+ $"/{ playerModel.MaxEnergy.Value }";
          }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}