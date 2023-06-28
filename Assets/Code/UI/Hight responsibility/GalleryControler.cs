using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GalleryControler : UiControler
{
    [SerializeField] private GridLayoutGroup layoutGroup;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Image backSize;
    [SerializeField] private GameObject galleryCell;

    [SerializeField] private int visibleColumpCount;
    [SerializeField] private int visibleRowsCount;
    [SerializeField] private int cellCount;
    [SerializeField] private string galleryURL;
    
    [SerializeField] private GuiPointerListener BackButton;

    private List<Cell> CellPoolList = new List<Cell>();
    private RectTransform content;

    private Vector3[] backSizeCorners = new Vector3[4];
    private float rectSide;

    private Cell lowCell;
    private int lowIndex;

    private float tick = 0.1f;

    private protected override void onAwake()
    {
        base.onAwake();

        BackButton.OnClick += data =>
        {
            StartCoroutine(StartTransition(ManagerScene.Scenes.MenuScene));
            BackButton.enabled = false;
        };

        lowIndex = 99;
        content = layoutGroup.GetComponent<RectTransform>();

        layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layoutGroup.constraintCount = visibleColumpCount;

        for (int i = 0; i < cellCount; i++)
        {
            GameObject C = Instantiate(galleryCell,content.transform);
            Cell cell = C.GetComponent<Cell>();
            cell.id = i+1;
            CellPoolList.Add(cell);

            cell.listener.OnClick += data =>
            {
                StartCoroutine(StartTransition(ManagerScene.Scenes.ViewScene));
                ManagerScene.Get().ViewSprite = cell.GetSprite();
            };
        }
    }

    private void Start()
    {
        backSize.rectTransform.GetWorldCorners(backSizeCorners);

        rectSide = 0;

        float width = backSize.rectTransform.rect.width / visibleColumpCount - layoutGroup.spacing.x * 3;
        float hight = (backSize.rectTransform.rect.height - layoutGroup.spacing.y * (visibleRowsCount + 1)) / visibleRowsCount ;

        if (hight > width)
            rectSide = width;
        else rectSide = hight;

        layoutGroup.cellSize = new Vector2(rectSide, rectSide);
        scrollRect.verticalScrollbar.value = 1;

        StartCoroutine(HideCell());
    }
        

    private IEnumerator HideCell()
    {        
        for (int i = CellPoolList.Count - 1; i >= 0; i--)
        {
            if (CellPoolList[i].LeftMin().y < (backSizeCorners[0].y - CellPoolList[i].GetRealSize().y))
            {
                CellPoolList[i].OffLine();
            }
            else
            {
                CellPoolList[i].OnLine(galleryURL);
            }
        }

        yield return new WaitForSeconds(0.1f);

        foreach (var C in CellPoolList)
        {
            if(C.LeftMin().y < backSizeCorners[0].y)
            {                
                if (lowIndex > C.id)
                    lowIndex = C.id;
            }
        }

        lowCell = CellPoolList.Find(X => X.id == lowIndex);
    }

    
    private void Update()
    {
        tick -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (lowCell != null && lowIndex < cellCount - visibleColumpCount && tick < 0)
        {
            if (lowCell.LeftMin().y > backSizeCorners[0].y)
            {
                for (int i = 0; i < visibleColumpCount; i++)
                {
                    CellPoolList.Find(X => X.id == lowIndex + visibleColumpCount + i).OnLine(galleryURL);
                }

                lowIndex += visibleColumpCount;
                lowCell = CellPoolList.Find(X => X.id == lowIndex);

                tick = 0.1f;
            }
        }
    }


}
