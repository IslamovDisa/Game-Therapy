using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpawnable
{
    public static SpawnManager Current;
    
    [SerializeField] private List<GameObject> _spawnables = new List<GameObject>();
    [SerializeField] private DraggableManager _draggableManager;
    [SerializeField] private SelectionManager _selectionManager;
    
    private readonly List<GameObject> _allSpawnedObjects = new List<GameObject>();

    public IDraggable CurrentSpawnObject { get; set; }
    
    private void Awake()
    {
        Current = this;
    }
    
    public void Spawn(int index)
    {
        _selectionManager.CurrentSelection = null;
        
        var spawnedActor = Instantiate(_spawnables[index]);
        CurrentSpawnObject = spawnedActor.GetComponent<DraggableActor>();
        _allSpawnedObjects.Add(spawnedActor);
    }
    
    public void Spawn(GameObject value)
    {
        _selectionManager.CurrentSelection = null;
        
        var spawnedActor = Instantiate(value);
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
