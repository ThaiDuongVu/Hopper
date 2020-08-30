using UnityEngine;
using System.Collections.Generic;

public class PerfectPath : MonoBehaviour
{
    public GameObject coinPrefab;
    private readonly List<GameObject> _coins = new List<GameObject>();

    private const float PathSize = 1f; // Distance between 2 cubes
    public int pathCount { get; set; } // Number of cubes that represent the path

    public int spawnDirection { get; set; }

    public void CalculateNextPath(Platform nextPlatform, Player player)
    {
        // Destroy old coins
        foreach (GameObject coin in _coins)
        {
            Destroy(coin);
        }
        pathCount = 0;

        // Calculate the point in between 2 platforms
        Vector3 playerPosition = player.transform.position;

        Vector3 sumPosition = nextPlatform.transform.position + playerPosition;
        Vector3 pointInBetween = new Vector3(sumPosition.x / 2f, playerPosition.y + 5f, sumPosition.z / 2f);

        SpawnCoins(pointInBetween, player);
    }

    // Spawn new coins while point is higher than player
    private void SpawnCoins(Vector3 pointInBetween, Player player)
    {
        int pointIndex = 1;

        while (pointInBetween.y - pointIndex * PathSize > player.transform.position.y)
        {
            Vector3 spawnPosition;
            Quaternion spawnRotation = coinPrefab.transform.rotation;

            if (spawnDirection == 0)
            {
                spawnPosition = new Vector3(pointInBetween.x + pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z - pointIndex * PathSize);

                _coins.Add(Instantiate(coinPrefab, spawnPosition, spawnRotation));

                spawnPosition = new Vector3(pointInBetween.x - pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z + pointIndex * PathSize);

                _coins.Add(Instantiate(coinPrefab, spawnPosition, spawnRotation));
            }
            else
            {
                spawnPosition = new Vector3(pointInBetween.x - pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z - pointIndex * PathSize);

                _coins.Add(Instantiate(coinPrefab, spawnPosition, spawnRotation));

                spawnPosition = new Vector3(pointInBetween.x + pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z + pointIndex * PathSize);

                _coins.Add(Instantiate(coinPrefab, spawnPosition, spawnRotation));
            }

            pathCount += 2;
            pointIndex++;
        }
    }
}
