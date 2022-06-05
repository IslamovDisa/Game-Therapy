using UnityEngine;
using UnityEngine.UI;

public class WorldElementInfoButton : MonoBehaviour
{
    [SerializeField] protected WorldElementInfo worldElementInfo;

    public WorldElementInfo WorldElementInfo
    {
        get => worldElementInfo;
        set => worldElementInfo = value;
    }

    [Space(10)] 
    [SerializeField] private Text _label;
    [SerializeField] private Image _thumbnail;
    
    public virtual void Init()
    {
        _label.text = worldElementInfo.Name;
        _thumbnail.sprite = worldElementInfo.Thumbnail;
    }
}
