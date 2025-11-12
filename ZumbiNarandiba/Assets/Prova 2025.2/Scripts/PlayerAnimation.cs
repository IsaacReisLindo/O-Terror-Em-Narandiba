using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public string[] idleDirections = { "N Idle", "NO Idle", "O Idle", "SO Idle", "S Idle", "SE Idle", "L Idle", "NE Idle" };
    public string[] walkDirections = { "N Walk", "NO Walk", "O Walk", "SO Walk", "S Walk", "SE Walk", "L Walk", "NE Walk" };
    int lastDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();


    }

    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;

        if (_direction.magnitude < 0.1f)
        {
            directionArray = idleDirections;
        }
        else
        {
            directionArray = walkDirections;
            lastDirection = DirectionToIndex(_direction);
        }

        animator.Play(directionArray[lastDirection]);
    }


    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;

        float step = 360f / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);

    }
}