using UnityEngine;
using System.Collections;

namespace FH.Gameplay
{
    public class GameplayModel : MonoBehaviour
    {
        [SerializeField]
        HeightModel height;
        [SerializeField]
        Theme theme;
        [SerializeField]
        InputStatus inputStatus;

        public IHeightModel Height
        {
            get
            {
                return height;
            }
        }

        public Theme Theme
        {
            get
            {
                return theme;
            }
        }

        public InputStatus InputStatus
        {
            get
            {
                return inputStatus;
            }
        }
    }

}