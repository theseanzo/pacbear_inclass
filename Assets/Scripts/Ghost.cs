using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : BaseUnit
{
    // Start is called before the first frame update
    Eyes eyes;
    SkinnedMeshRenderer body;
    Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };
    private Stack<Vector2Int> path = new Stack<Vector2Int>();
    private PacBear bear;
    private SpawnLocation spawnLocation;
    public bool isReturnToSpawn;
    // Update is called once per frame
    protected override void Start()
    {
        base.Start(); //this initializes our start in the base class
        eyes = this.GetComponentInChildren<Eyes>();
        eyes.gameObject.SetActive(false);
        body = this.GetComponentInChildren<SkinnedMeshRenderer>();
        bear = FindObjectOfType<PacBear>();
        spawnLocation = FindObjectOfType<SpawnLocation>();
    }
    public void OnEnable()
    {
        PacBear.onSpecialModeSwitch += PacBear_onSpecialModeSwitch;
    }
    public void OnDisable()
    {
        PacBear.onSpecialModeSwitch -= PacBear_onSpecialModeSwitch;
    }

    private void PacBear_onSpecialModeSwitch(bool obj)
    {
        GetComponentInChildren<Renderer>().material.SetFloat("_IsGhost", obj ? 1 : 0);
    }
    public void Death()
    {
        isReturnToSpawn = true;
        body.gameObject.SetActive(false);
        eyes.gameObject.SetActive(true);
        
    }
    void Update()
    {
        if(moveTimer >= 1f)
        {
            if (isReturnToSpawn)
                GoHome();
            else
                GoToPacBear();
            //Wander();
        }
        Move();
    }
    void GoToPacBear()
    {
        speed = 0.5f;
        path = PathFinder.instance.GetPath(nextPosInGrid, bear.posInGrid);
        if(path.Count != 0)
        {
            if(path.Count > 20)
            {
                Wander();
            }
            else
            {
                Vector2Int nextDestination = path.Pop();
                direction = nextDestination - nextPosInGrid;
            }

        }
    }
    void GoHome()
    {
        speed = 5.0f;
        path = PathFinder.instance.GetPath(nextPosInGrid, spawnLocation.posInGrid);
        if(path.Count != 0)
        {
            Vector2Int nextDestination = path.Pop();
            direction = nextDestination - nextPosInGrid;
        }
        if(path.Count == 0)
        {
            isReturnToSpawn = false;
            body.gameObject.SetActive(true); //show the body because we are no longer returning to spawn
            eyes.gameObject.SetActive(false);
        }
        
    }
    void Wander()
    {
        //We create a list to store the directions we can move in
        List<Vector2Int> options = new List<Vector2Int>();
        foreach (Vector2Int dir in directions)
        {
            //0 = pill and 1 = wall
            //use current position where are are as nextPosInGrid + each direction to determine points around us
            int adjacentObjInGrid = GameManager.instance.grid[nextPosInGrid.x + dir.x, nextPosInGrid.y + dir.y];
            //if object is not a wall, and if direct is not the opposite of current direection (shouldn't go forward to backward)
            if (adjacentObjInGrid != 1 && dir != -direction)
            {
                options.Add(dir);
            }
        }
        if (options.Count == 0)
        {
            direction = -direction;
        }
        else
        {
            direction = options[Random.Range(0, options.Count)];
        }
    }


}
