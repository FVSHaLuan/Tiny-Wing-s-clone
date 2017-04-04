using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class HeightUpdater : MonoBehaviour
    {
        [SerializeField, Tooltip("unit/seconds")]
        float updateSpeed = 20.0f;

        IHeightModel heightModel;

        public void Awake()
        {
            heightModel = GameplayEntry.Instance.Model.Height;
        }

        public void Update()
        {
            heightModel.UpdateByDistance(updateSpeed * Time.deltaTime);
        }
    }

}