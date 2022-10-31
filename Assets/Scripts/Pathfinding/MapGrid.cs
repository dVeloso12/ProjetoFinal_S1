using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class MapGrid<TGridObject>
{

    public event System.EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    int width, larg, height;
    float cellSize;

    //TGridObject[,,] TrigridArray;
    TGridObject[,] GridArray;

    TextMesh[,] debugTextArray;

    Vector3 originPosition;

    public GameObject groundCheck;
    public LayerMask groundLayer;

    public MapGrid(int _width, int _larg, int _height, float _cellSize, Vector3 _originPosition, GameObject groundCheck, LayerMask groundLayer, Func<MapGrid<TGridObject>, int, int, TGridObject> createGridObj)
    {

        width = _width;
        larg = _larg;
        height = _height;
        cellSize = _cellSize;
        originPosition = _originPosition;
        this.groundCheck = groundCheck;
        this.groundLayer = groundLayer;

        GridArray = new TGridObject[width, height];

        for(int x = 0; x < GridArray.GetLength(0); x++)
        {
            for(int y = 0; y < GridArray.GetLength(1); y++)
            {
                GridArray[x, y] = createGridObj(this, x, y);
            }
        }

        //Debug.Log("Width : " + GridArray.GetLength(0));
        //Debug.Log("Height : " + GridArray.GetLength(1));

        //Debug.Log("Value : " + GetWorldPosition((int)(GridArray.GetLength(0)), (int)(GridArray.GetLength(1))));

        DrawGrid();
        
        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
        {
            debugTextArray[eventArgs.x, eventArgs.y].text = GridArray[eventArgs.x, eventArgs.y]?.ToString();
        };

    }
        

    public void ResetGrid(int width, int height, Vector3 originPosition, GameObject groundCheck, Func<MapGrid<TGridObject>, int, int, TGridObject> createGridObj)
    {
        this.width = width; this.height = height;

        this.originPosition = originPosition;

        this.groundCheck = groundCheck;

        GridArray = new TGridObject[width, height];

        for(int x = 0;  x < GridArray.GetLength(0); x++)
        {
            for(int y = 0; y < GridArray.GetLength(1); y++)
            {
                GridArray[x, y] = createGridObj(this, x, y);
            }
        }

        DrawGrid();

    }

    public void DrawGrid()
    {
        bool ToDebug = true;


        if (ToDebug)
        {

            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < GridArray.GetLength(0); x++)
            {
                for (int y = 0; y < GridArray.GetLength(1); y++)
                {

                    //debugTextArray[x, y] = UtilsClass.CreateWorldText(GridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);

                    //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                    Vector3 worldPosition = GetWorldPosition(x, y);
                    Vector3 worldPositionYPlus1 = GetWorldPosition(x, y + 1);
                    Vector3 worldPositionXPlus1 = GetWorldPosition(x + 1, y);

                    //Debug.Log("World Position 01 : " + worldPosition);
                    //Debug.Log("World Position 02 : " + worldPositionYPlus1);
                    //Debug.Log("World Position 03 : " + worldPositionXPlus1);

                    //string toDebug = GridArray[x, y]?.ToString() + GridArray[x, y].



                    debugTextArray[x, y] = UtilsClass.CreateWorldText(GridArray[x, y]?.ToString(), null, new Vector3(worldPosition.x, 0, worldPosition.y) + new Vector3(cellSize, 0, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);

                    Debug.DrawLine(new Vector3(worldPosition.x, 0, worldPosition.y)
                        , new Vector3(worldPositionYPlus1.x, 0, worldPositionYPlus1.y)
                        , Color.white, 100f);


                    Debug.DrawLine(new Vector3(worldPosition.x, 0, worldPosition.y)
                        , new Vector3(worldPositionXPlus1.x, 0, worldPositionXPlus1.y)
                        , Color.white, 100f);

                }
            }

            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }

    }

    public LayerMask GetGroundLayer()
    {
        return groundLayer;
    }
    public GameObject GetGroundCheck()
    {
        return groundCheck;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;    

    }

    public void GetWorldPositionsOnMap(int x, int y)
    {

    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        //Debug.Log("WOrld position x : " + worldPosition.x);
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        //Debug.Log("Grid X : " + x);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

    }

    public void SetGridObj(int x, int y, TGridObject value)
    {
        if(x >= 0 && x < width && y >= 0 && y < height && value != null)
        {
            GridArray[x, y] = value;
            debugTextArray[x, y].text = GridArray[x, y].ToString();

            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

    }

    public void SetGridObj(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);

        SetGridObj(x, y, value);
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    public TGridObject GetGridObj(int x, int y)
    {        

        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return GridArray[x, y];
        }
        else
        {
            return default(TGridObject); 
        }

    }

    public TGridObject GetGridObj(Vector3 worldPosition)
    {
        int x, y;

        GetXY(worldPosition, out x, out y);

        return GetGridObj(x, y);

    }

}
