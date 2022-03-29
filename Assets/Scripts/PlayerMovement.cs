using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;

    float horizontalMove = 0f; //Input del eje horizontal

    [SerializeField] float runSpeed; //Velocidad de movimiento del personaje

    bool jump;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //Recogemos el input del eje horizontal (input manager)
            
            if(Input.GetButton("Jump"))//Si presionamos la tecla de salto
                jump = true;
        }
    }

    public void Jump()
    {
        jump = true;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() //para fisicas
    {
        characterController.Move(runSpeed, jump);
        jump = false; //Para que no vuelva a saltar hasta que se presione la tecla
    }
}
