using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform cellPrefab, gridPrefab, rowPrefab;

    private RectTransform gridTransform;
    private Vector2 imageSize;

    private void Start()
    {
        imageSize = GetComponent<RectTransform>().rect.size;
        print(imageSize);
    }

    public void CreateGrid(int width, int height)
    {
        if (gridTransform != null)
        {
            Destroy(gridTransform.gameObject);
        }
        gridTransform = Instantiate(gridPrefab, transform);
        float cellSize = 0;
        int rowPadding = 0;
        if (width == height || width > height)
        {
            cellSize = imageSize.x / width;
            int padding = (int)(imageSize.y - cellSize * height);
            gridTransform.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, padding / 2, padding / 2);
        }
        else
        {
            cellSize = imageSize.y / height;
            rowPadding = (int)(imageSize.x - cellSize * width);
        }
        for (int row = 0; row < height; row++)
        {
            RectTransform rowTransform = Instantiate(rowPrefab, gridTransform);
            rowTransform.sizeDelta = new Vector2(rowTransform.sizeDelta.x, cellSize);
            rowTransform.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(rowPadding / 2, rowPadding / 2, 0, 0);
            for (int cell = 0; cell < width; cell++)
            {
                RectTransform cellTransform = Instantiate(cellPrefab, rowTransform);
                cellTransform.sizeDelta = new Vector2(cellSize, cellSize);
            }
        }
    }
}
