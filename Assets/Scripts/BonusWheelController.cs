using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWheelController : MonoBehaviour
{
    [Header("Prefab Reference")]
    public GameObject bonusWheelPrefab;

    [Header("Spawn Settings")]
    public float spawnDelay = 5f;
    public float moveSpeed = 2f;
    public Vector2 levelBounds = new Vector2(8f, 5f);

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            SpawnBonusWheel();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnBonusWheel()
    {
        int side = Random.Range(0, 4);
        Vector3 startPos = Vector3.zero;
        Vector3 endPos = Vector3.zero;

        switch (side)
        {
            case 0: // Left to Right
                startPos = new Vector3(-levelBounds.x - 1f, 0, 0);
                endPos = new Vector3(levelBounds.x + 1f, 0, 0);
                break;
            case 1: // Right to Left
                startPos = new Vector3(levelBounds.x + 1f, 0, 0);
                endPos = new Vector3(-levelBounds.x - 1f, 0, 0);
                break;
            case 2: // Top to Bottom
                startPos = new Vector3(0, levelBounds.y + 1f, 0);
                endPos = new Vector3(0, -levelBounds.y - 1f, 0);
                break;
            case 3: // Bottom to Top
                startPos = new Vector3(0, -levelBounds.y - 1f, 0);
                endPos = new Vector3(0, levelBounds.y + 1f, 0);
                break;
        }

        GameObject wheel = Instantiate(bonusWheelPrefab, startPos, Quaternion.identity);
        StartCoroutine(MoveBonusWheel(wheel, endPos));
    }

    IEnumerator MoveBonusWheel(GameObject wheel, Vector3 endPos)
    {
        Vector3 start = wheel.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed / Vector3.Distance(start, endPos);
            wheel.transform.position = Vector3.Lerp(start, endPos, t);
            yield return null;
        }

        Destroy(wheel);
    }
}
