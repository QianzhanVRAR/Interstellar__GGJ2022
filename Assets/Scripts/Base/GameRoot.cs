using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR 
{

    public class GameRoot : AbstractController  
    {
        public static GameRoot Instance;

        public PlayerModel PlayerModel; 



        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            InterstellarApp.Instance.GetModel<PlayerModel>();
        }
        private void StartGameUI(bool  data)
        {

        }
    }


}

