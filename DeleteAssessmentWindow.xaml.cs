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

        /// <summary>
        /// Searches for an assessment using binary search 
        /// </summary>
        private void btnSearchAssessment_Click(object sender, RoutedEventArgs e)
        {
            // get assessment name entered by user
            string searchName = txtSearchAssessment.Text.Trim();

            // Check that the search box is not empty
            if (string.IsNullOrWhiteSpace(searchName))
            {
                MessageBox.Show("Please enter an assessment name to search for ");
                return;
            }

            // Search for the assessment using binary search
            Assessment? foundAssessment = BinarySearch(searchName);

            // Check whether an assessment was found 
            if (foundAssessment != null)
            {
                // Display only the matching assessment
                dgvAssessments.ItemsSource = new List<Assessment> { foundAssessment };

                // Automatically select the matching assessment
                dgvAssessments.SelectedItem = foundAssessment;
            }
            else
            {
                // Clear the table if no matching assessment is found 
                dgvAssessments.ItemsSource = null;

                MessageBox.Show("No assessments were found with that name");
            }
        }

        /// <summary>
        /// Restores datagrid to display all saved assessments 
        /// </summary>
        private void btnShowAllAssessments_Click(object sender, RoutedEventArgs e)
        {
            // Clear the search boxx 
            txtSearchAssessment.Clear();

            // Restore the complete assessment collection 
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
        /// Searches for an assessment by name using binary search 
        /// </summary>
        private Assessment? BinarySearch(string assessmentName)
        {
            // Create a temporary copy of the assessment collection
            List<Assessment> sortedAssessments =
                new List<Assessment>(assessmentManager.Assessments);

            // Sort the temporary collection alphabetically by assessment name
            sortedAssessments.Sort((a, b) =>
                 string.Compare(a.AssessmentName, b.AssessmentName,
                 StringComparison.OrdinalIgnoreCase));

            // Set the starting and ending positions of the search
            int first = 0;
            int last = sortedAssessments.Count - 1;

            // Continue searching while there are items to check
            while (first <= last)
            {
                //Find the middle position of the current search range
                int middle = first + (last - first) / 2;

                // Compare the middle assessment name with the search name
                int comparison = string.Compare(
                    sortedAssessments[middle].AssessmentName,
                    assessmentName,
                    StringComparison.OrdinalIgnoreCase);

                // Return the assessment if a match is found
                if (comparison == 0)
                {
                    return sortedAssessments[middle];
                }
                // search the righgt half if the name comes after the middle name
                else if (comparison < 0)
                {
                    first = middle + 1;
                }
                // otherwise search left half 
                else
                {
                    last = middle - 1;
                }
            }

            // Return null if no matching assessment found 
            return null;
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
