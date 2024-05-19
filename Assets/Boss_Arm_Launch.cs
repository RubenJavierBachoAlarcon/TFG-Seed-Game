using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Arm_Launch : MonoBehaviour
{
    public GameObject armPrefab;

    private void armLaunch()
    {
        // Lanzar el brazo recto
        GameObject armStraight = Instantiate(armPrefab, transform.position, Quaternion.Euler(0, 0, 180));

        // Lanzar el brazo 45 grados hacia arriba
        Quaternion rotationUp = Quaternion.Euler(0, 0, 160); // Ajusta el ángulo según sea necesario
        GameObject armUp = Instantiate(armPrefab, transform.position, rotationUp);

        // Lanzar el brazo 45 grados hacia abajo
        Quaternion rotationDown = Quaternion.Euler(0, 0, 200); // Ajusta el ángulo según sea necesario
        GameObject armDown = Instantiate(armPrefab, transform.position, rotationDown);
    }

    private void armLaunch2()
    {   
        Instantiate(armPrefab, transform.position, Quaternion.Euler(0, 0, 170));
        Instantiate(armPrefab, transform.position, Quaternion.Euler(0, 0, 190));
    }




}
