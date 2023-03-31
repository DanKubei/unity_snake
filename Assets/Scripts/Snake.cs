using System.Collections.Generic;
using UnityEngine;

public class Snake
{
    public Vector2 CurrentDirection { get { return direction; } }
    public List<Vector2> Parts { get { return partsPositions; } }
    public int Length { get { return partsPositions.Count; } }

    private Vector2 direction;
    private List<Vector2> partsPositions = new List<Vector2>();

    private readonly Vector2[] approvedDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    public Snake(Vector2 startPosition, int length)
    {
        direction = Vector2.up;
        for (int i = 0; i < length; i++)
        {
            partsPositions.Add(startPosition - direction * i);
        }
    }

    public void ChangeDirection(Vector2 direction)
    {
        if (direction == this.direction * -1)
        {
            return;
        }
        if (!approvedDirections.ToString().Contains(direction.ToString()))
        {
            return;
        }
        this.direction = direction;
    }

    public bool Move()
    {
        Vector2 lastPosition = partsPositions[0] + direction;
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
