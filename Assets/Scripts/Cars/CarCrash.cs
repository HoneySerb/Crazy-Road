using System.Collections;
using UnityEngine;

public class CarCrash : MonoBehaviour
{
    [SerializeField] private float _maxCrashResist;
    [SerializeField] private GameObject _crashEffects;


    private void OnCollisionEnter(Collision collision)
    {
        float crashImpulse = Vector3.Distance(Vector3.zero, collision.impulse);
        if (crashImpulse > _maxCrashResist)
        {
            Destroy(GetComponent<Car>());

            StartCoroutine(ReduceSpeed());

            _crashEffects.SetActive(true);
        }
    }

    private IEnumerator ReduceSpeed()
    {
        Rigidbody _rigidbody = GetComponent<Rigidbody>();
        while (_rigidbody.velocity != Vector3.zero)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, Time.deltaTime * 5f);

            yield return null;
        }
    }
}
