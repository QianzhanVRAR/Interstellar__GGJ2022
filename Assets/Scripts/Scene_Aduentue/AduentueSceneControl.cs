using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;
using UnityEngine.AddressableAssets;

public class AduentueSceneControl : ISceneControl
{
    public AssetReference NavigationItem;
    public GameObject PurposeSlelcted;
    public GameObject NavigationObj;
    public BattleshipModel BattleshipModel;
    public float remt;
    public Dictionary<Battleship, NavigationItemBuild> builds = new Dictionary<Battleship, NavigationItemBuild>() ;
    protected override void Awake()
    {
        this.RegisterEvent<BattleshipEvent.DispatchBattlenship>(DispatchBattlenship).UnRegisterWhenGameObjectDestroyed(gameObject);
        this.RegisterEvent<BattleshipEvent.RemoveDispatchBattlenship>(RemoveDispatchBattlenship).UnRegisterWhenGameObjectDestroyed(gameObject);

        base.Awake();
        BattleshipModel = this.GetModel<BattleshipModel>();
        if (BattleshipModel.AllBattleshipsCount  ==0)
        {
            PurposeSlelcted.SetActive(false);
        }
        this.SendCommand<SetBattleshipWordLocationCommand>();

        foreach (var item in BattleshipModel.Detached)
        {
            if (NavigationObj == null)
            {
                NavigationItem.LoadAssetAsync<GameObject>().Completed += (data) => {
                    NavigationObj = data.Result;
                    InitDispatchBattlenship(NavigationObj, item);
                };
            }
            else
            {
                InitDispatchBattlenship(NavigationObj, item);
            }

        }
    }

    private void InitDispatchBattlenship(GameObject obj, BattleshipAduentueData data)
    {
        NavigationItemBuild itemBuild = GameObject.Instantiate<GameObject>(obj).GetComponent<NavigationItemBuild>();
        itemBuild.Init()
        .SetStartPos(data.StartPlanet.WordLocation)
        .SetEndPos(data.EndPlanet.WordLocation)
        .SetSprite(data.Battleship.Details.Name)
        .SetPoint(data )
        ;
        builds.Add(data.Battleship,itemBuild);
    }

    private void OnGUI()
    {
        if (GUILayout .Button("asdas"))
        {
           _= this.GetModel<ExploreModel>().GetExploreEventData( ExploreEventEnum.B2);
        }
    }

    private void DispatchBattlenship(BattleshipEvent.DispatchBattlenship  dispatch)
    {
        PurposeSlelcted.SetActive(false);
        if (NavigationObj==null)
        {
            NavigationItem.LoadAssetAsync<GameObject>().Completed += (data) => {
                NavigationObj = data.Result;
                InitDispatchBattlenship(NavigationObj, dispatch.data);
            };
        }
        else
        {
            InitDispatchBattlenship(NavigationObj, dispatch.data);
        }
    }


    private void RemoveDispatchBattlenship(BattleshipEvent.RemoveDispatchBattlenship dispatch)
    {
        Destroy(builds[dispatch.data.Battleship].gameObject );
        builds.Remove (dispatch.data.Battleship);
        PurposeSlelcted.gameObject.SetActive(true);
    }

    
}
