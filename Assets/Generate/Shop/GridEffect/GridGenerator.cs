using System.Collections;
using UnityEngine;

public class GridGenerator : MonoBehaviour{

    [Header("Prefabs/Mats/Shaders")]
    public Material linesMaterial;
    public GameObject pointPrefab;
    public Vector3 SpawnTarget;

    [Header("General Stuff")]
    public int dimension;
    public int Size;
    Vector3 SpawnOnPosition;
    const string ALPHA = "_Alpha";
    public float MAXTRANSPARENCY = 0.5f;
    public bool _show;



    [Header("Points")]
    public GameObject points;
    public Material pointsMaterial;
    bool havePoints;
    public bool createNewPoints;

  

    void Start(){
        havePoints = false;
        resetMaterials();
    }

    void Update(){

        SpawnOnPosition = SpawnTarget;
        if(_show)
        {
        //spawnPoints();
        show();
        }
        else
        {
            hide();
        }
        
    }

    /// <summary>
    ///Urp Render Pipeline
    /// </summary>
    private void OnRenderObject()
    {
        draw3DMatrix();
    }

    /// <summary>
    ///Normal Render Pipeline
    /// </summary>
    //void OnPostRender(){
    //    draw3DMatrix();
    //}

    void draw3DMatrix()
    {
 
       Vector3 offset = new Vector3(dimension / 2, dimension / 2, dimension / 2);

        for (int i = 0; i <= dimension;i++)
        {
            for (int j = 0; j <= dimension; j++)
            {
               
                    Vector3 A1 = new Vector3(0, i*Size, j*Size) - offset + SpawnOnPosition;
                    Vector3 B1 = new Vector3(dimension * Size, i*Size, j*Size) - offset + SpawnOnPosition;
                    drawLine(A1, B1);

                    Vector3 A2 = new Vector3(j*Size, i*Size, 0) - offset + SpawnOnPosition;
                    Vector3 B2 = new Vector3(j*Size, i*Size, dimension * Size) - offset + SpawnOnPosition;
                    drawLine(A2, B2);

            }
        }

        for (int i = 0; i <= dimension; i++)
        {
            for (int j = 0; j <= dimension; j++)
            { 
                Vector3 A2 = new Vector3(j*Size, 0, i*Size) - offset + SpawnOnPosition;
                Vector3 B2 = new Vector3(j*Size, dimension*Size, i*Size) - offset + SpawnOnPosition;
                drawLine(A2, B2);
            }
        }

    }

    void drawLine(Vector3 from, Vector3 to){
        linesMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Vertex(from);
        GL.Vertex(to);
        GL.End();
    }

    void spawnPoints(){

        if (havePoints)
        {
            if(createNewPoints)
            {
                Destroy(points);
                createNewPoints = false;
                havePoints = false;
            }
        }
        else
        {
            points = new GameObject();
            Vector3 offset = new Vector3(dimension / 2, dimension / 2, dimension / 2);

            for (int i = 0; i <= dimension; i++)
            {
                for (int j = 0; j <= dimension; j++)
                {
                    for (int k = 0; k <= dimension; k++)
                    {
                        Vector3 pos = new Vector3(i*Size, j*Size, k*Size) - offset + SpawnOnPosition;
                        GameObject p = Instantiate(pointPrefab, pos, Quaternion.identity);
                        p.transform.SetParent(points.transform);
                    }
                }
            }
            havePoints = true;
        }
    }

    void resetMaterials(){
        linesMaterial.SetFloat(ALPHA, 0);
        pointsMaterial.SetFloat(ALPHA, 0);
    }

    public void show(){
        StartCoroutine(showCoroutine());
    }
    
    public void hide(){
        StartCoroutine(hideCoroutine());
    }

    IEnumerator showCoroutine(){
        float alpha = 0;
        linesMaterial.SetFloat(ALPHA, alpha);
        pointsMaterial.SetFloat(ALPHA, alpha);
        while (alpha < MAXTRANSPARENCY){
            alpha += 0.01f;
            linesMaterial.SetFloat(ALPHA, alpha);
            pointsMaterial.SetFloat(ALPHA, alpha);
            yield return null;
        }
    }

    IEnumerator hideCoroutine(){
        float alpha = MAXTRANSPARENCY;
        linesMaterial.SetFloat(ALPHA, alpha);
        pointsMaterial.SetFloat(ALPHA, alpha);
        while (alpha > 0f){
            alpha -= 0.01f;
            linesMaterial.SetFloat(ALPHA, alpha);
            pointsMaterial.SetFloat(ALPHA, alpha);
            yield return null;
        }
    }

}
