using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = Random.Range(0.5f, 2f);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}