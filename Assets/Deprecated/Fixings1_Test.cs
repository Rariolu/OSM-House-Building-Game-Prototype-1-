#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if FIXINGS1

public class Fixings1_Test : MonoBehaviour
{
    public Material confirmed_material;
    public Material unconfirmed_material;
    private bool confirmed1 = false;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (confirmed1 == false)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "FIXINGTAG1")
                    {
                        gameObject.GetComponent<MeshRenderer>().material = confirmed_material;
                        Fixings.fixings = Fixings.fixings - 1;
                        confirmed1 = true;
                    }
                    else
                    {
                        Debug.LogFormat("Not Logged. tag: {0};",hit.transform.tag);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (confirmed1 == true)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "FIXINGTAG1")
                    {
                        gameObject.GetComponent<MeshRenderer>().material = unconfirmed_material;
                        confirmed1 = false;
                        
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