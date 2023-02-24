using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    



    // Update is called once per frame
    void FixedUpdate()
    {
     /*  
        if (Input.GetKey("d")) { rb.AddForce(speed * Time.deltaTime, 0, 0); } //f=ma //search for force mode
        if (Input.GetKey("a")) { rb.AddForce(-speed * Time.deltaTime, 0, 0); }
        if (Input.GetKey("w")) { rb.AddForce(0, 0, speed * Time.deltaTime); }
        if (Input.GetKey("s")) { rb.AddForce(0, 0, -speed * Time.deltaTime); }
     */
      
        float xHorizontal = Input.GetAxis("Horizontal");
        float yVertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(xHorizontal, 0, yVertical);
        transform.Translate(move * speed * Time.deltaTime);

        //rb.AddForce(move); //or you can use this


    }
}
