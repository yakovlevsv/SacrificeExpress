using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictumsController : MonoBehaviour {

    public static VictumsController instance;

    [SerializeField]
    GameObject _victumPrefab;
    [SerializeField]
    Transform[] _victumPlaces;

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
        GameObject victumBody = Instantiate(_victumPrefab, spawnPoint.position, Quaternion.identity, transform);
        _victums.Add(new Victum
        {
            body = victumBody.transform,
            spawnPoint = spawnPoint
        });
    }
}
