using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDataView : MonoBehaviour
{
    [SerializeField] private RectTransform _actorButtonParant;
    [SerializeField] private GameObject _actorButton;

    [SerializeField] private RectTransform _topDragEndPanel;
    
    public void Start()
    {
        LoadActorData();
        
        _topDragEndPanel.gameObject.SetActive(true);
    }
    
    private void LoadActorData()
    {
        for (var index = 0; index < DataController.Current.AppData.ActorModels.Length; index++)
        {
            if (!DataController.Current.AppData.ActorModels[index].Added)
            {
                continue;
            }
            
            var go = Instantiate(_actorButton, _actorButtonParant);
            var worldButton = go.GetComponent<ActorOnSceneButton>();

            worldButton.DataIndex = index;
            worldButton.Init();
        }
    }
}
