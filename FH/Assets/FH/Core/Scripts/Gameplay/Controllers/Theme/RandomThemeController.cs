using UnityEngine;

namespace FH.Gameplay
{
    public class RandomThemeController : MonoBehaviour
    {
        const float InitialPosition = -9999;

        [SerializeField]
        int numberOfThemes = 5;

        Theme theme;

        public void Awake()
        {
            theme = GameplayEntry.Instance.Model.Theme;
            SetInitialThemeMark();
            theme.OnThemeChanged += Theme_OnThemeChanged;
        }

        void Theme_OnThemeChanged(float positionX)
        {
            int lastThemeId = theme.LastTheme;
            int themeId = Random.Range(0, numberOfThemes);
            while (themeId == lastThemeId)
            {
                themeId = Random.Range(0, numberOfThemes);
            }

            theme.SetThemeMark(positionX, themeId);
        }

        void SetInitialThemeMark()
        {
            theme.SetThemeMark(InitialPosition, Random.Range(0, numberOfThemes));
        }
    }

}