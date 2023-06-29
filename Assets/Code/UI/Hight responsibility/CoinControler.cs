using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class CoinControler : UiControler
{
    [SerializeField] private GuiPointerListener BackButton;
    [SerializeField] private GuiPointerListener StartButton;

    private bool isActiveRotation;
    private IEnumerator coroutine;

    public Rigidbody Coin;

    private protected override void onAwake()
    {
        base.onAwake();

        coroutine = Rotation();

        StartButton.OnClick += data =>
        {
            if (!isActiveRotation)
            {
                StartCoroutine(coroutine);
                isActiveRotation = true;
            }
            else
            {
                isActiveRotation = false;
                StopCoroutine(coroutine);
            }
        };

        BackButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.MenuScene));
            BackButton.enabled = false;
        };
    }
    
    private IEnumerator Rotation()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            Coin.AddTorque(new Vector3(0,20,0));
        }
    }
     

    
}
