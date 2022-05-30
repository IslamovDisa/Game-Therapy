using UnityEngine;

[CreateAssetMenu(fileName = "WeatherInfo", menuName = "GameTherapyData/WeatherInfo")]
public class WeatherInfo : WorldElementInfo
{
    public Texture2D[] Textures;
    public ParticleSystem _particles;
}