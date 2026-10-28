
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float xLimit = 7.5f;   // adjust to match your walls

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");   // A/D or arrow keys
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + input * speed * Time.deltaTime, -xLimit, xLimit);
        transform.position = pos;
    }
}
