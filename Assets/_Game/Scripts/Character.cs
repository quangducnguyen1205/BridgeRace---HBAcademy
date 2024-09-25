using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Character : MonoBehaviour
{
    [SerializeField] MeshRenderer meshR;
    [SerializeField] private Animator anim;
    [SerializeField] protected float moveSpeed;
    private string currenAnimName;
    protected bool isMoving = false;

    protected virtual void Start()
    {
        OnInit();
    }
    protected virtual void Update(){
        Move();
    }

    public virtual void OnInit(){

    }
    public virtual void OnDespawn(){

    }

    protected void ChangeAnim(string animName){
        if (currenAnimName != animName){
            anim.ResetTrigger(animName);
            currenAnimName = animName;
            anim.SetTrigger(animName);
        }
    }

    protected virtual void Move(){
        
    }
}
