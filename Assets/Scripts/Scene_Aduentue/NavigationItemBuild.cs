using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using QZVR;

public class NavigationItemBuild : MonoBehaviour
{
    private  LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private readonly string pathSprite = "Assets/Sprites/Battleship/";
    public NavigationItemBuild Init()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        spriteRenderer= GetComponentInChildren<SpriteRenderer>();
        return this ;
    }
    public NavigationItemBuild SetStartPos( Vector3 vector)
    {
        lineRenderer.SetPosition(0, vector);
        transform.position = vector;
        return this;
    }
    public NavigationItemBuild SetEndPos(Vector3 vector)
    {

       /* spriteRenderer.transform.eulerAngles  = Vector3.forward *
           (Vector3.Angle( vector - spriteRenderer.transform.position, spriteRenderer.transform.up) + 8.0f);
        Debug.LogError (Vector3.Angle(vector - spriteRenderer.transform.position, spriteRenderer.transform.up) + 8.0f);*/
        lineRenderer.SetPosition(1, vector);
        return this;
    }

    public NavigationItemBuild SetSprite(string   path)
    {

        Debug.Log(pathSprite + path + ".png");
        Addressables.LoadAssetAsync<Sprite>(pathSprite+path+".png").Completed += (asset) 
         =>{
            spriteRenderer.sprite = asset.Result ;
         };
       
        return this;
    }

    private void Update()
    {
        transform.LookAt(lineRenderer.GetPosition(1));
    }
    public NavigationItemBuild SetPoint(BattleshipAduentueData data)
    {
        data.Progress.RegisterOnValueChanged((data =>
        {
            transform.transform.position = Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), data);
   
        })).UnRegisterWhenGameObjectDestroyed (gameObject );
        transform.position = Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), data.Progress.Value );
        return this;
    }
}
