using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class CoinControler : UiControler
{
    [SerializeField] private Image ViewImage;
    [SerializeField] private GuiPointerListener BackButton;
    [SerializeField] private GuiPointerListener StartButton;

    public GameObject Coin;

    private protected override void onAwake()
    {
        base.onAwake();

        BackButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.MenuScene));
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

    
}
