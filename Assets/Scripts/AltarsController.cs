using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AltarsController : MonoBehaviour {

    public static AltarsController instance;

    [SerializeField]
    int _startPeriod;
    [SerializeField]
    int _periodStep;
    [SerializeField]
    int _minPeriod;

    List<GameObject> _altars = new List<GameObject>();
    int _altarIndex;
    int _period;

    void Awake()
    {
        instance = this;
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

            // altar.AskVictum(GetNextVictum())
            yield return new WaitForSeconds(_period);

            _period = Mathf.Max(_period - _periodStep, _minPeriod);
        }
    }

    GameObject GetNextAltar()
    {
        return _altars[_altarIndex++ % _altars.Count];
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
        // Todo: implement
        return false;
    }
}
