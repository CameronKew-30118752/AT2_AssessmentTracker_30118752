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
using System.Collections.Generic;

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
        /// Searches for an assessment using sequential search
        /// </summary>
        private void btnSearchAssessment_Click(object sender, RoutedEventArgs e)
        {
            // Get the assessment name entered by user
            string searchName = txtSearchAssessment.Text.Trim();

            // Check that search box is not empty
            if (String.IsNullOrWhiteSpace(searchName))
            {
                MessageBox.Show("Please enter an assessment name to search for");
                return;
            }

            // Search the assessment collection using sequential search
            Assessment? foundAssessment = assessmentManager.SequentialSearch(searchName);

            // Check whether the assessment was found
            if (foundAssessment != null)
            {
                // Display only the matching assessment
                dgvAssessments.ItemsSource = new List<Assessment> { foundAssessment };
            }
            else
            {
                // Display an empty table if no match found
                dgvAssessments.ItemsSource = null;

                MessageBox.Show("No assessment found with that name");
            }
        }

        /// <summary>
        /// Restores DataGrid to display all saved assessments
        /// </summary>
        private void btnShowAllAssessments_Click(object sender, RoutedEventArgs e)
        {
            // Clear search box
            txtSearchAssessment.Clear();

            // Restore the full assessment collection
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
