using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class Grid<TGridObject>
{

	public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;

	public class OnGridValueChangedEventArgs : EventArgs
	{
		public int x;
		public int z;
	}
    int width;
    int height;
    float cellSize;
    Vector3 originPosition;

    TGridObject[,] gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int z = 0; z < gridArray.GetLength(0); z++) {
	        for (int x = 0; x < gridArray.GetLength(1); x++)
	        {
		        gridArray[x, z] = createObject(this, x, z);
	        }
        }

        bool showDebug = true;
		if(showDebug){
        TextMesh[,] debugTextArray = new TextMesh[width, height];
        
        for (int z = 0; z < gridArray.GetLength(0); z++)
        {
            for (int x = 0; x < gridArray.GetLength(1); x++)
            {
                debugTextArray[x, z] = UtilsClass.CreateWorldText(gridArray[x, z]?.ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f, (int)(cellSize * 0.005f), Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
            }
        }
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        
		OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
		{
			debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();	
		};
	}

    }

    public int GetWidth()
    {
	    return gridArray.GetLength(0);
    }
    public int GetHeight()
    {
	    return gridArray.GetLength(1);
    }

    public float GetCellSize() {
		return cellSize;
    }

    private Vector3 GetWorldPosition(int x, int z) {
        
        return new Vector3(x, 0, z) * cellSize + originPosition;
        
    }

	public void GetXZ(Vector3 worldPosition, out int x, out int z){
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
	}
    
    
    public void SetGridObject(int x, int z, TGridObject value)
    {
        if(x >= 0 && z >= 0 && x < width && z < height){
            gridArray[x, z] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs{x = x, z = z});
        } 
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
	    if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs{x = x, z = z});
    }

	public void SetGridObject(Vector3 worldPosition, TGridObject value){
		int x, z;
		GetXZ(worldPosition, out x, out z);
		SetGridObject(x, z, value);
	}

	public TGridObject GetGridObject(int x, int z){
        if(x >= 0 && z >= 0 && x < width && z < height){
            return gridArray[x, z];
        }else{
			return default(TGridObject);
		}
}

	public TGridObject GetGridObject(Vector3 worldPosition){
		int x, z;
		GetXZ(worldPosition, out x, out z);
		return GetGridObject(x, z);
	}
}