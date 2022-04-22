using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_playerSpeed;

    public GameObject floatingText;
    public GameObject collectParticle;

    private Touch touch;

    private int m_rightAngle = 0;
    private int m_leftAngle = -90;

    private bool m_isLeft = false;


    private void Update()
    {
        transform.Translate(Vector3.forward * m_playerSpeed * Time.deltaTime, Space.Self);

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                transform.rotation = m_isLeft ? RotateRight() : RotateLeft();
            }
        }

        if (transform.position.y < -3.0f) {
            GameOver();
        }
    }
    private Quaternion RotateLeft()
    {
        m_isLeft = true;
        return Quaternion.Euler(0, m_leftAngle, 0);
    }
    private Quaternion RotateRight()
    {
        m_isLeft = false;
        return Quaternion.Euler(0, m_rightAngle, 0);
    }
    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gold")) {
            Destroy(other.gameObject);
            Instantiate(floatingText, transform.position, floatingText.transform.rotation);
            Instantiate(collectParticle, other.gameObject.transform.position, collectParticle.transform.rotation);
        }
    }
}
