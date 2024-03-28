using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    [SerializeField] private int _englishIndex = 0;
    [SerializeField] private int _russianIndex = 1;
    [SerializeField] private int _turkishIndex = 2;
    public void ChangeLanguageToEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_englishIndex];
        EventsManager.Instance.OnLanguageChangedEvent();
    }

    public void ChangeLanguageToRussian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_russianIndex];
        EventsManager.Instance.OnLanguageChangedEvent();
    }
    public void ChangeLanguageToTurkish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_turkishIndex];
        EventsManager.Instance.OnLanguageChangedEvent();
    }
}