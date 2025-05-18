using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel {
    
    public byte ID;
    public Vector3 voxelPos;
    public bool isSolid
    {
        get {
            return ID != 0;
        }
    }

    void GetNeighBourVoxels()
    {
        
    }
    
}