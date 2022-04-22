using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCube : MonoBehaviour
{
    private Vector3 rotation = new Vector3(90, 90, 90);

    private void Start()
    {
        int dice = Random.Range(0, 3);
        if (dice == 0) {
            gameObject.SetActive(true);
        }
        else { gameObject.SetActive(false); }
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime, Space.Self);
    }
}
