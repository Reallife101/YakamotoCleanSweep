using UnityEngine;
using System.Collections;

public class PlayerAudio : AudioController<PlayerMovement>
{
    /* --CLIPS--
    0 : Footsteps
    1 : Jump
    2 : Land
    3 : Slide
    4 : Speed boost
    */

    [SerializeField] private float walkDelay;
    // The time after each footstep to wait until the next one plays
    [SerializeField] private float sprintDelay;
    // Should be faster than above
    [SerializeField] private float crouchDelay;
    // Should be slower than both

    private float footstepDelay;

    private Rigidbody rb = null;

    private void Start()
    {
        rb = host.gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        host.OnJump += PlayJumpSound;
        Puddle.OnPuddleEnter += PlaySpeedBoostSound;
    }

    private void PlayJumpSound()
    {
        source.PlayOneShot(clips[0]);
    }

    private void PlaySpeedBoostSound()
    {
        source.PlayOneShot(clips[4]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" && rb.velocity.y < 0)
        {
            source.PlayOneShot(clips[1]);
        }
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            if (host.isSliding)
            {
                source.clip = clips[3];
                source.Play();
            }
            else if (host.isGrounded && footstepDelay < 0 && host.moveDirection.x != 0)
            {
                if (host.isSprinting)
                {
                    footstepDelay = sprintDelay;
                }
                else if (host.isCrouching)
                {
                    footstepDelay = crouchDelay;
                }
                else
                {
                    footstepDelay = walkDelay;
                }

                source.clip = clips[0];
                source.Play();
            }
            else
            {
                footstepDelay -= Time.deltaTime;
            }
        }
    }

    private void OnDisable()
    {
        host.OnJump -= PlayJumpSound;
        Puddle.OnPuddleEnter -= PlaySpeedBoostSound;
    }
}
