#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE1005

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script which manages main activity within the "Brochure" scene.
/// </summary>
public class BrochureSceneScript : MonoBehaviour
{
    public BackButton btnBack;
    Contract contract = new Contract();
    public Text lblBudget;
    public Text lblPrefabs;
    public Text lblStandards;
    public Text lblTasks;
    SCENE parentScene;
    public Image pbBlueprint;
    void Awake()
    {
        BrochureUtil util;
        if (BrochureUtil.InstanceAvailable(out util))
        {
            contract = util.DemoContract;
            parentScene = util.ParentScene;
            SceneObjectScript pScene;
            if (SceneObjectScript.InstanceExists(parentScene,out pScene))
            {
                pScene.SetActive(false);
            }
            if (parentScene == SCENE.InGame)
            {
                SceneObjectScript gameUIScene;
                if (SceneObjectScript.InstanceExists(SCENE.InGameUI, out gameUIScene))
                {
                    gameUIScene.SetActive(false);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (contract != null)
        {
            if (btnBack != null)
            {
                btnBack.previousScene = parentScene;
            }
            if (lblBudget != null)
            {
                lblBudget.text = "Budget: {0};".FormatText(contract.budget);
            }
            if (pbBlueprint != null)
            {
                Sprite sprite;
                if (ResourceManager.GetItem("{0}_{1}".FormatText(contract.finishedConstruction, CONSTRUCTION_IMAGE_TYPE.BLUEPRINT), out sprite))
                {
                    pbBlueprint.sprite = sprite;
                }
            }
            if (lblPrefabs != null)
            {
                lblPrefabs.text = contract.prefabCollections.GetCollectionString("Prefabs:");
            }
            if (lblStandards != null)
            {
                lblStandards.text = contract.standards.GetCollectionString("Standards:");
            }
            if (lblTasks != null)
            {
                lblTasks.text = contract.tasks.GetCollectionString("Tasks:");
            }
        }
    }
}