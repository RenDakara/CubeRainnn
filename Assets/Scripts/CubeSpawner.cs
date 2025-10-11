using UnityEngine;

public class CubeSpawner : GenericSpawner<Cube>
{
    public BombSpawner bombSpawner;

    private int _totalCubeSpawned = 0;

    protected override void OnGet(Cube cube)
    {
        base.OnGet(cube);

        cube.OnReturned += OnCubeReturned;
        _totalCubeSpawned++;
    }

    private void OnCubeReturned(Cube cube)
    {
        cube.OnReturned -= OnCubeReturned;  

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
