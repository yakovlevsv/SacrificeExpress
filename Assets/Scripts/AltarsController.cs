using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AltarsController : MonoBehaviour {

    public static AltarsController instance;

    public TimeSpan ProcessTime { get { return TimeSpan.FromSeconds(_processTime); } }

    [SerializeField]
    int _startPeriod;
    [SerializeField]
    int _periodStep;
    [SerializeField]
    int _minPeriod;
    [Header("Altar")]
    [SerializeField]
    float _processTime;
    [SerializeField]
    float _waitTime;

    List<Altar> _altars = new List<Altar>();
    int _altarIndex;
    int _period;

    void Awake()
    {
        instance = this;
        _altars.AddRange(FindObjectsOfType<Altar>());
    }

    IEnumerator Start()
    {
        yield return null;

        _period = _startPeriod;

        while (true)
        {
            var altar = GetNextAltar();
            if (altar == null)
            {
                yield return null;
                continue;
            }

            altar.Wait(GetNextVictum(), TimeSpan.FromSeconds(_waitTime));
            yield return new WaitForSeconds(_period);

            _period = Mathf.Max(_period - _periodStep, _minPeriod);
        }
    }

    Altar GetNextAltar()
    {
        var sleepAltars = _altars.FindAll(a => a.IsSleep && !a.IsSpecial);
        return sleepAltars[_altarIndex++ % sleepAltars.Count];
    }

    public Altar GetClosestAltar(Vector3 position, float maxDistance)
    {
        if (_altars.Count == 0)
            return null;

        float minDistance = Helpers.GetDistance(_altars[0].transform.position, position);
        int index = 0;
        for (int i = 1; i < _altars.Count; i++)
        {
            float distance = Helpers.GetDistance(_altars[i].transform.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }
        return minDistance < maxDistance ? _altars[index] : null;
    }



    VictumTypes GetNextVictum()
    {
        var types = (VictumTypes[])Enum.GetValues(typeof(VictumTypes));

        var probsTypes = new List<VictumTypes>();
        foreach(VictumTypes type in types)
        {
            probsTypes.Add(type);
            if (!HasVictumType(type))
                probsTypes.Add(type);
        }

        int index = UnityEngine.Random.Range(0, probsTypes.Count);
        return probsTypes[index];
    }

    bool HasVictumType(VictumTypes type)
    {
        return _altars.Find(a => a.IsWaiting && a.VictumType == type) != null;
    }
}
