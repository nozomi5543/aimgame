using UnityEngine;

public class CrosshairMove:MonoBehaviour
{
    public float speed = 300f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x,y,0) * speed * Time.deltaTime);
    }
}
