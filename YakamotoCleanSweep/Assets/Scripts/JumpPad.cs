using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float padJumpForce;
    [SerializeField] private string playerTag;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            collision.rigidbody.AddForce(new Vector3(0, padJumpForce, 0), ForceMode.Impulse);
        }
    }
}
