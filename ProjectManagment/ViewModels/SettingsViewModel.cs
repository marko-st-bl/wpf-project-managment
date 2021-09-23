using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectManagment.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(SessionContext context)
        {
            Context = context;

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;

            if (theme is Theme internalTheme)
            {

                var colorAdjustment = internalTheme.ColorAdjustment ?? new ColorAdjustment();
            }
            IThemeManager themeManager = paletteHelper.GetThemeManager();
            if (themeManager != null)
            {
                themeManager.ThemeChanged += (_, e) =>
                {
                    IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
                };
            }

            Languages = new ObservableCollection<Language>(new[]{
                new Language()
                {
                    Title = "English",
                    Code = "en"
                },
                new Language()
                {
                    Title = "Srpski",
                    Code = "sr"
                }
            });
            if(context.Settings != null)
            {
                _selectedLanguage = Languages.Where(l => l.Code == context.Settings.Language).First();
                IsPurpleChecked = context.Settings.PrimaryColor == "deeppurple";
                IsRedChecked = context.Settings.PrimaryColor == "red";
                IsBlueChecked = context.Settings.PrimaryColor == "blue";
            }

            SaveSettingsCommand = new AnotherCommandImplementation(o => SaveSettings());

        }
        public SessionContext Context { get; set; }
        public ObservableCollection<Language> Languages { get; set; }
        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if(SetProperty(ref _selectedLanguage, value))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(value.Code);
                }
            }
        }
        public static IEnumerable<Swatch> Swatches { get; } = new SwatchesProvider().Swatches;
        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary(Swatches.Where(s => s.Name == (string)o).First()));
        public ICommand SaveSettingsCommand { get; }
        private static void ApplyPrimary(Swatch swatch)
            => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if(SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }
        public bool IsPurpleChecked { get; set; }
        public bool IsRedChecked { get; set; }
        public bool IsBlueChecked { get; set; }

        public void SaveSettings()
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            MySqlSettings mySqlSettings = new MySqlSettings();
            Context.Settings = new Settings()
            {
                User_Id = Context.User.Id,
                DarkMode = IsDarkTheme,
                Language = SelectedLanguage.Code,
                PrimaryColor = IsPurpleChecked ? "deeppurple" : IsRedChecked ? "red" : "blue"
        };
            mySqlSettings.SaveSettings(Context.Settings);
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}
