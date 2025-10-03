using UnityEngine;

public class CubeSpawner : DualSpawner<Cube>
{
    public BombSpawner bombSpawner;

    private int totalCubeSpawned = 0;

    protected override void OnGet(Cube cube)
    {
        cube.transform.position = startPoint.position;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        totalCubeSpawned++;

        cube.gameObject.SetActive(true);
        cube.ResetState();

        cube.ReadyToReturn -= OnCubeReturned;
        cube.ReadyToReturn += OnCubeReturned;
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
