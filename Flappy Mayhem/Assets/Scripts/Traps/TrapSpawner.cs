using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float heightRange;
    [SerializeField] private GameObject trap;

    private float timer;

    void Start()
    {
        SpawnTrap();
    }

    void Update()
    {
        if (timer > maxTime)
        {
            SpawnTrap();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void SpawnTrap()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange) + 1);
        GameObject spawnedTrap = Instantiate(trap, spawnPos, Quaternion.identity);

        Destroy(spawnedTrap, 30f);
    }
}
