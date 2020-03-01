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
    public MATERIAL material;
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
    public static bool operator==(Prefab p1, Prefab p2)
    {
        bool compartEqual = p1.compart == p2.compart;
        bool floorTypeEqual = p1.floorType == p2.floorType;
        bool snapTypeEqual = p1.snapType == p2.snapType;
        bool prefabTypeEqual = p1.type == p2.type;
        return compartEqual && floorTypeEqual && snapTypeEqual && prefabTypeEqual;
    }
    public static bool operator !=(Prefab p1, Prefab p2)
    {
        return !(p1 == p2);
    }
    public override bool Equals(object obj)
    {
        if (obj is Prefab)
        {
            return (Prefab)obj == this;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}