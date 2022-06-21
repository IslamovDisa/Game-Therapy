using UnityEngine;
using UnityEngine.UI;

public class WeatherButton : MonoBehaviour
{
    public int DataIndex { get; set; }

    [SerializeField] private Text _label;
    [SerializeField] private Image _thumbnail;
    [SerializeField] protected Button _button;
    
    public void Init()
    {
        _label.text = DataController.Current.AppData.WeatherModels[DataIndex].Name;
        _thumbnail.sprite = Resources.Load<Sprite>("Thumbnails/" + DataController.Current.AppData.WeatherModels[DataIndex].ThumbnailPath);
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
    }
}
