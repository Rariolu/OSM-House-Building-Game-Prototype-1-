using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public struct Prefab
{
    public PREFAB_COMPART compart;
    public FLOORTYPE floorType;
    public PrefabOffsetProperties offset;
    public PREFAB_POSITION position;
    public Standard[] properties;
    public SNAP_POINT_TYPE snapType;
    public PREFABTYPE type;
    public override string ToString()
    {
        StringBuilderPro sb = new StringBuilderPro();
        sb.AppendLineFormat("Prefab Type: {0};", type);
        sb.AppendLineFormat("Snap Type: {0};", snapType);
        for(int i = 1; i <= properties.Length; i++)
        {
            sb.AppendLineFormat("Property {0}: {1};", i, properties[i - 1]);
        }
        return sb.ToString();
    }
}