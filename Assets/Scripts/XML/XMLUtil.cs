#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005
#pragma warning disable IDE1006

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

public static class XMLUtil
{
    #region consts
    const string contract = "contract";
    const string contractName = "name";
    const string finishedconstruction = "finishedconstruction";
    const string budget = "budget";
    const string fixtures = "fixtures";
    const string positionstaken = "positionstaken";
    const string vec3 = "vec3";
    const string prefabcollections = "prefabcollections";
    const string prefabcollection = "prefabcollection";
    const string prefaboffset = "prefaboffset";
    const string properties = "properties";
    const string snapType = "snaptype";
    const string prefab = "prefab";
    const string positions = "positions";
    const string position = "position";
    const string quantity = "quantity";
    const string rotation = "rotation";
    const string scale = "scale";
    const string standard = "standard";
    const string rating = "rating";
    const string type = "type";
    const string prefabtype = "prefabtype";
    const string standards = "standards";
    const string number = "number";
    const string tasks = "tasks";
    const string task = "task";
    const string floortype = "floortype";
    const string compart = "compart";
    const string prefabPosition = "prefabposition";
    const string time = "time";
    #endregion

    /// <summary>
    /// Save a contract in the given directory.
    /// </summary>
    /// <param name="xmlDirectory">The folder to save the xml file in.</param>
    /// <param name="contract"></param>
    /// <param name="aggr">Text to be appended to the file name.</param>
    public static void SaveContract(string xmlDirectory, Contract contract, string aggr = "")
    {
        if (!Directory.Exists(xmlDirectory))
        {
            Debug.LogFormat("\"{0}\" doesn't exist.", xmlDirectory);
            return;
        }
        string file = "{0}\\{1}_{2}_{3}.xml".FormatText(xmlDirectory, contract.name, contract.finishedConstruction, aggr);
        Debug.Log(file);
        XmlWriterSettings config = new XmlWriterSettings();
        config.Indent = true;
        config.IndentChars = "\t";

        XmlWriter xmlWriter = XmlWriter.Create(file, config);

        xmlWriter.WriteStartDocument();

        WriteContract(ref xmlWriter, contract);

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }

    /// <summary>
    /// Load a contract from the given file path.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="contract"></param>
    /// <returns></returns>
    public static bool LoadContract(string file, out Contract contract)
    {
        contract = null;
        if (!File.Exists(file) || !file.EndsWith(".xml"))
        {
            return false;
        }

        contract = new Contract();

        XmlReader xmlReader = XmlReader.Create(file);
        while (xmlReader.Read())
        {
            if (xmlReader.IsStartElement(contractName))
            {
                xmlReader.Read();
                string name = xmlReader.Value;
                contract.name = name;
            }

            if (xmlReader.IsStartElement(finishedconstruction))
            {
                xmlReader.Read();
                string fcon = xmlReader.Value;
                FINISHED_CONSTRUCTION fconEnum;
                if (Util.EnumTryParse(fcon, out fconEnum))
                {
                    contract.finishedConstruction = fconEnum;
                }
            }

            if (xmlReader.IsStartElement(budget))
            {
                xmlReader.Read();
                string b = xmlReader.Value;
                int cBudget;
                if (int.TryParse(b, out cBudget))
                {
                    contract.budget = cBudget;
                }
            }

            if (xmlReader.IsStartElement(fixtures))
            {
                xmlReader.Read();
                string f = xmlReader.Value;
                int fixs;
                if (int.TryParse(f, out fixs))
                {
                    contract.fixtures = fixs;
                }
            }

            if (xmlReader.IsStartElement(positionstaken))
            {
                XmlReader positionsTakenReader = xmlReader.ReadSubtree();
                contract.positionsTaken = ReadVec3Array(positionsTakenReader);
            }

            if (xmlReader.IsStartElement(prefabcollections))
            {
                XmlReader prefabCols = xmlReader.ReadSubtree();
                contract.prefabCollections = ReadPrefabCollectionArray(prefabCols);
            }

            if (xmlReader.IsStartElement(standards))
            {
                XmlReader standardsSubtree = xmlReader.ReadSubtree();
                contract.standards = ReadStandardArray(standardsSubtree);
            }

            if (xmlReader.IsStartElement(tasks))
            {
                XmlReader tasksSubTree = xmlReader.ReadSubtree();
                contract.tasks = ReadTasks(tasksSubTree);
            }

            if (xmlReader.IsStartElement(time))
            {
                xmlReader.Read();
                string strTime = xmlReader.Value;
                uint time;
                if (uint.TryParse(strTime,out time))
                {
                    contract.time = time;
                }
            }
        }

        xmlReader.Close();
        return true;
    }

    #region XMLReading

    static PrefabOffsetProperties ReadOffset(XmlReader subtree)
    {
        PrefabOffsetProperties pop = new PrefabOffsetProperties();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(position))
            {
                pop.offsetPosition = ReadVec3(subtree);
            }
            if (subtree.IsStartElement(rotation))
            {
                pop.offsetRotation = ReadVec3(subtree);
            }
            if (subtree.IsStartElement(scale))
            {
                pop.offsetScale = ReadVec3(subtree);
            }
        }
        return pop;
    }

    static Prefab ReadPrefab(XmlReader subtree)
    {
        Prefab prefab = new Prefab();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(prefaboffset))
            {
                XmlReader offsetSubtree = subtree.ReadSubtree();
                prefab.offset = ReadOffset(offsetSubtree);
            }
            if (subtree.IsStartElement(properties))
            {
                XmlReader propSubtree = subtree.ReadSubtree();
                prefab.properties = ReadStandardArray(propSubtree);
            }
            if (subtree.IsStartElement(snapType))
            {
                subtree.Read();
                string st = subtree.Value;
                SNAP_POINT_TYPE spt;
                if (Util.EnumTryParse(st, out spt))
                {
                    prefab.snapType = spt;
                }
            }
            if (subtree.IsStartElement(prefabtype))
            {
                subtree.Read();
                string strPT = subtree.Value;
                PREFABTYPE pt;
                if (Util.EnumTryParse(strPT, out pt))
                {
                    prefab.type = pt;
                }
            }
            if (subtree.IsStartElement(floortype))
            {
                subtree.Read();
                string strFT = subtree.Value;
                FLOORTYPE ft;
                if (Util.EnumTryParse(strFT, out ft))
                {
                    prefab.floorType = ft;
                }
            }
            if (subtree.IsStartElement(compart))
            {
                subtree.Read();
                string strCompart = subtree.Value;
                PREFAB_COMPART pc;
                if (Util.EnumTryParse(strCompart, out pc))
                {
                    prefab.compart = pc;
                }
            }
            if (subtree.IsStartElement(prefabPosition))
            {
                subtree.Read();
                string strPrefabPosition = subtree.Value;
                PREFAB_POSITION pp;
                if (Util.EnumTryParse(strPrefabPosition, out pp))
                {
                    prefab.position = pp;
                }
            }
        }
        return prefab;
    }

    static PrefabCollection ReadPrefabCollection(XmlReader subtree)
    {
        PrefabCollection pc = new PrefabCollection();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(prefab))
            {
                XmlReader prefabSubtree = subtree.ReadSubtree();
                pc.prefab = ReadPrefab(prefabSubtree);
            }
            if (subtree.IsStartElement(positions))
            {
                XmlReader posSubTree = subtree.ReadSubtree();
                pc.positionsTakenWithinContract = ReadVec3Array(posSubTree);
            }
            if (subtree.IsStartElement(quantity))
            {
                subtree.Read();
                string strQuan = subtree.Value;
                int quan;
                if (int.TryParse(strQuan, out quan))
                {
                    pc.quantity = quan;
                }
            }
        }
        return pc;
    }

    static PrefabCollection[] ReadPrefabCollectionArray(XmlReader subtree)
    {
        List<PrefabCollection> prefabCollections = new List<PrefabCollection>();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(prefabcollection))
            {
                XmlReader prefabCollectionSubtree = subtree.ReadSubtree();
                prefabCollections.Add(ReadPrefabCollection(prefabCollectionSubtree));
            }
        }
        return prefabCollections.ToArray();
    }

    static Standard ReadStandard(XmlReader subtree)
    {
        Standard standard = new Standard();
        string strR = subtree[rating];
        float r;
        if (float.TryParse(strR, out r))
        {
            standard.rating = r;
        }
        string strType = subtree[type];
        STANDARDTYPE t;
        if (Enum.TryParse(strType, out t))
        {
            standard.type = t;
        }
        return standard;
    }

    static Standard[] ReadStandardArray(XmlReader subtree)
    {
        List<Standard> standards = new List<Standard>();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(standard))
            {
                standards.Add(ReadStandard(subtree));
            }
        }
        return standards.ToArray();
    }

    static Task ReadTask(XmlReader subtree)
    {
        Task task = new Task();
        string strNum = subtree[number];
        string strType = subtree[type];
        if (!int.TryParse(strNum, out task.number))
        {
            Debug.Log("Task number not parsed.");
        }
        if (!Util.EnumTryParse(strType, out task.type))
        {
            Debug.LogFormat("\"{0}\" not parsed", strType);
        }
        return task;
    }

    static Task[] ReadTasks(XmlReader subtree)
    {
        List<Task> tasks = new List<Task>();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(task))
            {
                tasks.Add(ReadTask(subtree));
            }
        }
        return tasks.ToArray();
    }

    static Vector3 ReadVec3(XmlReader reader)
    {
        string strX = reader["x"];
        string strY = reader["y"];
        string strZ = reader["z"];
        float x, y, z;
        float.TryParse(strX, out x);
        float.TryParse(strY, out y);
        float.TryParse(strZ, out z);
        return new Vector3(x, y, z);
    }

    static Vector3[] ReadVec3Array(XmlReader subtree)
    {
        List<Vector3> vec3s = new List<Vector3>();
        while (subtree.Read())
        {
            if (subtree.IsStartElement(vec3))
            {
                vec3s.Add(ReadVec3(subtree));
            }
        }
        return vec3s.ToArray();
    }

    #endregion

    #region XMLWriting

    /// <summary>
    /// Use the given XmlWriter to serialise the given contract (saving it to the associated XML file).
    /// </summary>
    /// <param name="xmlWriter"></param>
    /// <param name="contract"></param>
    static void WriteContract(ref XmlWriter xmlWriter, Contract contract)
    {
        xmlWriter.WriteStartElement(XMLUtil.contract);

        xmlWriter.WriteStartElement(contractName);
        xmlWriter.WriteValue(contract.name);
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(finishedconstruction);
        xmlWriter.WriteValue(contract.finishedConstruction.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(budget);
        xmlWriter.WriteValue(contract.budget);
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(fixtures);
        xmlWriter.WriteValue(contract.fixtures);
        xmlWriter.WriteEndElement();

        WriteVector3Array(ref xmlWriter, positionstaken, contract.positionsTaken);

        WritePrefabCollections(ref xmlWriter, prefabcollections, contract.prefabCollections);

        WriteStandardArray(ref xmlWriter, standards, contract.standards);

        WriteTaskArray(ref xmlWriter, tasks, contract.tasks);

        xmlWriter.WriteStartElement(time);
        xmlWriter.WriteValue(contract.time.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
    }

    static void WritePrefab(ref XmlWriter xmlWriter, Prefab prefab)
    {
        xmlWriter.WriteStartElement(XMLUtil.prefab);

        WritePrefabOffset(ref xmlWriter, prefab.offset);

        WriteStandardArray(ref xmlWriter, properties, prefab.properties);

        xmlWriter.WriteStartElement(snapType);
        xmlWriter.WriteValue(prefab.snapType.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(prefabtype);
        xmlWriter.WriteValue(prefab.type.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(floortype);
        xmlWriter.WriteValue(prefab.floorType.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(compart);
        xmlWriter.WriteValue(prefab.compart.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement(prefabPosition);
        xmlWriter.WriteValue(prefab.position.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
    }

    static void WritePrefabCollection(ref XmlWriter xmlWriter, PrefabCollection prefabCollection)
    {
        xmlWriter.WriteStartElement(prefabcollection);

        WritePrefab(ref xmlWriter, prefabCollection.prefab);

        WriteVector3Array(ref xmlWriter, positions, prefabCollection.positionsTakenWithinContract);

        xmlWriter.WriteStartElement(quantity);
        xmlWriter.WriteValue(prefabCollection.quantity);
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
    }

    static void WritePrefabCollections(ref XmlWriter xmlWriter, string elementName, PrefabCollection[] prefabCollections)
    {
        xmlWriter.WriteStartElement(elementName);

        foreach (PrefabCollection prefabCollection in prefabCollections)
        {
            WritePrefabCollection(ref xmlWriter, prefabCollection);
        }

        xmlWriter.WriteEndElement();
    }

    static void WritePrefabOffset(ref XmlWriter xmlWriter, PrefabOffsetProperties offset)
    {
        xmlWriter.WriteStartElement(prefaboffset);

        WriteVector3(ref xmlWriter, position, offset.offsetPosition);
        WriteVector3(ref xmlWriter, rotation, offset.offsetRotation);
        WriteVector3(ref xmlWriter, scale, offset.offsetScale);

        xmlWriter.WriteEndElement();
    }

    static void WriteStandardArray(ref XmlWriter xmlWriter, string elementName, Standard[] standards)
    {
        xmlWriter.WriteStartElement(elementName);

        foreach (Standard standard in standards)
        {
            xmlWriter.WriteStartElement(XMLUtil.standard);
            xmlWriter.WriteAttributeString(rating, standard.rating.ToString());
            xmlWriter.WriteAttributeString(type, standard.type.ToString());
            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();
    }

    static void WriteTaskArray(ref XmlWriter xmlWriter, string elementName, Task[] tasks)
    {
        xmlWriter.WriteStartElement(elementName);

        foreach (Task task in tasks)
        {
            xmlWriter.WriteStartElement(XMLUtil.task);
            xmlWriter.WriteAttributeString(number, task.number.ToString());
            xmlWriter.WriteAttributeString(type, task.type.ToString());
            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();
    }

    static void WriteVector3(ref XmlWriter xmlWriter, string elementName, Vector3 vec3)
    {
        xmlWriter.WriteStartElement(elementName);

        xmlWriter.WriteAttributeString("x", vec3.x.ToString());
        xmlWriter.WriteAttributeString("y", vec3.y.ToString());
        xmlWriter.WriteAttributeString("z", vec3.z.ToString());

        xmlWriter.WriteEndElement();
    }

    static void WriteVector3Array(ref XmlWriter xmlWriter, string elementName, Vector3[] vec3Array)
    {
        xmlWriter.WriteStartElement(elementName);

        foreach (Vector3 vec3 in vec3Array)
        {
            WriteVector3(ref xmlWriter, XMLUtil.vec3, vec3);
        }

        xmlWriter.WriteEndElement();
    }
    #endregion
}