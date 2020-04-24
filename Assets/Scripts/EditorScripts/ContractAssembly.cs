using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

public class ContractAssembly : ScriptableObject
{
    [MenuItem("Tools/Contract/AutoAssemblyC1")]
    static void AutoAssembleContract1()
    {
        string xmlFile = "Assets\\Contracts\\Semi-Detached House_SEMI_DETACHED_HOUSE_0.xml";
        AutoAssembleContract(xmlFile);
    }

    [MenuItem("Tools/Contract/AutoAssemblyC2")]
    static void AutoAssembleContract2()
    {
        string xmlFile = "Assets\\Contracts\\Detached House_DETACHED_HOUSE_0.xml";
        AutoAssembleContract(xmlFile);
    }
    
    static void AutoAssembleContract(string xmlFile)
    {
        Contract contract = null;
        if(EditorUtility.DisplayDialog("Load XML?", "Load contract from XML file?", "YES", "NO"))
        {
            XMLUtil.LoadContract(xmlFile, out contract);
        }
        else
        {
            ConstructionUtil util;
            if (SingletonUtil.InstanceAvailable(out util))
            {
                contract = util.Contract;
            }
        }
        if (contract != null)
        {
            foreach(PrefabCollection prefab in contract.prefabCollections)
            {
                foreach(Vector3 pos in prefab.positionsTakenWithinContract)
                {
                    PrefabPlacedObject ppo = new PrefabPlacedObject(prefab.prefab, pos);
                    PrefabCounter counter;
                    if (SingletonUtil.InstanceAvailable(out counter))
                    {
                        counter.DecrementCount(prefab.prefab);
                    }
                }
            }
        }
    }
}

#endif