using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float turnSpeed;
    public int damage;
    public float attackRange;
    public Sprite GXF;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make the item rotate in your position
        transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            collision.GetComponent<Player>().Equip(this);
        }
    }
}
