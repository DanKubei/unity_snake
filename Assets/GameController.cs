using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Game game;
    [SerializeField]
    private Button leftButton, rightButton, upButton, downButton;

    private void Start()
    {
        leftButton.onClick.AddListener(delegate { OnButtonClick(Snake.Directions.Left); });
        rightButton.onClick.AddListener(delegate { OnButtonClick(Snake.Directions.Right); });
        upButton.onClick.AddListener(delegate { OnButtonClick(Snake.Directions.Up); });
        downButton.onClick.AddListener(delegate { OnButtonClick(Snake.Directions.Down); });
    }

    private void OnButtonClick(Snake.Directions direction)
    {
        game.ChangeSnakeDirection(direction);
    }

}
