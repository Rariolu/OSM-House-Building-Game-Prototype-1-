using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CONSTRUCTION_IMAGE_TYPE
{
    BLUEPRINT
}

/// <summary>
/// An enum representing the different types
/// of finished constructions.
/// </summary>
public enum FINISHED_CONSTRUCTION
{
    BUNGALOW,
    TOWNHOUSE,
    SEMI_DETACHED_HOUSE
}

public enum FIXINGSECTION
{
    ONE,
    TWO,
    THREE,
    FOUR,
    FIVE
}

/// <summary>
/// An enum representing which floor of
/// the building a particular prefab is attached to.
/// </summary>
public enum FLOORTYPE
{
    GROUND_FLOOR = 0,
    FIRST_FLOOR = 1,
    SECOND_FLOOR = 2
}

public enum LAYER
{
    DEFAULT = 4,
    IntersectionLayer = 9
}

public enum PREFABTYPE
{
    PANEL_FRONT_GROUND,
    PANEL_BACK_GROUND,
    PANEL_SIDE_GROUND,
    PANEL_FLOOR_GROUND,
    PANEL_FRONT_SECOND,
    PANEL_BACK_SECOND,
    PANEL_SIDE_SECOND,
    PANEL_FLOOR_SECOND,
    PANEL_ROOF,
    PANEL_BEDROOM,
    PANEL_BATHROOM,
    PANEL_KITCHEN,
    PANEL_DOOR_GROUND,
    PANEL_PORCH_GROUND,
    PANEL_EXTRUDE_DOOR_GROUND,
    PANEL_DOOR2_GROUND,
    PANEL_FRONT2_GROUND,
    PANEL_STAIRS1_GROUND,
    PANEL_SLANT_ROOF_GROUND,
    PANEL_FRONT_WINDOW_GROUND,
    PANEL_INTERIOR_GROUND,
    PANEL_BACK_DOOR_GROUND,
    PANEL_FRONT_OFFSET_WINDOW_FIRST,
    PANEL_INTERIOR_DOOR,
    PANEL_ROOF1,
    PANEL_WINDOW_LEFT,
    PANEL_WINDOW_RGHT
}

public enum PREFAB_COMPART
{
    OTHER,
    WALL,
    FLOOR,
    ROOF,
}

public enum PREFAB_POSITION
{
    EXTERIOR,
    INTERIOR
}

/// <summary>
/// An enum representing all the different
/// "scenes" that can be loaded by the Unity
/// Engine.
/// </summary>
public enum SCENE
{
    Menu = 0,
    ContractSelection = 1,
    InGame = 2,
    InGameUI = 3,
    Fixings_MiniGame = 4,
    EndScene = 5,
    Options = 6,
    Intermediate = 7,
    Brochure = 8,
    HouseViewing = 9,
    Name_Building = 10,
    SpecTestScene = 11
}

public enum SoundType
{
    SFX, Music
}

public enum SNAP_POINT_TYPE
{
    EDGE,
    CENTRE,
    FLOOR,
    ROOF
}

public enum STANDARDTYPE
{
    SAFETY,
    WATER_TIGHTNESS,
    DESIGN
}

public enum TAG
{
    spawnedPrefab,
    TESTTAG
}

public enum TASKTYPE
{
    BEDROOM_QUANTITY,
    HOUSE_WIDTH,
    HOUSE_HEIGHT,
    HOUSE_DEPTH
}