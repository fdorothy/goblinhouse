using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterFacing
{
    NORTH, EAST, SOUTH, WEST
}

public class CharacterFlipbook : MonoBehaviour
{
    public Sprite[] northIdle;
    public Sprite[] eastIdle;
    public Sprite[] southIdle;
    public Sprite[] westIdle;

    public Sprite[] northWalk;
    public Sprite[] eastWalk;
    public Sprite[] southWalk;
    public Sprite[] westWalk;

    private Flipbook flipbook;
    public CharacterFacing facing;
    public bool walking;

    // Start is called before the first frame update
    void Start()
    {
        flipbook = GetComponent<Flipbook>();
        flipbook.AddSequence("idle_NORTH", northIdle);
        flipbook.AddSequence("idle_EAST", eastIdle);
        flipbook.AddSequence("idle_SOUTH", southIdle);
        flipbook.AddSequence("idle_WEST", westIdle);

        flipbook.AddSequence("walk_NORTH", northWalk);
        flipbook.AddSequence("walk_EAST", eastWalk);
        flipbook.AddSequence("walk_SOUTH", southWalk);
        flipbook.AddSequence("walk_WEST", westWalk);
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            flipbook.PlaySequence("walk_" + facing.ToString(), false);
        } else
        {
            flipbook.PlaySequence("idle_" + facing.ToString(), false);
        }
    }

    public void CalculateFacing(Vector3 direction)
    {
        if (direction.magnitude < 0.001f)
            return;
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        float forwardComponent = Vector3.Dot(camForward.normalized, direction.normalized);
        float rightComponent = Vector3.Dot(camRight.normalized, direction.normalized);

        if (Mathf.Abs(forwardComponent) > Mathf.Abs(rightComponent))
        {
            // going forward mostly, so set facing to that direction
            if (forwardComponent > 0.0f)
                facing = CharacterFacing.NORTH;
            else
                facing = CharacterFacing.SOUTH;
        } else
        {
            // going forward mostly, so set facing to that direction
            if (rightComponent > 0.0f)
                facing = CharacterFacing.EAST;
            else
                facing = CharacterFacing.WEST;
        }
    }
}
