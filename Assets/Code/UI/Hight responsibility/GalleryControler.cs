using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GalleryControler : UiControler
{
    [SerializeField] private GridLayoutGroup layoutGroup;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Image backSize;

    [SerializeField] private int visibleColumpCount;
    [SerializeField] private int visibleRowsCount;
    [SerializeField] private GameObject galleryCell;
    [SerializeField] private List<Cell> CellPoolList;
    [SerializeField] private int cellCount;
    [SerializeField] private string galleryURL;

    private RectTransform content;

    Vector3[] corners = new Vector3[4];
    Vector3[] backSizeCorners = new Vector3[4];
    private float rectSide;

    [SerializeField] private Cell lowCell;
    [SerializeField] private Cell UpperCell;

    private int upIndex;
    private int lowIndex;

    private protected override void onAwake()
    {
        base.onAwake();
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
        }
    }

    private void Start()
    {
        upIndex = 1;

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
        StartCoroutine(PoolChecker());
    }
        

    private IEnumerator HideCell()
    {        
        for (int i = CellPoolList.Count - 1; i >= 0; i--)
        {
            if (CellPoolList[i].LeftMin().y < (backSizeCorners[0].y - CellPoolList[i].GetRealSize().y*3))
            {
                CellPoolList[i].OffLine();
            }
            else
            {
                CellPoolList[i].OnLine(galleryURL);
            }
        }

        yield return new WaitForSeconds(1);

        Debug.Log(backSizeCorners[0].y);
        foreach (var C in CellPoolList)
        {
            if(C.LeftMin().y < backSizeCorners[0].y)
            {                
                if (lowIndex > C.id)
                    lowIndex = C.id;
            }
        }

        lowCell = CellPoolList.Find(X => X.id == lowIndex);
        UpperCell = CellPoolList.Find(X => X.id == upIndex);
    }

    private IEnumerator PoolChecker()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (lowCell != null && UpperCell != null && lowIndex < cellCount - visibleColumpCount && upIndex > 0)
            {
                if (lowCell.RightMax().y > backSizeCorners[0].y + 0.5f)
                {
                    for (int i = 0; i < visibleColumpCount*2; i++)
                    {
                        CellPoolList.Find(X => X.id == lowIndex + visibleColumpCount + i).OnLine(galleryURL);
                    }

                    for (int i = 0; i < visibleColumpCount; i++)
                    {
                        CellPoolList.Find(X => X.id == upIndex + i).OffLine();
                    }

                    upIndex += visibleColumpCount;

                    lowIndex += visibleColumpCount;
                    

                    lowCell = CellPoolList.Find(X => X.id == lowIndex);
                    UpperCell = CellPoolList.Find(X => X.id == upIndex);
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        
    }


}
