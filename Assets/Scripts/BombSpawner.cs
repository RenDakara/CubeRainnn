using UnityEngine;

public class BombSpawner : DualSpawner<Bomb>
{
    protected override void Start() { }

    protected override void OnGet(Bomb bomb)
    {
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        bomb.gameObject.SetActive(true);
        bomb.ResetState();

        bomb.ReadyToReturn -= OnBombReturned;
        bomb.ReadyToReturn += OnBombReturned;
    }

    private void OnBombReturned(IPoolableObject poolable)
    {
        if (poolable is Bomb bomb)
            pool.Release(bomb);
    }

    public Bomb GetBombFromPoolAtPosition(Vector3 position)
    {
        Bomb bomb = pool.Get();
        bomb.transform.position = position;
        return bomb;
    }

    public void ReleaseBombToPool(Bomb bomb)
    {
        pool.Release(bomb);
    }
}
