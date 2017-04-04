using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMinVelocityXConstrainer : MonoBehaviour
    {
        [SerializeField]
        float minVelocityX = 5;

        new Rigidbody2D rigidbody2D;

        public void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (rigidbody2D.velocity.x <= minVelocityX)
            {
                var p = rigidbody2D.velocity;
                p.x = minVelocityX;
                rigidbody2D.velocity = p;
            }
        }
    }

}