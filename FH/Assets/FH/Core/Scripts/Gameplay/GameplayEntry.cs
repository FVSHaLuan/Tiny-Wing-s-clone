using UnityEngine;
using System.Collections;
using FH.Core.Architecture;
using System;
using FH.Core.Architecture.Pool;

namespace FH.Gameplay
{
    public class GameplayEntry : FirstWakeComponent
    {
        static GameplayEntry instance;

        [SerializeField]
        GameplayModel model;
        [SerializeField]
        PlayerEntry player;

        public MultiPrototypesPool<GeneralPoolMember> GeneralPool { get; private set; }

        public static GameplayEntry Instance
        {
            get
            {
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public GameplayModel Model
        {
            get
            {
                return model;
            }
        }

        public PlayerEntry Player
        {
            get
            {
                return player;
            }
        }

        public override void FirstAwake()
        {
            instance = this;
            GeneralPool = new GeneralPoolClass();
        }

        class GeneralPoolClass : MultiPrototypesPool<GeneralPoolMember> { }
    }
}
