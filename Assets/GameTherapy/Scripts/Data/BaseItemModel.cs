using System;

[Serializable]
public struct BaseItemModel
{
    public string Name;
    public string ThumbnailPath;
}

[Serializable]
public struct ActorModel
{
    public string Name;
    public string ThumbnailPath;

    public string PrefabPath;
    public bool Like;
    public bool Added;
    public bool New;
}

[Serializable]
public struct WorldModel
{
    public string Name;
    public string ThumbnailPath;

    public string SceneName;
    public bool Like;
    public bool New;
}

[Serializable]
public class WeatherModel
{
    public string Name;
    public string ThumbnailPath;

    public string[] TexturesPath;
    public string[] ParticlesPrefabPath;
}

[Serializable]
public class AppData
{
    public WorldModel[] WorldModels;
    public WeatherModel[] WeatherModels;
    public ActorModel[] ActorModels;
}




