using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mute : MonoBehaviour
{
    public bool soundON;
    public AudioListener audioListener;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleChanged()
    {
        audioListener.enabled = !audioListener.enabled;
    }
}
