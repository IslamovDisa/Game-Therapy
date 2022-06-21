using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorButton : MonoBehaviour
{
    public int DataIndex { get; set; }

    [SerializeField] private Text _label;
    [SerializeField] private Image _thumbnail;
    [SerializeField] protected Toggle _addToggle;
    [SerializeField] protected Toggle _likeToggle;

    public void Init()
    {
        _label.text = DataController.Current.AppData.ActorModels[DataIndex].Name;
        _thumbnail.sprite =
            Resources.Load<Sprite>("Thumbnails/" +
                                    DataController.Current.AppData.ActorModels[DataIndex].ThumbnailPath);
        _likeToggle.isOn = DataController.Current.AppData.ActorModels[DataIndex].Like;
        _addToggle.isOn = DataController.Current.AppData.ActorModels[DataIndex].Added;
    }
    
    protected virtual void OnEnable()
    {
        _addToggle.onValueChanged.AddListener(OnAddToggleClick);
        _likeToggle.onValueChanged.AddListener(OnLikeToggleClick);
    }

    private void OnAddToggleClick(bool value)
    {
        DataController.Current.AppData.ActorModels[DataIndex].Added = value;
    }

    protected virtual void OnDisable()
    {
        _addToggle.onValueChanged.RemoveListener(OnAddToggleClick);
        _likeToggle.onValueChanged.RemoveListener(OnLikeToggleClick);
    }

    protected virtual void OnLikeToggleClick(bool value)
    {
        DataController.Current.AppData.ActorModels[DataIndex].Like = value;
    }
}
