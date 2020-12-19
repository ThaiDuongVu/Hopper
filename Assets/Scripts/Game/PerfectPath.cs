using UnityEngine;
using System.Collections.Generic;

public class PerfectPath : MonoBehaviour
{
    public GameObject coinPrefab;
    private readonly List<GameObject> _coins = new List<GameObject>();
    public Transform coins;
    public Color32[] coinColors;

    private const float PathSize = 1f; // Distance between 2 cubes
    public int PathCount { get; set; } // Number of cubes that represent the path

    public int SpawnDirection { get; set; }

    public void CalculateNextPath(Platform nextPlatform, Player player)
    {
        // Destroy old coins
        foreach (GameObject coin in _coins)
        {
            Destroy(coin);
        }
        PathCount = 0;

        // Calculate the point in between 2 platforms
        Vector3 playerPosition = player.transform.position;

        Vector3 sumPosition = nextPlatform.transform.position + playerPosition;
        Vector3 pointInBetween = new Vector3(sumPosition.x / 2f, playerPosition.y + 5f, sumPosition.z / 2f);

        // Spawn a new path
        SpawnPath(pointInBetween, player);

        // Rotate path to player's direction
        Vector3 coinDirection = new Vector3(-player.direction.x, 0f, -player.direction.z);

        if (SpawnDirection == 0)
            coins.transform.right = coinDirection;
        else
            coins.transform.forward = coinDirection;
    }

    // Spawn new coins while point is higher than player
    private void SpawnPath(Vector3 pointInBetween, Player player)
    {
        // Set coins position to middle position
        coins.position = pointInBetween;

        // The color to spawn the coin with
        Color32 coinColor = coinColors[Random.Range(0, coinColors.Length)];

        int pointIndex = 1;

        // While spawn coin position is higher than player
        while (pointInBetween.y - pointIndex * PathSize > player.transform.position.y)
        {
            // Spawn coin rotation = coin default rotation
            Quaternion spawnRotation = coinPrefab.transform.rotation;

            // Left direction
            if (SpawnDirection == 0)
            {
                // Spawn 2 coins at either sides of the path
                SpawnCoin(new Vector3(pointInBetween.x + pointIndex * PathSize,
                            pointInBetween.y - pointIndex * PathSize,
                            pointInBetween.z - pointIndex * PathSize), spawnRotation, coinColor);

                SpawnCoin(new Vector3(pointInBetween.x - pointIndex * PathSize,
                            pointInBetween.y - pointIndex * PathSize,
                            pointInBetween.z + pointIndex * PathSize), spawnRotation, coinColor);
            }
            // Right direction
            else
            {
                // Spawn 2 coins at either sides of the path

                SpawnCoin(new Vector3(pointInBetween.x - pointIndex * PathSize,
                            pointInBetween.y - pointIndex * PathSize,
                            pointInBetween.z - pointIndex * PathSize), spawnRotation, coinColor);

                SpawnCoin(new Vector3(pointInBetween.x + pointIndex * PathSize,
                            pointInBetween.y - pointIndex * PathSize,
                            pointInBetween.z + pointIndex * PathSize), spawnRotation, coinColor);
            }

            // Next iteration
            pointIndex++;
        }
    }

    // Spawn a new coin object at position and rotation
    private void SpawnCoin(Vector3 spawnPosition, Quaternion spawnRotation, Color32 spawnColor)
    {
        // Instantiate coin
        GameObject newCoin = Instantiate(coinPrefab, spawnPosition, spawnRotation);

        // Set new coin parent to coins
        newCoin.transform.parent = coins;

        // Set the coin color
        newCoin.GetComponent<MeshRenderer>().material.color = spawnColor;

        // Add new coin to coins list and update path count
        _coins.Add(newCoin);
        PathCount++;
    }
}
