using UnityEngine;


public class Car : MonoBehaviour
{
    private Rigidbody rigidbodyCar;
    private float speedRotationCar = 2.0f;
    void Start()
    {
        rigidbodyCar = GetComponent<Rigidbody>();
    }
    void Update()
    {
        InputConroler.SwipeEvent += OnTraffic; 
    }
    void OnTraffic(float speed)
    {      
        Drive(speed);       
    }
    void Drive(float speedCar)
    {
        rigidbodyCar.velocity = transform.forward * speedCar;
    }
    public void OnRotationCar(float y)
    {
        transform.Rotate(new Vector3(0,y* Arrow.Instation.angleCar,0) * speedRotationCar * Time.deltaTime);
    }
}
