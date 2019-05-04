using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] Transform _start;
    [SerializeField] Transform _end;

    [SerializeField] Vector3 _startPoint;
    [SerializeField] Vector3 _endPoint;

    [SerializeField] float ROAD_SIZE = 5;

    List<RoadSegment> _roadsArray = new List<RoadSegment>();
    [SerializeField] GameObject _roadInstance;

    Vector3 newPoint;

    private void Start()
    {
        _startPoint = _start.position;
        _endPoint = _end.position;
    }
    void CreateRoad()
    {
        GameObject lRoad = Instantiate(_roadInstance);
        lRoad.GetComponent<RoadSegment>().DrawRoad(_startPoint, _endPoint);

        _startPoint = lRoad.GetComponent<RoadSegment>().intersectionPoint;
        _endPoint = lRoad.GetComponent<RoadSegment>().newPoint;

        _roadsArray.Add(lRoad.GetComponent<RoadSegment>());
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,70,200,40), "Draw Road"))
        {
            if (_roadsArray.Count != 0) ResetRoads();

            CreateRoad();
        }
    }

    private void ResetRoads()
    {
        for(int i = 0; i < _roadsArray.Count; i++)
        {
            Destroy(_roadsArray[i].gameObject);
        }

        _startPoint = _start.position;
        _endPoint = _end.position;

        _roadsArray.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_startPoint, 0.1f);
        Gizmos.DrawSphere(_endPoint, 0.1f);
    }
}
