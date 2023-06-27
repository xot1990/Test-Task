using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManagerScene : MonoBehaviourService<ManagerScene>
{
    public enum Scenes
    {
        LoadScene,
        MenuScene,
        ViewScene,
        GalleryScene
    }

    private Scenes NextLoadScene;

    private AsyncOperation asyncOperation;


    protected override void OnCreateService()
    {
        EventBus.finishLoading += NextSceneLoad;
        EventBus.startLoading += SceneLoad;

        DontDestroyOnLoad(gameObject);
        NextLoadScene = Scenes.MenuScene;
    }

    protected override void OnDestroyService()
    {
        EventBus.finishLoading -= NextSceneLoad;
        EventBus.startLoading -= SceneLoad;
    }
   
    private void NextSceneLoad()
    {
        Debug.Log(NextLoadScene.ToString());
        SceneManager.LoadSceneAsync(NextLoadScene.ToString());
    }

    private void SceneLoad(Scenes scenes)
    {
        NextLoadScene = scenes;
        Debug.Log("2");
        SceneManager.LoadSceneAsync(Scenes.LoadScene.ToString());
    }
    

    
}
