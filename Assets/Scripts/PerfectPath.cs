using UnityEngine;
using System.Collections.Generic;

public class PerfectPath : MonoBehaviour
{
    public GameObject pathPresentor;
    private List<GameObject> _presentors = new List<GameObject>();

    private const float PathSize = 1f; // Distance between 2 cubes
    public int pathCount { get; set; } // Number of cubes that represent the path

    public void CalculatePath(Platform nextPlatform, Player player)
    {
        // Destroy old presentors
        foreach (GameObject presentor in _presentors)
        {
            Destroy(presentor);
        }
        pathCount = 0;

        // Calculate the point in between 2 platforms
        Vector3 sumPosition = nextPlatform.transform.position + player.transform.position;
        Vector3 pointInBetween = new Vector3(sumPosition.x / 2f, player.transform.position.y + 5f, sumPosition.z / 2f);

        int pointIndex = 0;

        // Spawn new presentors while point is higher than player
        while (pointInBetween.y - pointIndex * PathSize > player.transform.position.y)
        {
            Vector3 spawnPosition = new Vector3(pointInBetween.x + pointIndex * PathSize, pointInBetween.y - pointIndex * PathSize, pointInBetween.z - pointIndex * PathSize);
            Vector3 spawnPosition2 = new Vector3(pointInBetween.x - pointIndex * PathSize, pointInBetween.y - pointIndex * PathSize, pointInBetween.z + pointIndex * PathSize);
            Quaternion spawnRotation = pathPresentor.transform.rotation;

            GameObject newPresentor = Instantiate(pathPresentor, spawnPosition, spawnRotation);
            GameObject newPresentor2 = Instantiate(pathPresentor, spawnPosition2, spawnRotation);

            _presentors.Add(newPresentor);
            _presentors.Add(newPresentor2);

            pathCount += 2;
            pointIndex++;
        }
    }
}
