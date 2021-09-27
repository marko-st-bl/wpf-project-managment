using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ProjectManagment.DataAccess;
using ProjectManagment.Models;
using ProjectManagment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectManagment.Views
{
    /// <summary>
    /// Interaction logic for MemberView.xaml
    /// </summary>
    public partial class MemberView : Window
    {
        public MemberView(User user)
        {
            _context = new SessionContext()
            {
                User = user
            };
            LoadSettings(user.Id);
            InitializeComponent();
            DataContext = new MemberViewModel(_context);
        }

        private SessionContext _context;
        private void LoadSettings(int user_id)
        {
            Settings settings = new MySqlSettings().GetSettingsByUserId(user_id);
            if (settings != null)
            {
                _context.Settings = settings;

                //set language
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(settings.Language);

                //set theme
                var paletteHelper = new PaletteHelper();
                var theme = paletteHelper.GetTheme();

                theme.SetBaseTheme(settings.DarkMode ? Theme.Dark : Theme.Light);
                IEnumerable<Swatch> swatches = new SwatchesProvider().Swatches;
                theme.SetPrimaryColor(swatches.Where(s => s.Name == settings.PrimaryColor).First().ExemplarHue.Color);

                paletteHelper.SetTheme(theme);
            }
        }
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
    }
}
