using UnityEngine;
using System.Collections.Generic;

public class PerfectPath : MonoBehaviour
{
    public GameObject coinPrefab;
    private readonly List<GameObject> _coins = new List<GameObject>();
    public Transform coins;

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

        // Spawn a new path
        SpawnPath(pointInBetween, player);

        // Rotate path to player's direction
        Vector3 coinDirection = new Vector3(-player.direction.x, 0f, -player.direction.z);
        if (spawnDirection == 0)
        {
            coins.transform.right = coinDirection;
        }
        else
        {
            coins.transform.forward = coinDirection;
        }
    }

    // Spawn new coins while point is higher than player
    private void SpawnPath(Vector3 pointInBetween, Player player)
    {
        // Set coins position to middle position
        coins.position = pointInBetween;

        int pointIndex = 1;

        // While spawn coin position is higher than player
        while (pointInBetween.y - pointIndex * PathSize > player.transform.position.y)
        {
            // Position to spawn coins
            Vector3 spawnPosition;

            // Spawn coin rotation = coin default rotation
            Quaternion spawnRotation = coinPrefab.transform.rotation;

            // Left direction
            if (spawnDirection == 0)
            {
                // Spawn 2 coins at either sides of the path
                spawnPosition = new Vector3(pointInBetween.x + pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z - pointIndex * PathSize);

                SpawnCoin(spawnPosition, spawnRotation);

                spawnPosition = new Vector3(pointInBetween.x - pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z + pointIndex * PathSize);

                SpawnCoin(spawnPosition, spawnRotation);
            }
            // Right direction
            else
            {
                // Spawn 2 coins at either sides of the path
                spawnPosition = new Vector3(pointInBetween.x - pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z - pointIndex * PathSize);

                SpawnCoin(spawnPosition, spawnRotation);

                spawnPosition = new Vector3(pointInBetween.x + pointIndex * PathSize,
                    pointInBetween.y - pointIndex * PathSize,
                    pointInBetween.z + pointIndex * PathSize);

                SpawnCoin(spawnPosition, spawnRotation);
            }

            // Next iteration
            pointIndex++;
        }
    }

    private void SpawnCoin(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, spawnRotation);

        newCoin.transform.parent = coins;
        _coins.Add(newCoin);

        pathCount++;
    }
}
