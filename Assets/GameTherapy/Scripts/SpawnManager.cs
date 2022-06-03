using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpawnable
{
    [SerializeField] private List<GameObject> _spawnables = new List<GameObject>();
    [SerializeField] private DraggableManager _draggableManager;
    [SerializeField] private SelectionManager _selectionManager;
    
    private readonly List<GameObject> _allSpawnedObjects = new List<GameObject>();

    public IDraggable CurrentSpawnObject { get; set; }

    public void Spawn(int index)
    {
        _selectionManager.CurrentSelection = null;
        
        var spawnedActor = Instantiate(_spawnables[index]);
        CurrentSpawnObject = spawnedActor.GetComponent<DraggableActor>();
        _allSpawnedObjects.Add(spawnedActor);
    }

    public void Despawn(GameObject value)
    {
        if (_allSpawnedObjects.Contains(value))
        {
            Destroy(value);
        }
    }
}
