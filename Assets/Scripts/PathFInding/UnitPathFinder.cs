using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitPathFinder : MonoBehaviour
{
    private int currentPathIndex;
    private List<Vector3> pathVectorList = new List<Vector3>();
    
    [SerializeField]private float speed;
    void Update()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        if(pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 0.12f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
                //start moving animation
                //rotate to moveDirection
            }
            else {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count){
                    StopMoving();
                    //stop moving animation
                }
            }
        }
    }

    void StopMoving() {
        pathVectorList = null;
    }
    Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPosition);
            
            if(pathVectorList != null && pathVectorList.Count > 1) {
                pathVectorList.RemoveAt(0);
            }
    }
}