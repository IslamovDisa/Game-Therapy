using System.Collections.Generic;
using UnityEngine;

public class HorizontalLayoutButtonsFactory : MonoBehaviour
{
    [SerializeField] private RectTransform _parant;
    [SerializeField] private List<ItemInfo> _itemsInfo = new List<ItemInfo>();
    [SerializeField] private ItemInfoButton _itemInfoButtonPrefab;
    
    private List<ItemInfoButton> _itemInfoButtons = new List<ItemInfoButton>();

    private void Start()
    {
        foreach (var itemInfo in _itemsInfo)
        {
            var itemInfoButton = Instantiate(_itemInfoButtonPrefab, _parant);
            itemInfoButton.WorldElementInfo = itemInfo;
            _itemInfoButtons.Add(itemInfoButton);
        }
    }
}
