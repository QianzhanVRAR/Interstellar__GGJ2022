using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace QZVR
{
    public class DetailedVive : IControl
    {
        public static bool  AtAct=false ;
        public static DetailedVive Instance  ;

        public RectTransform canvasRectTransform;
        public Camera mainCamera;
        public GameObject rootObj;
        public float Space;
        public float Test;
        public StarTriggerTool AtTrigger;
        public  LineRenderer LineRenderer;
        TMP_Text MaxEnergy;
        TMP_Text AtEnergy;

        private float  MaxEnergyData;
        private float  AtEnergyData;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
         
            rootObj.SetActive(false);
            MaxEnergy = this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "MaxEnergy");
            AtEnergy = this.GetUtility<TransfromUtility>().FindChild<TMP_Text>(transform, "AtEnergy");
        }
        public void ShowOrHUi(StarTriggerTool Trigger)
        {
            if (AtTrigger!=null&& AtTrigger == Trigger)
            {
                AtAct = false ;
                rootObj.SetActive(AtAct);
                AtTrigger=null;
                return;
            }
            AtAct = true;
            rootObj.SetActive(AtAct);
            //TODO更新照片
            MaxEnergyData = this.GetModel<PlayerModel>().HaveEnergy .Value;
            MaxEnergy.text ="/"+ ((int )MaxEnergyData).ToString ();
            AtEnergyData = this.GetSystem<ExploreSystem>().GetEnergyConsumption(Trigger.details);
            AtEnergy.text = ((int)AtEnergyData).ToString ();
            AtTrigger = Trigger;
            LineRenderer.SetPosition(0, Trigger.transform.position);
            LineRenderer.SetPosition(1, this.GetModel<BattleshipModel>().PitchOn.OnPlanet.Value .WordLocation + (((Vector2)Trigger.transform.position - this.GetModel<BattleshipModel>().PitchOn.OnPlanet.Value.WordLocation).normalized * Space));

        }
      
        public override void Cinit()
        {
           
        }
        
        public Vector2 WorldToUgui(Vector3 position)
        {

            Vector2 screenPoint = mainCamera.WorldToScreenPoint(position);//世界坐标转换为屏幕坐标
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            screenPoint -= screenSize / 2;//将屏幕坐标变换为以屏幕中心为原点
            Vector2 anchorPos = screenPoint / screenSize * canvasRectTransform.sizeDelta;//缩放得到UGUI坐标
            return anchorPos;
        }


        public void SetSail()
        {
            if (this .GetModel <PlayerModel>().HaveEnergy.Value <AtEnergyData)
            {
                Debug.Log("能量不够");
                return;
            }
            if (this.GetModel<BattleshipModel>().PitchOn .OnPlanet.Value  .Equals ( AtTrigger.details))
            {
                return;
            }
            this.SendCommand(new SetSailCommand()
            {
                Energy = (int )AtEnergyData,
                ToPlanetEnum = AtTrigger.details,
                ExpendTime= AtEnergyData,
                FromPlanetEnum=this .GetModel <BattleshipModel>().PitchOn.OnPlanet.Value 
            });
            canvasRectTransform.gameObject.SetActive(false);
            ShowOrHUi(AtTrigger );
        }
    }
}