using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : BaseObject
{
    public float speed;

    protected Vector2Int direction;

    protected Vector2Int nextPosInGrid;

    private Vector3 posInWorld;
    private Vector3 nextPosInWorld;

    protected float moveTimer = Mathf.Infinity;

    private Vector2Int prevDirection;

    // Use this for initialization
    protected virtual void Start()
    {
        //We assign nextPosInGrid, because when moveTimer >=1, the posInGrid gets set to that
        nextPosInGrid = posInGrid;
    }

    protected void Move()
    {
        
        if (moveTimer >= 1f)
        {
            posInGrid = nextPosInGrid;
            //We calculate the next position, based on the current position and direction
            Vector2Int checkPos = posInGrid + direction;

            //If it's moving into a wall, don't change direction
            if (GameManager.instance.grid[checkPos.x, checkPos.y] == 1)
            {
                direction = prevDirection;
            }

            nextPosInGrid = posInGrid + direction;

            //If it's still moving into a wall, just stop moving
            if (GameManager.instance.grid[nextPosInGrid.x, nextPosInGrid.y] == 1)
            {
                nextPosInGrid = posInGrid;
            }

            //We convert the IntVector2, to a Vector3 to place our object in 3D space
            posInWorld = new Vector3(posInGrid.x, 0, posInGrid.y);
            nextPosInWorld = new Vector3(nextPosInGrid.x, 0, nextPosInGrid.y);

            prevDirection = direction;
            moveTimer = 0;

            if (direction.x == -1)
                transform.localEulerAngles = new Vector3(0, -90, 0);
            if (direction.x == 1)
                transform.localEulerAngles = new Vector3(0, 90, 0);
            if (direction.y == -1)
                transform.localEulerAngles = new Vector3(0, 180, 0);
            if (direction.y == 1)
                transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        //We increment the moveTimer by the duration of the last frame
        moveTimer += Time.deltaTime * speed;
        transform.localPosition = Vector3.Lerp(posInWorld, nextPosInWorld, moveTimer);
    }
}
