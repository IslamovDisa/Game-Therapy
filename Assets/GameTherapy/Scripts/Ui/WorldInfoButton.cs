using UnityEngine;
using UnityEngine.UI;

public class WorldInfoButton : WorldElementInfoButton
{
    [SerializeField] private Image _like;
    [SerializeField] private Image _dislike;

    public override void Init()
    {
        base.Init();

        var worldInfo = worldElementInfo as WorldInfo;
        _like.gameObject.SetActive(worldInfo.Like);
        _dislike.gameObject.SetActive(!worldInfo.Like);
    }
}
