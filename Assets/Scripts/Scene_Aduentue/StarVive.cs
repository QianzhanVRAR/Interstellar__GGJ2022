using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;
using TMPro;
public class StarVive : AbstractController
{
    public static StarVive Instance;
    private PlanetDetails AtPlanet;
    public  GameObject RootPath;
    public RectTransform canvasRectTransform;
    public Camera mainCamera;
    public TMP_Text Name;
    public TMP_Text LightYear ;
    private void Awake()
    {
        Instance = this;
        RootPath.SetActive(false);
        mainCamera = Camera.main;
    }
    public void ShowData(PlanetDetails planet,Vector2 localtion)
    {
        RootPath.SetActive(true );
        AtPlanet = planet;
        transform.localPosition  = WorldToUgui(localtion);
        Name.text = planet.Name;
        LightYear.text = this.GetSystem<ExploreSystem>().GetLightYear(planet.Location).ToString ();
    }
    public void HideData(PlanetDetails planet)
    {
       if (AtPlanet.Equals (planet))
        {
            RootPath.SetActive(false);
        }

    }
    public  Vector2 WorldToUgui(Vector3 position)
    {

        Vector2 screenPoint = mainCamera.WorldToScreenPoint(position);//��������ת��Ϊ��Ļ����
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        screenPoint -= screenSize / 2;//����Ļ����任Ϊ����Ļ����Ϊԭ��
        Vector2 anchorPos = screenPoint / screenSize * canvasRectTransform.sizeDelta;//���ŵõ�UGUI����
        return anchorPos;
    }
}