using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    public struct NodeData
    {
        private float height;
        private float positionX;
        Vector2 velocity;
        Extremeness extremeness;
        Extremeness lastExtremeness;
        int lastExtremeNodeInterval;

        public float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public float PositionX
        {
            get
            {
                return positionX;
            }

            set
            {
                positionX = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public Extremeness Extremeness
        {
            get
            {
                return extremeness;
            }

            set
            {
                extremeness = value;
            }
        }

        public Extremeness LastExtremeness
        {
            get
            {
                return lastExtremeness;
            }

            set
            {
                lastExtremeness = value;
            }
        }

        public int LastExtremeNodeInterval
        {
            get
            {
                return lastExtremeNodeInterval;
            }

            set
            {
                lastExtremeNodeInterval = value;
            }
        }

        public NodeData(float height, float positionX, Vector2 velocity, Extremeness extremeness, Extremeness lastExtremeness, int lastExtremeNodeInterval)
        {
            this.height = height;
            this.positionX = positionX;
            this.velocity = velocity;
            this.extremeness = extremeness;
            this.lastExtremeness = lastExtremeness;
            this.lastExtremeNodeInterval = lastExtremeNodeInterval;
        }
    }
}
