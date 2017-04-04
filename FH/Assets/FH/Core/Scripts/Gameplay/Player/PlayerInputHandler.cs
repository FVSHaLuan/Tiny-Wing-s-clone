using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField]
        float minForce = 5;
        [SerializeField]
        float maxForce = 10;
        [SerializeField]
        float timeToReachMaxForce = 1;

        new Rigidbody2D rigidbody2D;
        InputStatus inputStatus;

        public void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            inputStatus = GameplayEntry.Instance.Model.InputStatus;
        }

        void Update()
        {
            if (inputStatus.Holding)
            {
                float currentForce = Mathf.Lerp(minForce, maxForce, inputStatus.HeldTime / timeToReachMaxForce);
                rigidbody2D.AddForce(Vector2.down * currentForce, ForceMode2D.Impulse);
            }
        }
    }

}