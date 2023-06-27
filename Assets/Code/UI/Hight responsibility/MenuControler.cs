using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControler : UiControler
{
    [SerializeField] private GuiPointerListener galleryButton;

    private protected override void onAwake()
    {
        base.onAwake();

        galleryButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.GalleryScene));
            galleryButton.enabled = false;
        };
    }    
}
