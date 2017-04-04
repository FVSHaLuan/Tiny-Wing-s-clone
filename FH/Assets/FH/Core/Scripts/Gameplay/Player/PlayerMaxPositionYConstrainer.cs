using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    public class PlayerMaxPositionYConstrainer : MonoBehaviour
    {
        [SerializeField]
        float maxPositionY = 15.0f;

        void Update()
        {
            if (transform.position.y >= maxPositionY)
            {
                var p = transform.position;
                p.y = maxPositionY;
                transform.position = p;
            }
        }
    }

}