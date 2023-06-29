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
        RacingClabScene,
        Quit
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

        if (GetBackScene() == Scenes.Quit)
        {
            Application.Quit();
            return;
        }

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
    }
    
    private Scenes GetBackScene()
    {
        BackScene = SceneManager.GetActiveScene().buildIndex;

        switch (BackScene)
        {
            case 1:
                return Scenes.Quit;
            case 2:
                return Scenes.MenuScene;
            case 3:
                return Scenes.GalleryScene;
            default:
                return Scenes.MenuScene;                         
        }
    }

    public void ResetCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
}
