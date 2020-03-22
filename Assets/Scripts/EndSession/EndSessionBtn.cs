using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSessionBtn: UIButton
{
	public int fixingsLeeway = 10;

    private int minimumFixtures;
    private int contractFixtures;
    private int noOfFixings = 37;

    protected override void Start()
	{
		base.Start();
		Click += EndSessionButton_Click;
	}

    public void EndSessionButton_Click(UIButton sender)
	{
        minimumFixtures = noOfFixings - fixingsLeeway;

        if (Fixings.fixings >= minimumFixtures)
		{
		    UnityEngine.SceneManagement.SceneManager.LoadScene("Name_Building");
		}
		else
		{
			Logger.Log("BuildingCollapsed");
		}	
	}	
}