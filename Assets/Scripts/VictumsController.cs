using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VictumsController : MonoBehaviour {

    public static VictumsController instance;

    [Serializable]
    public class VictumInfo
    {
        public VictumTypes type;
        public GameObject prefab;
        public float speedCoef;
    }

    [SerializeField]
    VictumInfo[] _victumPrefab;
    [SerializeField]
    Transform[] _victumPlaces;

    List<VictumTypes> _nextVictums = new List<VictumTypes>();
    List<Victum> _victums = new List<Victum>();

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        foreach(var spawn in _victumPlaces)
            SpawnNewVictum(spawn);
    }

    public Victum GetClosestVictum(Vector3 position, float maxDistance)
    {
        if (_victums.Count == 0)
            return null;

        float minDistance = Helpers.GetDistance(_victums[0].body.position, position);
        int index = 0;
        for (int i = 1; i < _victums.Count; i++)
        {
            float distance = Helpers.GetDistance(_victums[i].body.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }
        return minDistance < maxDistance ? _victums[index] : null;
    }

    public void KillVictum(Victum victum)
    {
        _victums.Remove(victum);
        SpawnNewVictum(victum.spawnPoint);
    }

    void SpawnNewVictum(Transform spawnPoint)
    {
        if (_nextVictums.Count == 0)
        {
            var types = (VictumTypes[])Enum.GetValues(typeof(VictumTypes));

            foreach(VictumTypes type in types)
                _nextVictums.Add(type);

            Helpers.Shuffle(_nextVictums);
        }

        var victumType = _nextVictums[0];
        _nextVictums.RemoveAt(0);
        GameObject prefab = GetVictumInfo(victumType).prefab;

        GameObject victumBody = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform);
        _victums.Add(new Victum
        {
            body = victumBody.transform,
            spawnPoint = spawnPoint,
            type = victumType
        });
    }

    public VictumInfo GetVictumInfo(VictumTypes victumType)
    {
        return Array.Find(_victumPrefab, vp => vp.type == victumType);
    }
}
