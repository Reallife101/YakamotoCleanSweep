using UnityEngine;

public class MediaController<T> : MonoBehaviour
{
    [SerializeField] protected T host = default(T);
    [SerializeField] protected Animator anim = null;
}
