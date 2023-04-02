using System.Collections.Generic;
using UnityEngine;

public class Snake
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public Vector2 CurrentDirection { get { return direction; } }
    public List<Vector2> Parts { get { return partsPositions; } }
    public int Length { get { return partsPositions.Count; } }

    private Vector2 direction;
    private List<Vector2> partsPositions = new List<Vector2>();

    private readonly Vector2[] approvedDirections = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

    public Snake(Vector2 startPosition, int length, Directions direction = Directions.Up)
    {
        ChangeDirection(direction);
        for (int i = 0; i < length; i++)
        {
            partsPositions.Add(startPosition + this.direction * i);
        }
    }

    public void ChangeDirection(Directions direction)
    {
        if (approvedDirections[(int)direction] == this.direction * -1)
        {
            return;
        }
        this.direction = approvedDirections[(int)direction];
    }

    public bool Move()
    {
        Vector2 lastPosition = partsPositions[0] - direction;
        if (partsPositions.Contains(lastPosition))
        {
            return false;
        }
        for (int i = 0; i < partsPositions.Count; i++)
        {
            Vector2 position = partsPositions[i];
            partsPositions[i] = lastPosition;
            lastPosition = position;
        }
        return true;
    }

    public void AddPart()
    {
        foreach (Vector2 direction in approvedDirections)
        {
            Vector2 position = partsPositions[partsPositions.Count] + direction;
            if (!partsPositions.Contains(position))
            {
                partsPositions.Add(position);
                break;
            }
        }
    }
}
