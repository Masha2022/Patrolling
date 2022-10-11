using System.Collections.Generic;
using UnityEngine;

public class PatrollingBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _stopTime = 2f;

    private float _counterMovement;
    private int _startPoint;
    private int _endPoint;
    private int _index;
    private float _time;
    private float _distance;
    private float _duration;
    private float _stopTimeProgress;

    private void Start()
    {
        _cube = Instantiate(_cube, _points[0].position, Quaternion.Euler(0, 0, 0));
    }

    private void Update()
    {
        if (_stopTimeProgress < _stopTime) //cчетчик для задержки на позиции
        {
            _stopTimeProgress += Time.deltaTime; //если время для смены _points не пришло, накапливаю счетчик
            return;
        }

        _startPoint = _index % _points.Count;
        _endPoint = (_startPoint + 1) % _points.Count;

        _distance = CalculateDistance();
        _counterMovement = _distance / _speed;

        if (_time < _counterMovement)
        {
            KeepMoving();
        }
        else
        {
            _time = 0;
            _stopTimeProgress = 0;
            _index++;
        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(_points[_startPoint].position,
            _points[_endPoint].position); //нахожу дистанцию между точками
    }

    private void KeepMoving()
    {
        _time += Time.deltaTime;
        float distanceTraveled = _time * _speed; //вычисляю пройденное расстояние;
        _duration = distanceTraveled / _distance; //делю пройденное расстояние на весь путь от одной т. к другой
        _cube.transform.position = Vector3.Lerp(_points[_startPoint].position, _points[_endPoint].position,
            _duration);
    }
}