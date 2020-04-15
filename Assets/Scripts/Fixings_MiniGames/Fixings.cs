#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script which represents a particular fixture point.
/// </summary>
public class Fixings : MultitonScript<Fixings,FIXINGSECTION>
{
    // Declaring the materials used for the fixings
    public Material confirmed_material;
	public Material unconfirmed_material;
    //static int f = 0;
    //// Counter for the number of fixings used
    //public static int fixings
    //{
    //    get
    //    {
    //        return f;
    //    }
    //    set
    //    {
    //        if (value >= 0)
    //        {
    //            f = value;
    //        }
    //        else
    //        {
    //            Logger.Log(value);
    //        }
    //    }
    //}
    // Boolean to determine if the placement of the fixing is confirmed, stops multiple additions to the counter
    private bool confirmed = false;
    public bool Confirmed
    {
        get
        {
            return confirmed;
        }
        private set
        {
            confirmed = value;
            MeshRenderer.material = confirmed ? confirmed_material : unconfirmed_material;
        }
    }
    
	MeshRenderer mRenderer;
	MeshRenderer MeshRenderer
	{
		get
		{
			return mRenderer ?? (mRenderer = GetComponent<MeshRenderer>());
		}
	}
	
    public FIXINGSECTION fixingSection;
    void Awake()
    {
        SetInstance(fixingSection, this);
    }

    void OnMouseDown()
    {
        if (!confirmed)
        {
            // It adds 1 to the counter and changes the material of the object
            FixingsUtil util;
            if (SingletonUtil.InstanceAvailable(out util))
            {
                util.Fixings--;
            }
            Confirmed = true;
            IntegratedSoundManager.PlaySoundAsync(SOUNDNAME.FIXING);
        }
        else
        {
            //fixings = fixings + 1;
            FixingsUtil fixingsUtil;
            if (SingletonUtil.InstanceAvailable(out fixingsUtil))
            {
                fixingsUtil.Fixings++;
            }
            Confirmed = false;
			ConstructionUtil util;
			if (SingletonUtil.InstanceAvailable(out util))
			{
				util.IncrementModifiedFixings();
			}
        }
    }
    public void Confirm()
    {
        MeshRenderer.material = confirmed_material;
        confirmed = true;
    }
}