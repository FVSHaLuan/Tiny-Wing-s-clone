using UnityEngine;
using System.Collections;

namespace FH.Gameplay.Controller
{
    public class HeightModelInitializer : MonoBehaviour
    {
        [SerializeField]
        HeightSpeedYModifer initialSpeedYModifer;
        [SerializeField]
        int numberOfInitialNodes = 100;

        public void Awake()
        {
            var height = GameplayEntry.Instance.Model.Height;
            height.SpeedYModifier = initialSpeedYModifer;
            height.UpdateByNodes(numberOfInitialNodes);
        }
    }

}