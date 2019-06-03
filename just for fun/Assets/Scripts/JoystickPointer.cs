using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPointer : MonoBehaviour
{
    public Vector2 difference;
    public float rotZ;
    public Player player;

    public GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal2")) > 0f || Mathf.Abs(Input.GetAxis("Vertical2")) > 0f)
        {
            pointer.SetActive(true);
        }
        else
        {
            pointer.SetActive(false);
        }

        if (Input.GetAxis("Horizontal2") == 0f && Input.GetAxis("Vertical2") == 0f)
        {
            if (player.flipSprite)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else
        {
            difference = new Vector2(-1f * Input.GetAxis("Horizontal2"), -1f * Input.GetAxis("Vertical2")) - Vector2.zero;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);
        }
    }
}
