using UnityEngine;
using System.Collections.Generic;

public class PerfectPath : MonoBehaviour
{
    public GameObject coin;
    private List<GameObject> coins = new List<GameObject>();

    private const float PathSize = 1f; // Distance between 2 cubes
    public int pathCount { get; set; } // Number of cubes that represent the path

    public void CalculateNextPath(Platform nextPlatform, Player player)
    {
        // Destroy old coins
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        pathCount = 0;

        // Calculate the point in between 2 platforms
        Vector3 sumPosition = nextPlatform.transform.position + player.transform.position;
        Vector3 pointInBetween = new Vector3(sumPosition.x / 2f, player.transform.position.y + 5f, sumPosition.z / 2f);

        SpawnCoins(pointInBetween, player);
    }

    // Spawn new coins while point is higher than player
    private void SpawnCoins(Vector3 pointInBetween, Player player)
    {
        int pointIndex = 1;

        while (pointInBetween.y - pointIndex * PathSize > player.transform.position.y)
        {
            Vector3 spawnPosition;
            Quaternion spawnRotation = coin.transform.rotation;

            spawnPosition = new Vector3(pointInBetween.x + pointIndex * PathSize, pointInBetween.y - pointIndex * PathSize, pointInBetween.z - pointIndex * PathSize);
            GameObject newCoin = Instantiate(coin, spawnPosition, spawnRotation);
            coins.Add(newCoin);

            spawnPosition = new Vector3(pointInBetween.x - pointIndex * PathSize, pointInBetween.y - pointIndex * PathSize, pointInBetween.z + pointIndex * PathSize);
            GameObject newCoin2 = Instantiate(coin, spawnPosition, spawnRotation);
            coins.Add(newCoin2);

            pathCount += 2;
            pointIndex++;
        }
    }
}
