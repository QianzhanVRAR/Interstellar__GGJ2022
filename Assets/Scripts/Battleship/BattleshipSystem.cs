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


        public void DispatchBattlenship(Battleship battleship, int [] now, float  _durationTime, int _energyExpended, PlanetDetails _startPlanet, PlanetDetails _endPlanet)
        {
            BattleshipEnum bEnum = BattleshipModel.GetBlattlenshipType(battleship);
            if (BattleshipModel.GetBattleships(bEnum).Count == 0)
            {
                Debug.Log("²»´æÔÚ¸Ã´¬");
                return;
            }
            BattleshipAduentueData data = new BattleshipAduentueData();
            data.m_DataTime = now;
            data.Progress = new BindableProperty<float>() { Value = 0 };
            data.m_DurationTime = _durationTime;
            data.EnergyExpended = _energyExpended;
            data.StartPlanet = _startPlanet;
            data.EndPlanet = _endPlanet;
            data.Battleship = battleship;
            BattleshipModel.AddDetached(data);
            BattleshipModel.RemoveBattlenships(battleship);

            if (data.Battleship.Crews.Count == 0)
            {
                Crew temp = this.GetModel<CrewModel>().GetCrew();
                data.Battleship.Crews.Add(temp);
                temp.SetBattleship(data.Battleship);
            }
            battleship.SetToPlanetIng(_startPlanet.Location);
           
        }
    }

}