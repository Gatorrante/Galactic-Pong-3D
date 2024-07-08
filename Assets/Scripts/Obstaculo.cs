using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public enum Direction { R, L }
    public Direction movementDirection = Direction.R;

    public float speed = 2f;
    private float zMin = -5.20f;
    private float zMax = 5.20f;
    private bool movingForward = true;

    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        if (movementDirection == Direction.R)
        {
            if (movingForward)
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                if (transform.position.z >= zMax)
                    movingForward = false;
            }
            else
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                if (transform.position.z <= zMin)
                    movingForward = true;
            }
        }
        else if (movementDirection == Direction.L)
        {
            if (movingForward)
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                if (transform.position.z <= zMin)
                    movingForward = false;
            }
            else
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                if (transform.position.z >= zMax)
                    movingForward = true;
            }
        }
    }
}
