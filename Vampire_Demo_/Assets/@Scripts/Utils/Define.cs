using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum Scene
    {
        Unknown,
        DevScene,
        GameScene,
    }

    public enum Sound
    {
        BGM,
        Effect,
    }

    public enum ObjectType
    {
        Player,
        Monster,
        Projectile,
        Env,
    }

    public enum SkillType
    {
        None,
        Sequence,
        Repeat,
    }

    public enum StageType
    {
        Normal,
        Boss,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead
    }

    public const int VINE_ID = 1;
    public const int MUD_ID = 2;
    public const int BOSS_ID = 3;

    public const int PLAYER_DATA_ID = 1;
    public const string EXP_GEM_PREFAB = "Expgem.prefab";

    public const int EGO_SWORD_ID = 10;
    public const int FIRE_BALL_ID = 1;
}
