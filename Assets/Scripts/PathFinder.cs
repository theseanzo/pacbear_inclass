using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile
{
    public bool isWalkable;
    public Vector2Int pos;
    public Tile previousTile;
}

public class PathFinder : Singleton<PathFinder>
{
    private Tile[,] tiles;
    private Queue<Tile> frontier;
    private List<Tile> surroundTiles;
    public void SetGrid(int[,] grid)
    {
        tiles = new Tile[grid.GetLength(0), grid.GetLength(1)];
        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for(int j = 0; j < tiles.GetLength(1); j++)
            {
                Tile t = new Tile();
                t.pos = new Vector2Int(i, j);
                t.isWalkable = grid[i, j] != 1; //1 is a wall. we do not walk on walls, or on water, but we do walk on sunshine
                tiles[i, j] = t;
            }
        }
    }
    public Stack<Vector2Int> GetPath(Vector2Int startPos, Vector2Int destination)
    {
        //clear out all of our previous tiles
        foreach(Tile t in tiles)
        {
            t.previousTile = null;
        }

        Stack<Vector2Int> path = new Stack<Vector2Int>();//this is what we will eventually return
        Tile startTile = tiles[startPos.x, startPos.y];
        frontier = new Queue<Tile>();
        frontier.Enqueue(startTile);
        while (frontier.Count != 0) //if we ever make it to 0 in our queue, we know there is no path to our destination
        {
            Tile current = frontier.Dequeue();
            if(current.pos == destination)
            {
                while(current != startTile && current != null) 
                {
                    path.Push(current.pos);
                    current = current.previousTile;
                }
                return path;
            }
            AddSurroundingTiles(current);
        }
        return path;//we return with nothing
    }
    private void AddSurroundingTiles(Tile origin)
    {
        AddTile(origin, 1, 0);
        AddTile(origin, 0, 1);
        AddTile(origin, 0, -1);
        AddTile(origin, -1, 0);
    }
    private void AddTile(Tile origin, int dirX, int dirY)
    {
        int tileX = origin.pos.x + dirX;
        int tileY = origin.pos.y + dirY;
        if (tileX < 0 || tileX >= tiles.GetLength(0) || tileY < 0 || tileY >= tiles.GetLength(1))
            return; //if it is outside the boundaries return
        Tile nextTile = tiles[tileX, tileY];
        if (nextTile.previousTile != null || !nextTile.isWalkable)
            return;//if you have previously searched that tile, or it's not walkable, return
        nextTile.previousTile = origin;
        frontier.Enqueue(nextTile);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
