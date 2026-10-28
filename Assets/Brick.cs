using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 100;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<BallController>() != null)
        {
            GameManager.Instance.BrickDestroyed(points);
            Destroy(gameObject);
        }
    }
}
