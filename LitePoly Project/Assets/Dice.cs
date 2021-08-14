using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.05f;
    }

	public int GetDiceCount()
	{
		if (Vector3.Dot(transform.forward, Vector3.up) > 0.6f)
			return 1;
		if (Vector3.Dot(-transform.forward, Vector3.up) > 0.6f)
			return 6;
		if (Vector3.Dot(transform.up, Vector3.up) > 0.6f)
			return 5;
		if (Vector3.Dot(-transform.up, Vector3.up) > 0.6f)
			return 2;
		if (Vector3.Dot(transform.right, Vector3.up) > 0.6f)
			return 4;
		if (Vector3.Dot(-transform.right, Vector3.up) > 0.6f)
			return 3;
		return -1;
	}
}
