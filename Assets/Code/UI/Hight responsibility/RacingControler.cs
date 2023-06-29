using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class RacingControler : UiControler
{
    [SerializeField] private GuiPointerListener leftArrow;
    [SerializeField] private GuiPointerListener rightArrow;
    [SerializeField] private GuiPointerListener GasButton;
    [SerializeField] private GuiPointerListener StopButton;
    [SerializeField] private GuiPointerListener BackButton;

    [SerializeField] private CarControler car;

    private bool isRotateLeft;
    private bool isRotateRight;
    private bool isMove;
    private bool isStop;
    
    private protected override void onAwake()
    {
        base.onAwake();

        SetControls();

        BackButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.MenuScene));
            BackButton.enabled = false;
        };
    }


    private void FixedUpdate()
    {
        if(isRotateLeft)
            car.GetRotation(-1);

        if (isRotateRight)
            car.GetRotation(1);

        if(!isRotateLeft && !isRotateRight)
            car.GetZeroRotation();

        if (isMove)
            car.GetMove();
        else car.GetStoped();

        if(isStop)
            car.GetStop();

        car.UpdateVisual();
    }

    private void SetControls()
    {
        leftArrow.OnDown += data =>
        {
            isRotateLeft = true;
        };

        rightArrow.OnDown += data =>
        {
            isRotateRight = true;
        };

        GasButton.OnDown += data =>
        {
            isMove = true;
        };

        StopButton.OnDown += data =>
        {
            isStop = true;
        };

        leftArrow.OnUp += data =>
        {
            isRotateLeft = false;
        };

        rightArrow.OnUp += data =>
        {
            isRotateRight = false;
        };

        GasButton.OnUp += data =>
        {
            isMove = false;
        };

        StopButton.OnUp += data =>
        {
            isStop = false;
        };

    }

}
