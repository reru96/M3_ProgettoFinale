using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("OpenDoor", 10f);
    }

    // Update is called once per frame
    void OpenDoor()
    {
        anim.SetBool("DoorOpen", true);
    }
}
