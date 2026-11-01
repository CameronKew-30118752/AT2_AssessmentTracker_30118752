/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: MainWindow.xaml.cs
 * 
 * Description:
 * This file contains the code for the login window.
 * It initialises the AssessmentManager, loads previously saved
 * assessments and opens the main menu when Login button is clicked.
 */

using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _30118752_AT2._3_GUI1
{
    /// <summary>
    /// Displays the login screen and initialises the assessment data
    /// </summary>
    public partial class MainWindow : Window
    {

        // Stores the assessment manager used throughout the application
        private readonly AssessmentManager assessmentManager;

        public MainWindow()
        {
            InitializeComponent();

            // Create the assessment manager and load saved assessments
            assessmentManager = new AssessmentManager();
            assessmentManager.LoadAssessmentsFromFile();
        }

        /// <summary>
        /// Opens the main menu when the login button is clicked
        /// </summary>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Create and display the main menu 
            MenuWindow menuWindow = new MenuWindow(assessmentManager);
            menuWindow.Show();

            //Close the login window
            this.Close();
        }
    }
}