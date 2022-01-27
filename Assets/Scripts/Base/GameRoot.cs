using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace QZVR 
{

    public class GameRoot : AbstractController  
    {
        public static GameRoot Instance;

        PlayerModel playerModel;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InterstellarApp.Instance.GetModel<PlayerModel>();
        }

        private void Start()
        {
            playerModel = this.GetModel<PlayerModel>();
            this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "Name").text = playerModel.Name.Value;

            TMP_Text EnergyValue = this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "EnergyValue");
            EnergyValue.text = $"{ playerModel.HaveEnergy.Value }/{ playerModel.MaxEnergy.Value }";
        }
        private void StartGameUI(bool  data)
        {

        }
    }


}

