using System;
using UnityEngine;
using UnityEngine.UI;

public class WorldElementInfoButton : MonoBehaviour
{
    [SerializeField] protected WorldElementInfo worldElementInfo;
    [SerializeField] protected Button _button;
    
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

    protected virtual void OnEnable()
    {
        if (_button == null)
        {
            return;
        }

        _button.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnDisable()
    {
        if (_button == null)
        {
            return;
        }
        
        _button.onClick.RemoveListener(OnButtonClick);
    }

    protected virtual void OnButtonClick()
    {
        var worldInfo = worldElementInfo as WorldInfo;
        var sceneLoadEvent = new SceneLoadEvent(worldInfo?.SceneName);
        EventManager.Instance.QueueEvent(sceneLoadEvent);
    }
}
