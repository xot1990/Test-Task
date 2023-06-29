using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ViewControler : UiControler
{
    [SerializeField] private Image ViewImage;
    [SerializeField] private GuiPointerListener BackButton;

    private protected override void onAwake()
    {
        base.onAwake();

        BackButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.GalleryScene));
            BackButton.enabled = false;
        };
    }

    private void Start()
    {
        SetImage();
    }
        
    private void SetImage()
    {
        ViewImage.sprite = ManagerScene.Get().ViewSprite;
    }

    private protected override void SetSceneOrientation()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
