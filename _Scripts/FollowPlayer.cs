using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position;
    }
    private void LateUpdate()
    {
        if (player.position.y > -3.0f)
        {
            float distanceOffset = Mathf.Abs(player.position.x) + player.position.z;
            distanceOffset /= 2;
            transform.position = new Vector3(transform.position.x, distanceOffset + cameraOffset.y, transform.position.z);
        }
    }
}
