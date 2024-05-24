using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Object : MonoBehaviour
{
    // Start is called before the first frame update
    public void DeleteParent()
    {
        // Check if the GameObject has a parent
        if (transform.parent.parent != null)
        {
            // Destroy the parent GameObject
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
