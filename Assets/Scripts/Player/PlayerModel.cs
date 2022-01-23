using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QZVR
{
    public class PlayerModel : AbstractModel
    {
        public BindableProperty<bool> StartGame = new BindableProperty<bool>();
        public  BindableProperty <string > Name = new BindableProperty<string>();
        public BindableProperty<int> HaveEnergy = new BindableProperty<int>();
        public BindableProperty<int > MaxEnergy = new BindableProperty<int>();


        

        protected override void OnInit()
        {
            LoadPlayerData();
          
        }

        public void InitPlayer(string name)
        {
            Name.Value = name; 
            MaxEnergy.Value = GetStartEnergy();
            HaveEnergy.Value = MaxEnergy.Value;

            ES3.Save("PlayerName", name);
            ES3.Save("MaxEnergy", MaxEnergy.Value);
            ES3.Save("HaveEnergy", HaveEnergy.Value);
            ES3.Save("StartGame", true);
            RegPropertyEvent();
        }
     
        private void LoadPlayerData()
        {
            if (!ES3.KeyExists("StartGame"))
            {
                StartGame.Value = true ;
                this.SendEvent<PlayerEvent.StartGame>();
                return;
            }
            Name.Value = ES3.Load<string>("PlayerName");
            MaxEnergy.Value = ES3.Load<int>("MaxEnergy");
            HaveEnergy.Value = ES3.Load<int>("HaveEnergy");

            RegPropertyEvent();
        }


        private void RegPropertyEvent()
        {
            Name.RegisterOnValueChanged((v) =>
            {
                ES3.Save("PlayerName", v);
            });

            HaveEnergy.RegisterOnValueChanged((v) =>
            {
                ES3.Save("HaveEnergy", v);
            });

            MaxEnergy.RegisterOnValueChanged((v) =>
            {
                ES3.Save("MaxEnergy", v);
            });
        }




        private int GetStartEnergy()
        {
            return GetEnergy("初始能量");
        }


        private int  GetEnergy(string Type)
        {
           var  data=  SQLit.ReadTable("EnergyTables", new string[] { "Value" }, new string[] { "Type" }, new string[] { "=" }, new string[] { $"'{Type}'" });
            while (data.Read ())
            {
                return data.GetInt32(data.GetOrdinal("Value"));
            }
            Debug.LogError($"没有读取到数据库内容:表：EnergyTables---Type:{Type}");
            return 0;
        }
       
    }

}

