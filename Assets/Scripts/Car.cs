using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGain = 0.2f;


    void Update()
    {
        speed += speedGain * Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
