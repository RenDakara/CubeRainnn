using UnityEngine;

public class BombSpawner : GenericSpawner<Bomb>
{
    protected override void OnGet(Bomb bomb)
    {
        base.OnGet(bomb);
        bomb.OnReturned -= OnBombReturned;
        bomb.OnReturned += OnBombReturned;
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
        bomb.OnReturned -= OnBombReturned;
        pool.Release(bomb);
    }
}
