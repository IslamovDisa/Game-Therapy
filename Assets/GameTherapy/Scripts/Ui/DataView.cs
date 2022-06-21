using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataView : MonoBehaviour
{
    [SerializeField] private RectTransform _worldButtonsParant;
    [SerializeField] private GameObject _worldButton;
    
    [Space(10)]
    [SerializeField] private RectTransform _weatherButtonParant;
    [SerializeField] private GameObject _weatherButton;
    
    [Space(10)]
    [SerializeField] private RectTransform _actorButtonParant;
    [SerializeField] private GameObject _actorButton;
    
    public void Start()
    {
        LoadWorldsData();
        LoadWeatherData();
        LoadActorData();
    }

    private void LoadWorldsData()
    {
        for (var index = 0; index < DataController.Current.AppData.WorldModels.Length; index++)
        {
            var go = Instantiate(_worldButton, _worldButtonsParant);
            var worldButton = go.GetComponent<WorldButton>();

            worldButton.DataIndex = index;
            worldButton.Init();
        }
    }
    
    private void LoadWeatherData()
    {
        for (var index = 0; index < DataController.Current.AppData.WeatherModels.Length; index++)
        {
            var go = Instantiate(_weatherButton, _weatherButtonParant);
            var worldButton = go.GetComponent<WeatherButton>();

            worldButton.DataIndex = index;
            worldButton.Init();
        }
    }
    
    private void LoadActorData()
    {
        for (var index = 0; index < DataController.Current.AppData.ActorModels.Length; index++)
        {
            var go = Instantiate(_actorButton, _actorButtonParant);
            var worldButton = go.GetComponent<ActorButton>();

            worldButton.DataIndex = index;
            worldButton.Init();
        }
    }
}
