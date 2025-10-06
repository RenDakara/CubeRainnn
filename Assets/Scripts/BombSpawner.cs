using UnityEngine;

public class BombSpawner : GenericSpawner<Bomb>
{
    protected override void OnGet(Bomb bomb)
    {
        base.OnGet(bomb);

        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        bomb.ResetState();

        bomb.ReadyToReturn -= OnBombReturned;
        bomb.ReadyToReturn += OnBombReturned;
    }

    private void OnBombReturned(Bomb bomb)
    {
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
