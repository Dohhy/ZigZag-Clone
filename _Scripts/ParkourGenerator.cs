using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{
    private Transform m_player;

    public GameObject platformPrefab;
    private float prefabScaleZ;

    #region Spawn Variables
    public Vector3 spawnPosition;
    public int spawnDistance;
    private int m_distanceX;
    private int m_distanceZ;
    private int m_diagonal;
    private bool m_isAllowedRandomPosition;
    #endregion


    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        prefabScaleZ = platformPrefab.GetComponent<Transform>().localScale.z;
    }

    private void Update()
    {
        CalculateDiagonal();

        if (m_diagonal < spawnDistance) {
            CalculateNextSpawnPoint();

            Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
        }
    }

    private void CalculateNextSpawnPoint()
    {
        if (spawnPosition.z > Mathf.Abs(spawnPosition.x) + 2.5f || Mathf.Abs(spawnPosition.x) > spawnPosition.z + 2.5f) {
            m_isAllowedRandomPosition = false;
        }

        if (m_isAllowedRandomPosition) {
            int dice = Random.Range(0, 2);

            if (dice == 0) {
                spawnPosition = new Vector3(spawnPosition.x - prefabScaleZ, spawnPosition.y, spawnPosition.z);  //Go Left
            }

            if (dice == 1) {
                spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z + prefabScaleZ);  //Go Right
            }
        }

        if (!m_isAllowedRandomPosition) { 
            if (spawnPosition.z > Mathf.Abs(spawnPosition.x) + 2.5f) {
                spawnPosition = new Vector3(spawnPosition.x - prefabScaleZ, spawnPosition.y, spawnPosition.z);
            }
            if (Mathf.Abs(spawnPosition.x) > spawnPosition.z + 2.5f) {
                spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z + prefabScaleZ);
            }
        }

        m_isAllowedRandomPosition = true;
    }
    private void CalculateDiagonal()
    {
        m_distanceX = (int)(Mathf.Abs(spawnPosition.x) - Mathf.Abs(m_player.position.x));
        m_distanceZ = (int)(spawnPosition.z - m_player.position.z);

        m_distanceX *= m_distanceX;
        m_distanceZ *= m_distanceZ;

        m_diagonal = m_distanceX + m_distanceZ;
        m_diagonal *= m_diagonal;
    }
}
