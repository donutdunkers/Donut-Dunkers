using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBox : MonoBehaviour {

    private Animator boxAnimator;
    
    void Start() {
        boxAnimator = GetComponent<Animator>();
    }

    public void Open() {
        boxAnimator.SetBool("isOpen", true);
    }

    public void Close() {
        boxAnimator.SetBool("isOpen", false);
    }
}
