using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ItemMovementComponent), typeof(SphereCollider))]
public class BreakableItem : MonoBehaviour
{
    public UnityEvent onBreak;
    public Vector3 startingForward;
    public Collider possessCollider;
    public GameObject brokenVersion;
    public bool canBreak;
    public void Start()
    {
        startingForward = transform.forward;
        gameObject.tag="PossessItem";
        gameObject.layer = LayerMask.NameToLayer("Item");

        GameEvents.RegisterItem();
    }
    
    public void Break()
    {

        gameObject.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("BrokenItem");
        possessCollider.enabled = false;
        GameEvents.ItemBroken();

        if(onBreak == null)
        {
            return;
        }

        onBreak.Invoke();

        var go = GameObject.Instantiate(brokenVersion, transform.position, Quaternion.identity);
        go.transform.localScale = transform.localScale;
        go.transform.rotation = transform.rotation;
        Destroy(gameObject);

        SoundManager.PlayBreakingSound();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Floor")
        {
            Break();
        }
    }
}
