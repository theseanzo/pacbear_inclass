using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public BaseObject[] objectPrefabs;
    public List<SpawnLocation> spawnLocations;
    //this is a jagged array. Which means an array of arrays
    //The difference between a jagged array and a 2D array, is that a jagged array can have multiple sub-arrays with different lengths
    //public int[][] jaggedGrid;
    private int _totalPills;
    public int TotalPills //the total number of pills left
    {
        get; private set;
    }
    public int NumPillsLeft
    {
        get
        {
            return _numPillsLeft;
        }
        set
        {
            _numPillsLeft = value;
            if(_numPillsLeft == 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    private int _numPillsLeft = 0;
    //This is a 2D array
    //0 = pill, 1 = wall, 2 = ghost, 3 = macman, 4 = specialPill
    //5 = spawn
    public int[,] grid = new int[,]
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1 },
        { 1, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 1, 1, 3, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1 },
        { 1, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 1, 1, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 2, 0, 1, 5, 5, 5, 0, 2, 0, 0, 0, 0, 1, 1, 2, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 5, 5, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1 },
        { 1, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };
    // Start is called before the first frame update
    
    void Awake()
    {
        int gridSizeX = grid.GetLength(0);
        int gridSizeY = grid.GetLength(1);

        for(int i = 0; i < gridSizeX; i++)
        {
            for(int j = 0; j < gridSizeY; j++)
            {
                int gridValue = grid[i, j];
                if (gridValue == 0)
                    NumPillsLeft++;
                BaseObject objectClone = Instantiate(objectPrefabs[gridValue]);
                objectClone.transform.localPosition = new Vector3(i, 0, j);
                objectClone.posInGrid = new Vector2Int(i, j);
                if(gridValue == 5)
                {
                    spawnLocations.Add((SpawnLocation)objectClone);
                }
            }
        }
        TotalPills = NumPillsLeft;
        PathFinder.instance.SetGrid(grid); //add this line to your GameManager, because the PathFinder needs a grid for its searching
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
