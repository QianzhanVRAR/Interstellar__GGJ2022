using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public class ExploreSystem :AbstractSystem
    {
        BattleshipModel BattleshipModel;
        protected override void OnInit()
        {
            BattleshipModel=  this.GetModel<BattleshipModel>();


        }

		public int GetLightYear(Vector2Int planetVector)
        {
            //TODO角色技能较少光年
            return (int)(planetVector - BattleshipModel.PitchOn.Location.Value).magnitude;
        }
        public int GetEnergyConsumption(PlanetDetails details)
        {
            int consumptio = GetLightYear(details.Location);
            return consumptio;
        }
    }
}