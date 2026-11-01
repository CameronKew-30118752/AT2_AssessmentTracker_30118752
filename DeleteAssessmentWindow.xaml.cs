/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: DeleteAssessmentWindow.xaml.cs
 * 
 * Description:
 * This file contains the code for the Delete assessment window.
 * It displays saves assessments, allows users to select an assessment
 * for deletion and saves the updated list using AssessmentManager.
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
    /// Allows users to view and delete existing assessments
    /// </summary>
    public partial class DeleteAssessmentWindow : Window
    {
        // Store the shared assessment manager
        private readonly AssessmentManager assessmentManager;

        /// <summary>
        /// Initialises the window and displays the saved assessments
        /// </summary>
        /// <param name="manager">The shared assessment manager</param>
        public DeleteAssessmentWindow(AssessmentManager manager)
        {
            InitializeComponent();

            // Keep the manager shared by the other windows 
            assessmentManager = manager;

            // Display the assessments in the table
            dgvAssessments.ItemsSource = assessmentManager.Assessments;
        }

        private void btnDeleteAssessment_Click(object sender, RoutedEventArgs e)
        {
            // Check whether an assessment has been selected
            if (dgvAssessments.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select an assessment to delete.",
                    "No Assessment Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // Ask user to confirm deletion
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete the selected assessment?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            // Only delete the assessment if the user selects Yes
            if (result == MessageBoxResult.Yes)
            {
                Assessment selectedAssessment =
                    (Assessment)dgvAssessments.SelectedItem;

                // Remove the assessment from the shared list
                assessmentManager.Assessments.Remove(selectedAssessment);

                // Save the updated list to the text file
                assessmentManager.SaveAssessmentsToFile();

                // Refresh the table to show the remaining assessments
                dgvAssessments.ItemsSource = null;
                dgvAssessments.ItemsSource = assessmentManager.Assessments;

                // Show user confirmation of deleted assessment
                MessageBox.Show(
                    "Assessment deleted successfully",
                    "Deletion Complete",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Closes the window and returns to main menu
        /// </summary>
        private void btnReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
