using UnityEngine;

public class BombSpawner : GenericSpawner<Bomb>
{
    protected override void OnGet(Bomb bomb)
    {
        base.OnGet(bomb);
    }

    public Bomb GetBombFromPoolAtPosition(Vector3 position)
    {
        Bomb bomb = pool.Get();
        bomb.transform.position = position;
        return bomb;
    }

    public void ReleaseBombToPool(Bomb bomb)
    {
        bomb.ReadyToReturn -= ReturnToPool;
        pool.Release(bomb);
    }
}
