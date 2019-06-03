using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Vector2 difference;
    public float rotZ;
    public Player player;

    public GameObject pointer;

    public SpriteRenderer sprite;
    public bool toggle;

    private bool activatePointer;

    // Start is called before the first frame update
    void Start()
    {
        activatePointer = false;
        toggle = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            toggle = !toggle;
        }

        if (toggle)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
