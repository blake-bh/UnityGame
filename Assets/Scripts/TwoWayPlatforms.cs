using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayPlatforms : MonoBehaviour
{

   private Collider2D collider;
   private bool playerOnPlatform;
    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame

    private  void Update()
    {
    
        if(playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            
            collider.enabled = false;
            Debug.Log("collider disabled");
            StartCoroutine(EnableCollider());
            
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
        Debug.Log("collider enabled");
       
    }
    private void SetPlayerOnPlatform(Collision2D collision, bool value)
    {
        if (collision.gameObject.name == "Player"){
           
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
        SetPlayerOnPlatform(other, true);
    }
}
