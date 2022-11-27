using UnityEngine;

public class Thrown : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    } // test
}
