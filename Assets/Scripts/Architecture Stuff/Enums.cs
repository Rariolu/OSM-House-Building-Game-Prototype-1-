using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum CAMERA_DIR
{
    LEFT = -1,
    RIGHT = 1
}

public enum CONSTRUCTION_IMAGE_TYPE
{
    BLUEPRINT
}

public enum EXIT_STATE
{
    WIN,
    LOSE
}

/// <summary>
/// An enum representing the different types
/// of finished constructions.
/// </summary>
public enum FINISHED_CONSTRUCTION
{
    BUNGALOW,
    TOWNHOUSE,
    SEMI_DETACHED_HOUSE,
    DETACHED_HOUSE
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

public enum MATERIAL
{
    GENERIC,
    WOOD,
	BRICK
}

public enum PREFABTYPE
{
    PANEL_FRONT_GROUND,
    PANEL_BACK_GROUND,
    PANEL_SIDE_GROUND,
    PANEL_FLOOR_GROUND,
    PANEL_FRONT_SECOND,
    PANEL_WINDOW_CENTRE_SECOND,
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
    PANEL_WINDOW_RGHT,
    PANEL_WINDOW_LEFT_SECOND,
    PANEL_WINDOW_RIGHT_RIGHT,
    PANEL_C2_EXTERIOR,
    PANEL_C2_FRONT_DOOR,
    PANEL_C2_BACK_DOOR,
    PANEL_C2_WINDOW_CENTRE,
    PANEL_C2_WINDOW_LEFT,
    PANEL_C2_WINDOW_RIGHT,
    PANEL_C2_SML_ROOF
}

public enum PREFAB_COMPART
{
    OTHER,
    WALL,
    FLOOR,
    ROOF,
    NONE
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
    Fixings_MiniGame = 3,
    Name_Building = 4,
    EndScene = 5,
    Options = 6,
    How_To_Play = 7,
    Credits = 8,
    ResourceLoader = 9,
    SpecTestScene = 10
}

public enum SoundType
{
    SFX, Music, Master
}

public enum SNAP_POINT_TYPE
{
    EDGE,
    CENTRE,
    FLOOR,
    ROOF,
    NONE
}

public enum SOUNDNAME
{
	MENU_BUTTON_CLICK,
	SWIPE_RIGHT,
	SWIPE_LEFT,
    SELL_HOUSE,
    TURNING_PAGE,
    EXTRA_BUTTON_CLICK,
    FIXINGS,
    DESTRUCTION_0
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