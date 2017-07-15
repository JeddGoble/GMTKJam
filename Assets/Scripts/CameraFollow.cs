using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform camTarget;
	public float trackingSpeed = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
	{
		if (camTarget != null)
		{
			var targetPos = new Vector2(camTarget.position.x / 4f, camTarget.position.y / 4f);

			var newPos = Vector2.Lerp(transform.position,
			                          targetPos,
						  Time.deltaTime * trackingSpeed);
			var camPosition = new Vector3(newPos.x, newPos.y, -10f);
			transform.position = camPosition;
		}
	}
}
