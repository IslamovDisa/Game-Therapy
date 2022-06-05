using System.Collections.Generic;
using UnityEngine;

public class HorizontalLayoutButtonsFactory : MonoBehaviour
{
    [SerializeField] private RectTransform _parant;
    [SerializeField] private List<WorldElementInfo> _itemsInfo = new List<WorldElementInfo>();
    [SerializeField] private GameObject __worldElementInfoButtonPrefab;
    
    private List<WorldElementInfoButton> _worldElementInfoButtons = new List<WorldElementInfoButton>();

    private void Start()
    {
        foreach (var itemInfo in _itemsInfo)
        {
            var infoButton = Instantiate(__worldElementInfoButtonPrefab, _parant);
            var worldElementInfoButton = infoButton.GetComponent<WorldElementInfoButton>();
            
            worldElementInfoButton.WorldElementInfo = itemInfo;
            worldElementInfoButton.Init();
            
            _worldElementInfoButtons.Add(worldElementInfoButton);
        }
    }
}
