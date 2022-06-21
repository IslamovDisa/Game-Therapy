using UnityEngine;
using UnityEngine.UI;

public class WorldButton : MonoBehaviour
{
    public int DataIndex { get; set; }

    [SerializeField] private Text _label;
    [SerializeField] private Image _thumbnail;
    [SerializeField] protected Button _button;
    [SerializeField] protected Toggle _likeToggle;

    public void Init()
    {
        _label.text = DataController.Current.AppData.WorldModels[DataIndex].Name;
        var path = "Thumbnails/" + DataController.Current.AppData.WorldModels[DataIndex].ThumbnailPath;
        //Debug.Log(path);
        _thumbnail.sprite = Resources.Load<Sprite>(path);
        _likeToggle.isOn = DataController.Current.AppData.WorldModels[DataIndex].Like;
    }
    
    protected virtual void OnEnable()
    {
        if (_button == null)
        {
            return;
        }

        _button.onClick.AddListener(OnButtonClick);
        _likeToggle.onValueChanged.AddListener(OnLikeToggleClick);
    }

    protected virtual void OnDisable()
    {
        if (_button == null)
        {
            return;
        }
        
        _button.onClick.RemoveListener(OnButtonClick);
        _likeToggle.onValueChanged.RemoveListener(OnLikeToggleClick);
    }
    
    protected virtual void OnButtonClick()
    {
        var sceneLoadEvent = new SceneLoadEvent(DataController.Current.AppData.WorldModels[DataIndex].SceneName);
        EventManager.Instance.QueueEvent(sceneLoadEvent);
    }
    
    protected virtual void OnLikeToggleClick(bool value)
    {
        DataController.Current.AppData.WorldModels[DataIndex].Like = value;
    }
}
