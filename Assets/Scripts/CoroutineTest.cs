using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    Rigidbody _rb;
    private bool _isJumping = false;
    private string _groundTag = "Ground";
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Bosluga basti");
            _rb.AddForce(Vector3.up * 10f,ForceMode.Impulse);
            _isJumping = true;
            StartCoroutine(WaitForJump());
        }
    }
    private IEnumerator WaitForJump()
    {
        yield return new WaitUntil(() => !_isJumping);

        Debug.Log("Cube has jumped");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isJumping = false;
        }
    }
}
