using UnityEngine;
using UnityEngine.UI;

public class WorldElementInfoButton : MonoBehaviour
{
    [SerializeField] protected WorldElementInfo worldElementInfo;
    
    [Space(10)] 
    [SerializeField] private Text _label;
    [SerializeField] private Image _thumbnail;
    
    public virtual void Start()
    {
        _label.text = worldElementInfo.Name;
        _thumbnail.sprite = worldElementInfo.Thumbnail;
    }
}
