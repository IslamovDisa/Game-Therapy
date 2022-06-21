using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Current;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Text _loadingText;
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
        StartCoroutine(StartLoad(info.SceneName));
        //SceneManager.LoadScene(info.SceneName);
    }

    public void SceneLoad(string sceneName)
    {
        // SceneManager.LoadScene(sceneName);
        StartCoroutine(StartLoad(sceneName));
    }
    
    private IEnumerator StartLoad(string sceneName)
    {
        _loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));
        
        var operation = SceneManager.LoadSceneAsync(sceneName);
        
        while (!operation.isDone)
        {
            _loadingText.text = "Loading " + (operation.progress * 100) + "%";
            yield return null;
        }
        
        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        
        _loadingScreen.SetActive(false);
    }
    
    private  IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        var startValue = _canvasGroup.alpha;
        float time = 0;
        
        while (time < duration)
        {
            _canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        _canvasGroup.alpha = targetValue;
    }
}
