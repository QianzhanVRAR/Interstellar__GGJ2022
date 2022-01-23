using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MaskableGraphic
{
    public Texture texture;
    public override Texture mainTexture => texture;

    List<List<UIVertex>> vertexQuadList = new List<List<UIVertex>>();
    List<UIVertex> vertexQuad;
    public float lineWidth = 2;
    Vector3 lastleftPoint;
    Vector3 lastrightPoint;
    Vector3 lastPos;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        for (int i = 0; i < vertexQuadList.Count; i++)
        {
            vh.AddUIVertexQuad(vertexQuadList[i].ToArray());
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vertexQuadList.Clear();
            lastPos = Input.mousePosition;
            lastleftPoint = lastPos - new Vector3(Screen.width / 2, Screen.height / 2, 0) + Vector3.up * lineWidth;
            lastrightPoint = lastPos - new Vector3(Screen.width / 2, Screen.height / 2, 0) - Vector3.up * lineWidth;
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 newVec = Input.mousePosition - lastPos;
                if (newVec.magnitude < 0.1f)
                {
                    return;
                }

                vertexQuad = new List<UIVertex>();
                Vector3 vec = Vector3.Cross(newVec.normalized, Vector3.forward).normalized;
                Vector3 newleftPoint = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0) + vec * lineWidth;
                Vector3 newrightPoint = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0) - vec * lineWidth;
                UIVertex uIVertex = new UIVertex();
                uIVertex.position = lastleftPoint;
                uIVertex.color = color;
                vertexQuad.Add(uIVertex);
                UIVertex uIVertex1 = new UIVertex();
                uIVertex1.position = lastrightPoint;
                uIVertex1.color = color;
                vertexQuad.Add(uIVertex1);
                UIVertex uIVertex3 = new UIVertex();
                uIVertex3.position = newrightPoint;
                uIVertex3.color = color;
                vertexQuad.Add(uIVertex3);
                UIVertex uIVertex2 = new UIVertex();
                uIVertex2.position = newleftPoint;
                uIVertex2.color = color;
                vertexQuad.Add(uIVertex2);
                lastleftPoint = newleftPoint;
                lastrightPoint = newrightPoint;
                vertexQuadList.Add(vertexQuad);

                lastPos = Input.mousePosition;

                SetVerticesDirty();
            }
        }
    }

}
