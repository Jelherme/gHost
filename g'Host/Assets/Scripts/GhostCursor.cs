using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCursor : MonoBehaviour
{
    [SerializeField] Rigidbody cursor;
    [SerializeField] Rigidbody ghost;

    Vector3 mousePos;
    Vector3 worldPos;

    [SerializeField] bool collision = false;
    [SerializeField] float releaseTime = 0.2f;

    void FixedUpdate()
    {
        MousePos();
    }

    void MousePos()
    {
        if (!collision)
        {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z; //garantir que o mousePos.z = camPos.z para em baixo converter tudo em world coords. como e 2d, o z vai ser subtraido no ScreenToWorldPoint 

            worldPos = Camera.main.ScreenToWorldPoint(mousePos); //converte a mousePos em coords do world
            cursor.transform.position = worldPos;
            ghost.transform.position = worldPos;
        }
    }

    void OnCollisionEnter(Collision wall)
    {
        collision = true;        
    }

    private void OnCollisionExit(Collision wall)
    {
        collision = false;
        StartCoroutine(ReturnToPos());
    }

    IEnumerator ReturnToPos()
    {
        yield return new WaitForSeconds(releaseTime);

        Vector3 direction = (cursor.transform.position - ghost.transform.position);
        ghost.transform.position = direction.normalized * Time.deltaTime;
    }
}
