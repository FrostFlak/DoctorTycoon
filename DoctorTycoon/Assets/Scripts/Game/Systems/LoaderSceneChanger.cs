using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Loader = 0 ,
    Game = 1
}
public class LoaderSceneChanger : MonoBehaviour
{
    private float _waitForLoadSceneTime = 2.5f;
    private AsyncOperation _loadOperation;

    public AsyncOperation LoadOperation {  get { return _loadOperation; } } 

    private void Start()
    {
        StartCoroutine(LoadMainScene());
    }
    private IEnumerator LoadMainScene()
    {
        _loadOperation = SceneManager.LoadSceneAsync((int)Scenes.Game);
        _loadOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(_waitForLoadSceneTime);
        _loadOperation.allowSceneActivation = true;
    }
}
