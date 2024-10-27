using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGain = 0.2f;
    [SerializeField] private float turnSpeed = 200f;

    private int steer;

    void Update()
    {
        speed += speedGain * Time.deltaTime;

        transform.Rotate(0f, steer * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    public void Steer(int value)
    {
        steer = value;
    }
}
