public class SceneLoadEvent : GameEvent
{
    public string SceneName;

    public SceneLoadEvent(string sceneName)
    {
        SceneName = sceneName;
    }
}