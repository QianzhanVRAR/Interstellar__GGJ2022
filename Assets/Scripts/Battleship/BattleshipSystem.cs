using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public class BattleshipSystem :AbstractSystem
    {
        BattleshipModel BattleshipModel;
		protected override void OnInit()
		{
            BattleshipModel = this.GetModel<BattleshipModel>();
        }

        public void SetBattleshipDefPlanet(PlanetDetails planet )
        {
            BattleshipModel.TraversalBattleships((data) =>
           {
               if (!data.Init )
               {
                   data.InitPlanet(planet);

               }
           });
            BattleshipModel.SaveBattlenships();
        }
    }
}