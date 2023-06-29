using UnityEngine;

public class MenuControler : UiControler
{
    [SerializeField] private GuiPointerListener galleryButton;
    [SerializeField] private GuiPointerListener coinRotationButton;
    [SerializeField] private GuiPointerListener racingClabButton;
    [SerializeField] private GuiPointerListener BackButton;

    private protected override void onAwake()
    {
        base.onAwake();

        galleryButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.GalleryScene));
            canvasGroup.blocksRaycasts = false;
        };

        coinRotationButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.CoinRotationScene));
            canvasGroup.blocksRaycasts = false;
        };

        racingClabButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.RacingClabScene));
            canvasGroup.blocksRaycasts = false;
        };

        BackButton.OnClick += data =>
        {
            Application.Quit();
        };
    }    
}
