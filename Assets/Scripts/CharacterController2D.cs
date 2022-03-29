using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rbPlayer;

    //Salto
    [Space]
    [Header("JUMP")]
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck; //Posición desde donde saldrá el rayo que detecta si estamos en el suelo
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] float groundCheckDistance; //Distancia que mide el raycast
    [SerializeField] LayerMask whatIsGround; //Layer que engloba el suelo para el jugador
    bool grounded; //Indica si el personaje esta en el suelo

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(groundCheck.position, groundCheck.position - new Vector3(0, groundCheckDistance, 0), Color.green);
        Debug.DrawLine(groundCheckLeft.position, groundCheckLeft.position - new Vector3(0, groundCheckDistance, 0), Color.green);
        Debug.DrawLine(groundCheckRight.position, groundCheckRight.position - new Vector3(0, groundCheckDistance, 0), Color.green);
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        //Comprobar suelo
        RaycastHit2D groundRaycast = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        RaycastHit2D groundRaycastLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, groundCheckDistance, whatIsGround);
        RaycastHit2D groundRaycastRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, groundCheckDistance, whatIsGround);

        if (groundRaycast || groundRaycastLeft || groundRaycastRight)//Si el raycast toca algo
            grounded = true;
        else
            grounded = false;
    }
    
    public void Move(float move, bool jump)
    {
        //Calculamos la velocidad máxima a la que queremos que se mueva el personaje
        transform.position += new Vector3(move, 0,0);

        if(jump && grounded)
        {
            grounded = false;
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
            rbPlayer.AddForce(new Vector2(0, jumpForce)); //Añade una fuerza vertical
        }
    }

}
