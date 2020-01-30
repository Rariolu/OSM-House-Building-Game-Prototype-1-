#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

#if FIXINGS4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixings4 : MonoBehaviour
{
    public Material confirmed_material;
    public Material unconfirmed_material;
	private bool confirmed4 = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

			if(confirmed4 == false)
			{
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "FIXINGTAG4")
					{
						gameObject.GetComponent<MeshRenderer>().material = confirmed_material;
						Fixings.fixings = Fixings.fixings - 1;
						confirmed4 = true;
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

			if(confirmed4 == true)
			{
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "FIXINGTAG4")
					{
						gameObject.GetComponent<MeshRenderer>().material = unconfirmed_material;
						confirmed4 = false;
						//if (Fixings.fixings >= 1)
						//{
							Fixings.fixings = Fixings.fixings + 1;

						//}
					}
                //if (Fixings.fixings >= 1)
                //{
                //    Fixings.fixings = Fixings.fixings - 1;

                //}
				}
			}
        }
    }

}

#endif