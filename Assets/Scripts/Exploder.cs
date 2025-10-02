using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _force = 700f;

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
          if(collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_force, transform.position, _radius);
            }
        }
    }
}
