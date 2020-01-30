#pragma warning disable IDE0018

#if FIXINGS2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixings2 : MonoBehaviour
{
    public Material confirmed_material;
    public Material unconfirmed_material;
	private bool confirmed2 = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if(confirmed2 == false)
			{
				if (Physics.Raycast(ray, out hit))
				{
				    if (hit.transform.tag == "FIXINGTAG2")
                {
                    gameObject.GetComponent<MeshRenderer>().material = confirmed_material;
                    Fixings.fixings = Fixings.fixings - 1;
					confirmed2 = true;
                }
                else
                {
                    Debug.Log("Not Logged");
                }
            }
		}

        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if(confirmed2 == true)
			{
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "FIXINGTAG2")
					{
						gameObject.GetComponent<MeshRenderer>().material = unconfirmed_material;
						confirmed2 = false;

						//if (Fixings.fixings >= 1)
						//{
							Fixings.fixings = Fixings.fixings + 1;
						//}
					}
				}
			}
        }
    }
}

#endif