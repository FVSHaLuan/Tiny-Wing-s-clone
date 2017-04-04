using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.Pool
{
    public class GeneralPoolMember : MultiPrototypesPoolMemeberMonoBehavior<GeneralPoolMember>
    {
        public void TryReturnToPool()
        {
            if (Pool != null)
            {
                Pool.PushInstance(this);
            }
        }
    }

}