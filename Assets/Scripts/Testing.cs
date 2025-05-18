using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    
    [SerializeField]private UnitPathFinder unit;
    private PathFinding pathFinding;
    private void Start()
    {
        pathFinding = new PathFinding(10, 10);
    }

    private void Update()
    {
        Vector3 mouseWorldPos = GetPointFromCam();
        pathFinding.GetGrid().GetXZ(mouseWorldPos, out int x, out int z);
        
        if (Input.GetMouseButtonDown(0))
        {
            List<PathNode> path = pathFinding.FindPath(0, 0, x, z);
            if (path != null) {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(path[i].x, path[i].z) + Vector3.one * 0.5f,
                            new Vector3(path[i+1].x, path[i+1].z) + Vector3.one * 0.5f);
                    }
            }
            //movePlayer......
            unit.SetTargetPosition(new Vector3(3, 7));
        }
        
        //Set to not walkable
        if (Input.GetMouseButtonDown(1)) {
            pathFinding.GetNode(x, z).SetIsWalkable(!pathFinding.GetNode(x, z).isWalkable);
        }
    }
    
    Vector3 GetPointFromCam()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            Vector3 pos = hit.point;
            pos.y = 0;
            return pos;
        }
        return default;
    }
}
