using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Current;

    private void Awake()
    {
        Current = this;
    }

    public void Start()
    {
        EventManager.Instance.AddListener<SceneLoadEvent>(OnSceneLoadEvent);
    }

    private void OnSceneLoadEvent(SceneLoadEvent info)
    {
        SceneManager.LoadScene(info.SceneName);
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
