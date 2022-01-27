using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public class SetSailCommand : AbstractCommand
    {
        public PlanetDetails ToPlanetEnum;
        public PlanetDetails FromPlanetEnum;
        public int Energy;
        public float  ExpendTime ;
        protected override void OnExecute()
        {
            BattleshipSystem exploreSystem = this.GetSystem <BattleshipSystem>();
            this.GetModel<PlayerModel>().HaveEnergy .Value -= Energy;
            //TODO时间系统

            exploreSystem.DispatchBattlenship(
                this.GetModel<BattleshipModel>().PitchOn,
                TimeSystem .GetTime(),
                ExpendTime,
                Energy,
                FromPlanetEnum,
                ToPlanetEnum
                ) ;
          
        }
    }

}
