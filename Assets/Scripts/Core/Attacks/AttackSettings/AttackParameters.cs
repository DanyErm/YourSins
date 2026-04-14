using System;

namespace Core.Attacks.AttackSettings
{
    [Serializable]
    public struct AttackParameters
    {
        public float DelayTime;
        public float FirstSpawnParameter;
        public float SecondSpawnParameter;
        public ProjectileType ProjectileType;
        public AttackLocation AttackLocation;
    }

    public enum ProjectileType
    {
        boneShard,
        chasingPacman,
        circlingPacman,
        face,
        spinningBone,
        spinningKnife,
        straightFlyingBone,
        straightFlyingKnife
    }

    public enum AttackLocation
    {
        arenaBot,
        arenaLeft,
        arenaRight,
        arenaTop,
        chasing,
        nextToArena,
        nextToChar,
        random
    }
}