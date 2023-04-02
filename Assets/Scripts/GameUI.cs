using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform cellPrefab, gridPrefab, rowPrefab, snakePrefab, snakeTransform, coinPrefab;
    [SerializeField]
    private Sprite snakeHeadSprite, snakeBodySprite, snakeTailSprite;

    private RectTransform gridTransform, coinTransform;
    private List<RectTransform> snakeParts = new List<RectTransform>();
    private Vector2 imageSize, imagePosition;
    private float cellSize;

    private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        imageSize = rect.rect.size;;
        imagePosition = new Vector2(rect.rect.xMin, rect.rect.yMax);
    }

    public void CreateGrid(int width, int height)
    {
        width += 1;
        height += 1;
        if (gridTransform != null)
        {
            Destroy(gridTransform.gameObject);
        }
        gridTransform = Instantiate(gridPrefab, transform);
        cellSize = 0;
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

    public void MoveSnake(Snake snake)
    {
        for (int i = 0; i < snake.Length; i++)
        {
            RectTransform snakePart = snakeParts[i];
            snakePart.anchoredPosition = imagePosition +
                new Vector2(cellSize * (snake.Parts[i].x + 1), -cellSize * (snake.Parts[i].y + 1));
            if (i == 0)
            {
                float rotation = 0;
                if (snake.CurrentDirection == Vector2.left)
                {
                    rotation = -90;
                }
                if (snake.CurrentDirection == Vector2.down)
                {
                    rotation = -180;
                }
                if (snake.CurrentDirection == Vector2.right)
                {
                    rotation = 90;
                }
                snakePart.rotation = Quaternion.Euler(0, 0, rotation);
            }
            if (i == snake.Length - 1)
            {
                float rotation = 0;
                Vector2 direction = snake.Parts[i - 1] - snake.Parts[i];
                if (direction == Vector2.left)
                {
                    rotation = 90;
                }
                if (direction == Vector2.up)
                {
                    rotation = -180;
                }
                if (direction == Vector2.right)
                {
                    rotation = -90;
                }
                snakePart.rotation = Quaternion.Euler(0, 0, rotation);
            }
        }
    }

    public void CreateSnake(Snake snake)
    {
        for (int i = 0; i < snakeParts.Count; i++)
        {
            Destroy(snakeParts[i]);
        }
        snakeParts.Clear();
        for (int i = 0; i < snake.Length; i++)
        {
            RectTransform snakePart = Instantiate(snakePrefab, snakeTransform);
            snakePart.sizeDelta = new Vector2(cellSize, cellSize);
            snakePart.anchoredPosition = imagePosition +
                new Vector2(cellSize * (snake.Parts[i].x + 1), -cellSize * (snake.Parts[i].y + 1));
            if (i == 0)
            {
                float rotation = 0;
                if (snake.CurrentDirection == Vector2.left)
                {
                    rotation = -90;
                }
                if (snake.CurrentDirection == Vector2.down)
                {
                    rotation = -180;
                }
                if (snake.CurrentDirection == Vector2.right)
                {
                    rotation = 90;
                }
                snakePart.rotation = Quaternion.Euler(0,0,rotation);
                snakePart.GetComponent<Image>().sprite = snakeHeadSprite;
            }
            if (i == snake.Length - 1)
            {
                float rotation = 0;
                Vector2 direction = snake.Parts[i - 1] - snake.Parts[i];
                if (direction == Vector2.left)
                {
                    rotation = 90;
                }
                if (direction == Vector2.up)
                {
                    rotation = -180;
                }
                if (direction == Vector2.right)
                {
                    rotation = -90;
                }
                snakePart.rotation = Quaternion.Euler(0, 0, rotation);
                snakePart.GetComponent<Image>().sprite = snakeTailSprite;
            }
            snakeParts.Add(snakePart);
        }
    }

    public void AddSnakePart(Snake snake)
    {
        int part = snakeParts.Count;
        RectTransform snakePart = Instantiate(snakePrefab, snakeTransform);
        snakePart.sizeDelta = new Vector2(cellSize, cellSize);
        snakePart.anchoredPosition = imagePosition +
        new Vector2(cellSize * (snake.Parts[part].x + 1), -cellSize * (snake.Parts[part].y + 1));
        float rotation = 0;
        Vector2 direction = snake.Parts[part - 1] - snake.Parts[part];
        if (direction == Vector2.left)
        {
            rotation = 90;
        }
        if (direction == Vector2.up)
        {
            rotation = -180;
        }
        if (direction == Vector2.right)
        {
            rotation = -90;
        }
        snakePart.rotation = Quaternion.Euler(0, 0, rotation);
        snakePart.GetComponent<Image>().sprite = snakeTailSprite;
        snakeParts.Add(snakePart);
        snakeParts[part - 1].GetComponent<Image>().sprite = snakeBodySprite;
    }

    public void CreateCoin(Vector2 position)
    {
        coinTransform = Instantiate(coinPrefab, snakeTransform);
        MoveCoin(position);
    }

    public void MoveCoin(Vector2 position)
    {
        coinTransform.sizeDelta = new Vector2(cellSize, cellSize);
        coinTransform.anchoredPosition = imagePosition +
        new Vector2(cellSize * (position.x + 1), -cellSize * (position.y + 1));
    }
}
