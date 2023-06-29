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
        GalleryScene,
        CoinRotationScene,
        RacingClabScene
    }

    private Scenes NextLoadScene;
    private int BackScene;
    

    private AsyncOperation asyncOperation;

    public Sprite ViewSprite;

    private void Start()
    {
        SceneManager.LoadScene(Scenes.LoadScene.ToString());
    }

    private void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKey(KeyCode.Escape))
        {
            BackSceneLoad();
        }
#endif
    }

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

    private void BackSceneLoad()
    {
        NextLoadScene = GetBackScene();
        SceneManager.LoadSceneAsync(Scenes.LoadScene.ToString());
        
    }
   
    private void NextSceneLoad()
    {
        SceneManager.LoadSceneAsync(NextLoadScene.ToString());
    }

    private void SceneLoad(Scenes scenes)
    {
        NextLoadScene = scenes;
        SceneManager.LoadSceneAsync(Scenes.LoadScene.ToString());
        BackScene = SceneManager.GetActiveScene().buildIndex;
    }
    
    private Scenes GetBackScene()
    {
        switch(BackScene)
        {
            case 2:
                return Scenes.MenuScene;
            case 3:
                return Scenes.GalleryScene;
            default:
                return Scenes.MenuScene;                         
        }
    }
    
}
