using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrack : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    int dotsCount = 10;
    [SerializeField]
    float speed = 10;
    float gravity;

    void Start()
    {
        gravity = Physics2D.gravity.y;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lineRenderer.positionCount = dotsCount;

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mouseWorldPos - Vector3.zero;
            float angle = Mathf.Atan2(direction.y, direction.x); // радианы
                                                                 //float angleDeg = angle * Mathf.Rad2Deg;//градусы
            for (int i = 0; i < dotsCount; i++)
            {
                float t = i * .1f;
                float x = t * speed * Mathf.Cos(angle);
                float y = t * speed * Mathf.Sin(angle) + gravity * t * t / 2;
                Vector3 pos = new Vector3(x, y, 0);
                lineRenderer.SetPosition(i, pos);

            }
        }
    }
}
