using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{

    short cameraMode = 0;

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, 45, Space.Self);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.up, -45, Space.Self);

    }

    void CamFollowPlayer()
    {
        GameObject actualUnit = TurnManager.GetActualUnit();

        if(actualUnit.tag == "Player")
            this.transform.position = actualUnit.transform.position;

    }

    private void Update()
    {


        //TODO - Tecla para trocar modo de Camera
        switch(cameraMode)
        {
            case 0:
                CamFollowPlayer();
                break;

            case 1:
                break;
            default:
                break;
        }
    }
}
