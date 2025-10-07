using UnityEngine;

public class CubeSpawner : GenericSpawner<Cube>
{
    public BombSpawner bombSpawner;

    private int _totalCubeSpawned = 0;

    protected override void OnGet(Cube cube)
    {
        base.OnGet(cube);
        _totalCubeSpawned++;
    }

    private void OnCubeReturned(IPoolableObject poolable)
    {
        if (poolable is Cube cube)
        {
            Vector3 pos = cube.transform.position;
            pool.Release(cube);

            Bomb bomb = bombSpawner.GetBombFromPoolAtPosition(pos);
            if (bomb != null)
            {
                bomb.StartFadeAndExplode(() =>
                {
                    bombSpawner.ReleaseBombToPool(bomb);
                });
            }
        }
    }
}
