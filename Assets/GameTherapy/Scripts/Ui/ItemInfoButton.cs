using UnityEngine;
using UnityEngine.UI;

public class ItemInfoButton : WorldElementInfoButton
{
    [SerializeField] private Image _like;
    [SerializeField] private Image _dislike;
    
    public override void Init()
    {
        base.Init();

        var itemInfo = worldElementInfo as ItemInfo;
        _like.gameObject.SetActive(itemInfo.Like);
        _dislike.gameObject.SetActive(!itemInfo.Like);
    }
}
