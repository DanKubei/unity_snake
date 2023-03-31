using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Width { get { return width; } }
    public int Height { get { return height; } }
    public List<Vector2> Walls { get { return walls; } }
    public Vector2 CoinPostion { get { return coin; } }

    private int width, height;
    private List<Vector2> walls = new List<Vector2>();
    private Vector2 coin;

    public Map(int width, int height, Vector2[] walls = null)
    {
        if (width < 0 || height < 0)
        {
            throw new System.ArgumentOutOfRangeException();
        }
        this.width = width;
        this.height = height;
        foreach (Vector2 wall in walls)
        {
            if (wall.x >= width || wall.y >= height || wall.x < 0 || wall.y < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            this.walls.Add(wall);
        }
    }

    public void GenerateCoin(Vector2[] cancelZone)
    {
        List<Vector2> fullCancelZone = new List<Vector2>();
        fullCancelZone.AddRange(cancelZone);
        fullCancelZone.AddRange(walls);
        while (true)
        {
            Vector2 position = new Vector2(Random.Range(0, width - 1), Random.Range(0, height - 1));
            if (fullCancelZone.Contains(position))
            {
                continue;
            }
            coin = position;
            break;
        }
    }
}
