using UnityEngine;

class FollowCamera : MonoBehaviour
{
    public Player player = null;

    protected float distance = 100.0f;
    protected float rotateSpeed = 100.0f;
    protected Camera cam;

    public void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void Update()
    {
        if (player == null)
        {
            return;
        }
        transform.position = player.transform.position - transform.forward * distance;

        float s = cam.orthographicSize;
        s += Input.mouseScrollDelta.y;

        if (s < 10.0f)
            s = 10.0f;
        else if (s > 50.0f)
            s = 50.0f;
        cam.orthographicSize = s;

        float delta = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            delta = rotateSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            delta = -rotateSpeed;
        }
        transform.RotateAround(player.transform.position, Vector3.up, delta * Time.deltaTime);
    }
}
