using System.Collections;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]private Rigidbody _rigidbodyCar;
    private Vector3 transformCar;  
    void  OnTriggerStay(Collider collision)
    {      
        transformCar = _rigidbodyCar.transform.position;           
    }
    void  OnTriggerExit(Collider collision)
    {          
        StartCoroutine(WaitForReturng());            
    }
    IEnumerator WaitForReturng()
    {       
        yield return new WaitForSeconds(1.10f);
        ReturCar();
    }
    void ReturCar()
    {
        _rigidbodyCar.transform.position = transformCar;
        _rigidbodyCar.velocity = Vector3.zero;       
    }


}
