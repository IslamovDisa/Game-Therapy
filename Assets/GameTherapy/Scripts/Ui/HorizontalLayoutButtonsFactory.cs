using System.Collections.Generic;
using UnityEngine;

public class HorizontalLayoutButtonsFactory : MonoBehaviour
{
    [SerializeField] private RectTransform _parant;
    [SerializeField] private List<WorldElementInfo> _itemsInfo = new List<WorldElementInfo>();
    [SerializeField] private GameObject _itemInfoButtonPrefab;
    
    private List<WorldElementInfoButton> _worldElementInfoButtons = new List<WorldElementInfoButton>();

    private void Start()
    {
        foreach (var itemInfo in _itemsInfo)
        {
            var itemInfoButton = Instantiate(_itemInfoButtonPrefab, _parant).GetComponent<ItemInfoButton>();
            itemInfoButton.WorldElementInfo = itemInfo;
            _worldElementInfoButtons.Add(itemInfoButton);
        }
    }
}
