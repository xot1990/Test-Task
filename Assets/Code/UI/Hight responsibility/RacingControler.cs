using UnityEngine.UI;
using UnityEngine;


public class RacingControler : UiControler
{
    [SerializeField] private GuiPointerListener leftArrow;
    [SerializeField] private GuiPointerListener rightArrow;
    [SerializeField] private GuiPointerListener GasButton;
    [SerializeField] private GuiPointerListener StopButton;
    [SerializeField] private GuiPointerListener BackButton;
    [SerializeField] private GuiPointerListener MoveDirButton;
    [SerializeField] private GuiPointerListener ResetButton;

    [SerializeField] private GameObject cameraObj;

    [SerializeField] private Sprite moveForward;
    [SerializeField] private Sprite moveBackward;

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

        ResetButton.OnClick += data =>
        {
            ManagerScene.Get().ResetCurrentScene();
        };
    }

    private void Update()
    {
        cameraObj.transform.position = car.cameraPos.position;
        cameraObj.transform.rotation = car.cameraPos.rotation;
    }

    private protected override void SetSceneOrientation()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void SetControls()
    {
        MoveDirButton.OnClick += data =>
        {
            if (car.SwitchDirection() == 1)
                MoveDirButton.GetComponent<Image>().sprite = moveForward;
            else MoveDirButton.GetComponent<Image>().sprite = moveBackward;
        };

        leftArrow.OnDown += data =>
        {
            car.GetRotationLeft(true);
        };

        rightArrow.OnDown += data =>
        {
            car.GetRotationRight(true);
        };

        GasButton.OnDown += data =>
        {
            car.GetMove(true);
        };

        StopButton.OnDown += data =>
        {
            car.GetStop(true);
        };

        leftArrow.OnUp += data =>
        {
            car.GetRotationLeft(false);
        };

        rightArrow.OnUp += data =>
        {
            car.GetRotationRight(false);
        };

        GasButton.OnUp += data =>
        {
            car.GetMove(false);
        };

        StopButton.OnUp += data =>
        {
            car.GetStop(false);
        };

    }

}
