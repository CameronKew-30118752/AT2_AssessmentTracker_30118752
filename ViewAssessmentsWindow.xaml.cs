/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: ViewAssessmentsWindow.xaml.cs
 * 
 * Description:
 * This file contains the code for the View Assessments window.
 * It displays all saved assessments in a DataGrid using the 
 * shared AssessmentManager
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _30118752_AT2._3_GUI1
{
    /// <summary>
    /// Displays all assessments currently stores by the application
    /// </summary>
    public partial class ViewAssessmentsWindow : Window
    {
        // Stores the assessment manager shared by the other windows 
        private readonly AssessmentManager assessmentManager;

        /// <summary>
        /// Initialises the window and loads the assessments into the table
        /// </summary>
        /// <param name="manager">The shared assessment manager</param>
        public ViewAssessmentsWindow(AssessmentManager manager)
        {
            InitializeComponent();

            // Keep a reference to the shared assessment manager
            assessmentManager = manager;

            // Display the assessments in the DataGrid
            dgvAssessments.ItemsSource = assessmentManager.Assessments;
        }

        /// <summary>
        /// Closes this window and returns to the main menu 
        /// </summary>
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
