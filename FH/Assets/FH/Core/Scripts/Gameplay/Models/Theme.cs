using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Gameplay
{
    public class Theme : MonoBehaviour
    {
        [System.Serializable]
        struct ThemeMark
        {
            public float positionX;
            public int themeId;
        }
       
        ThemeMark lastThemeMark;

        public event ThemeChangedHandler OnThemeChanged;
        public delegate void ThemeChangedHandler(float positionX);

        List<ThemeMark> themeMarks = new List<ThemeMark>();

        public int LastTheme
        {
            get
            {
                return lastThemeMark.themeId;
            }
        }

        public int GetThemeAt(float x)
        {
            int themeId = lastThemeMark.themeId;
            for (int i = 0; i < themeMarks.Count; i++)
            {
                ThemeMark currentThemeMark = themeMarks[i];

                if (x >= currentThemeMark.positionX)
                {
                    themeId = currentThemeMark.themeId;
                }
                else
                {
                    break;
                }
            }

            return themeId;
        }

        public void SetThemeMark(float positionX, int themeId)
        {
            Assert.IsTrue(themeMarks.Count == 0 || positionX > lastThemeMark.positionX);
            ThemeMark themeMark = new ThemeMark() { themeId = themeId, positionX = positionX };
            themeMarks.Add(themeMark);
            lastThemeMark = themeMark;
        }

        public void ChangeTheme(float positionX)
        {
            if (OnThemeChanged != null)
            {
                OnThemeChanged(positionX);
            }
        }

    }

}