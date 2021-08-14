using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;

	private int diceCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsMoving()
    {
        if (rb.velocity.magnitude > 0.05f)
        {
            return true;
        }
        return false;
    }

	public int GetDiceCount()
	{
		diceCount = 0;
		regularDiceCount();
		return diceCount;
	}

	void regularDiceCount()
	{
		if (Vector3.Dot(transform.forward, Vector3.up) > 0.6f)
			diceCount = 1;
		if (Vector3.Dot(-transform.forward, Vector3.up) > 0.6f)
			diceCount = 6;
		if (Vector3.Dot(transform.up, Vector3.up) > 0.6f)
			diceCount = 5;
		if (Vector3.Dot(-transform.up, Vector3.up) > 0.6f)
			diceCount = 2;
		if (Vector3.Dot(transform.right, Vector3.up) > 0.6f)
			diceCount = 4;
		if (Vector3.Dot(-transform.right, Vector3.up) > 0.6f)
			diceCount = 3;

	}
}
