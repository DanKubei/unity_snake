using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int mapWidth, mapHeight, startSnakeLength, ticksInSecond;
    [SerializeField]
    private Vector2[] walls;

    private Map map;
    private Snake snake;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        map = new Map(mapWidth, mapHeight, walls);
        snake = new Snake(new Vector2(mapWidth / 2, mapHeight / 2), startSnakeLength);
        StartCoroutine(TickUpdate(ticksInSecond));
    }

    private IEnumerator TickUpdate(int ticksInSecond)
    {
        while (true)
        {
            snake.Move();
            Vector2 snakeHead = snake.Parts[0];
            if (snakeHead.x < 0 || snakeHead.x >= map.Width || snakeHead.y < 0 || snakeHead.y >= map.Height)
            {
                GameOver();
                break;
            }
            if (snake.Parts.Contains(map.CoinPostion))
            {
                snake.AddPart();
                map.GenerateCoin(snake.Parts.ToArray());
            }
            yield return new WaitForSeconds(1 / ticksInSecond);
        }
    }

    private void GameOver()
    {
        
    }
}
