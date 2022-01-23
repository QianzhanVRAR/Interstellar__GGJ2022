using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace QZVR
{
    public class LogInContrl : IControl
    {

        public Button login;
        public GameObject ADDPlayer;
	    public override void Cinit()
        {
         
        }
        private void Start()
        {
            login.onClick.AddListener(LoginCallBask);
        }

        public void LoginCallBask()
        {
            if(!this .GetModel <PlayerModel >().StartGame .Value)
            {
                SceneManager.LoadScene(1);
                return;
            }
            else
            {
                ADDPlayer.SetActive(true);
                gameObject.SetActive(false);
            }
        }


    }
}