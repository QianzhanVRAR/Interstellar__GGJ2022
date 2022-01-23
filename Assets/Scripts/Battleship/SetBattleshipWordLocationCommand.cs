using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public class SetBattleshipWordLocationCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<BattleshipSystem>().SetBattleshipDefPlanet(this.GetModel<PlanetModel>().StartPlanet);
        }
    }
}


