/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: MenuWindow.xaml.cs
 * 
 * Description:
 * This file contains the code for the main menu window.
 * It allows users to navigate to the View, Add, and 
 * Delete assessments screens.
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
    /// Displays the main menu and opens the selected assessment screen
    /// </summary>
    public partial class MenuWindow : Window
    {
        // Stores the shared assessment manager
        private readonly AssessmentManager assessmentManager;

        public MenuWindow(AssessmentManager manager)
        {
            InitializeComponent();

            // Keep the manager passed from the login window
            assessmentManager = manager;
        }

        /// <summary>
        /// Opens the View Assessments Screen
        /// </summary>
        private void btnViewAssessments_Click(object sender, RoutedEventArgs e)
        {
            ViewAssessmentsWindow viewWindow = new ViewAssessmentsWindow(assessmentManager);
            viewWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the Add Assessments Screen
        /// </summary>
        private void btnAddAssessment_Click(object sender, RoutedEventArgs e)
        {
            AddAssessmentWindow addWindow = new AddAssessmentWindow(assessmentManager);
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the Delete Assessments Screen
        /// </summary>
        private void btnDeleteAssessment_Click(Object sender, RoutedEventArgs e)
        {
            DeleteAssessmentWindow deleteWindow = new DeleteAssessmentWindow(assessmentManager);
            deleteWindow.ShowDialog();
        }
    }
}
