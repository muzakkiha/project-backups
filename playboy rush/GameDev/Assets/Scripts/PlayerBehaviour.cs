using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float charm = 5f;

    public Vector3 target;

    // Player speed
    public float speed;
    public float speedKeyboard;


    //TODO
    //Implementasi control kepada player
    // Use this for initialization
    void Start () {
        target = transform.position;
    }

    // Update is called once per frame
    void Update () {
        // Lock the z-axis
        Vector3 pos = transform.position;
        pos.z = 2;
        transform.position = pos;
        //TO DO
        //Implementasi metode untuk membuat player bergerak, baik dengan mouse click atau dengan keyboard
        if (Input.GetMouseButtonDown(0)) {
            MouseClick();
        }
        MovePlayer();
        //TO DO
        // Movement using keyboard
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 position = transform.position;
            position.x -= speedKeyboard * Time.deltaTime;
            // Membatasi gerak agar tetap dalam arena permainan
            transform.position = new Vector2 (Mathf.Clamp(position.x, -10.5f, 10.5f), position.y);
            target = transform.position;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 position = transform.position;
            position.x += speedKeyboard * Time.deltaTime;
            transform.position = new Vector2(Mathf.Clamp(position.x, -10.5f, 10.5f), position.y);
            target = transform.position;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 position = transform.position;
            position.y += speedKeyboard * Time.deltaTime;
            transform.position = new Vector2 (position.x, Mathf.Clamp(position.y, -5f, 5f));
            target = transform.position;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 position = transform.position;
            position.y -= speedKeyboard * Time.deltaTime;
            transform.position = new Vector2(position.x, Mathf.Clamp(position.y, -5f, 5f));
            target = transform.position;
        }
    }

    void MouseClick()
    {
        //Mengambil posisi Vector3 dari layar dengan mouse click
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //TODO
        //Implementasi assign value untuk posisi baru
        target = temp;
    }

    void MovePlayer()
    {
        //TODO
        //Implementasi perpindahan player
        //Hint: Vector3.MoveTowards();
        if ((Vector2)transform.position != (Vector2)target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
