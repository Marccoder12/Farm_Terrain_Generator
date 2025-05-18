using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelEngine;

public class WorldManager : MonoBehaviour
{
    public Camera cam;
    Vector3 point;
    public byte myID = 2;
    public Material worldMaterial;
    private Container container;
    public Transform gridTracker;

    public VoxelColor[] WorldColors;
    private Voxel voxel;

    void Start()
    {
        #region Instance

        if (_instance != null) {
            if(_instance != null)
                Destroy(this);
        }
        else {
            _instance = this;
        }

        #endregion
        
        // grid = new Grid<Voxel>(10, 10, 10f, Vector3.zero)
        
        
        GameObject cont = new GameObject("Container");
        cont.transform.parent = transform;
        container = cont.AddComponent<Container>();
        container.Initialize(worldMaterial, Vector3.zero);
        //
        // for (int x = 0; x < 4; x++)
        // {
        //     for (int z = 0; z < 4; z++)
        //     {
        //         container[new Vector3(x, 0, z)] = new Voxel() { ID = 1 };
        //     }
        // }
        //
        // container.GenerateMesh();
        // container.UploadMesh();
        //     
    }
    
    void Update() {
        int x, y, z;
        point = GetPointFromCam();
        GetXZ(point, out x, out z);
        gridTracker.position = new Vector3(x, 0, z);
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            myID = 0;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            myID = 1;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            myID = 2;
        }

        if (Input.GetMouseButton(0))
        {
            container[new Vector3(x, 0, z)] = new Voxel() { ID = myID };
            
            container.GenerateMesh();
            container.UploadMesh();
        }

        if (Input.GetMouseButtonDown(1))
        {
            byte formerID = myID;
            myID = 0;
            container[new Vector3(x, 0, z)] = new Voxel() { ID = myID };
            myID = formerID;
            
            container.GenerateMesh();
            container.UploadMesh();
        }


    }
    
    Vector3 GetPointFromCam()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
             Vector3 pos = hit.point;
             pos.y = 0;
        return pos;
        }
        return default;
    }
    
    private void GetXZ(Vector3 worldPosition, out int x, out int z){
        x = Mathf.FloorToInt(worldPosition.x);
        z = Mathf.FloorToInt(worldPosition.z);
    }

    
    private static WorldManager _instance;
    #region Instance

    public static WorldManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<WorldManager>();
            return _instance;
        }
    }
    #endregion
}