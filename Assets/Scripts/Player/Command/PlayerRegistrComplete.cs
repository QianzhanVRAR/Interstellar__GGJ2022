using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR 
{
    public class PlayerRegistrComplete : AbstractCommand
    {
        public string name;
        protected override void OnExecute()
        {
            int ranCraw = Random.Range(0, 3);
            this.GetModel<CrewModel>().AddCrew((CrewEnum)ranCraw);
            this.GetModel<BattleshipModel >().AddBattlenships( BattleshipEnum.Discover);
            this.GetModel<PlayerModel>().InitPlayer (name);
        }
    }

}

