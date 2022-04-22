using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInBounds : MonoBehaviour
{
    private Vector3 destroyPosition;

    private bool m_isPlayerPassed;

    private void Start()
    {
        destroyPosition = new Vector3(transform.position.x, -8, transform.position.z);
    }
    private void Update()
    {
        if (m_isPlayerPassed) {
            float step = 3 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destroyPosition, step);
        }

        if (transform.position.y <= destroyPosition.y) {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(MoveCoroutine());
        }
    }
    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        m_isPlayerPassed = true;
    }
}
