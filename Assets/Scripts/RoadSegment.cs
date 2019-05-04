using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RoadSegment : MonoBehaviour
{
    Vector3 startPoint;
    Vector3 endPoint;
    public Vector3 intersectionPoint;
    public Vector3 newPoint;
    float roadLenght;
    Vector3 roadVector;

    LineRenderer lr;

    public void DrawRoad(Vector3 pStartPoint, Vector3 pEndPoint, float pLenght, float pWidth)
    {
        lr = GetComponent<LineRenderer>();

        startPoint = pStartPoint;
        endPoint = pEndPoint;
        roadLenght = pLenght;

        roadVector = endPoint - startPoint;

        lr.SetPosition(0, startPoint);
        lr.SetPosition(1, endPoint);
        lr.SetWidth(pWidth, pWidth);

        intersectionPoint = GetCenterRoad(roadVector);
        newPoint = GetNewPoint();
    }

    Vector3 GetNewPoint()
    {
        Vector3 lOrthogonalVector = GetOrthogonalVector(roadVector);
        int lRandomDirection = GetRandomDirection();

        Vector3 lNewPoint = intersectionPoint + (lOrthogonalVector * roadLenght * lRandomDirection);

        return lNewPoint;
    }

    Vector3 GetCenterRoad(Vector3 pRoadVector)
    {
        Vector3 lCenterPoint = (startPoint + endPoint) / 2;
        int lRandomDirection = GetRandomDirection();

        lCenterPoint += pRoadVector.normalized * 0.4f * lRandomDirection;

        return lCenterPoint;
    }

    Vector3 GetOrthogonalVector(Vector3 pRoadVector)
    {
        Vector3 lOrthogonalVector = Vector3.Cross(pRoadVector, Vector3.up);
        lOrthogonalVector.Normalize();

        return lOrthogonalVector;
    }

    int GetRandomDirection()
    {
        int lDirection = Random.Range(-1, 1);
        if (lDirection == 0) lDirection = 1;

        return lDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(intersectionPoint, 0.1f);
        Gizmos.DrawSphere(newPoint, 0.1f);
        Gizmos.DrawLine(intersectionPoint, newPoint);
    }
}
