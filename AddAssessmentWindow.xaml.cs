/* Project: Assessment Tracker
 * Cameron Kew
 * 30118752
 * C#One 
 * AT2.3
 * File: AddAssessmentWindow.xaml.cs
 * 
 * Description:
 * This file contains the code for the Add assessment window.
 * It collects assessment information from the user, validates the
 * required fields and saves new assessments using AssessmentManager.
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
    /// Allows users to enter and save a new assessment
    /// </summary>
    public partial class AddAssessmentWindow : Window
    {

        //Stores the shared assessment manager
        private readonly AssessmentManager assessmentManager;

        /// <summary>
        /// Initialises the window with the shared assessment manager
        /// </summary>
        /// <param name="manager">The assessment manager is used by the application</param>
        public AddAssessmentWindow(AssessmentManager manager)
        {
            InitializeComponent();
            assessmentManager = manager;
        }

        /// <summary>
        /// Validates and adds the assessment entered by the user
        /// </summary>
        private void btnAddAssessment_Click(object sender, RoutedEventArgs e)
        {
            //Check that all the required fields have been completed
            if (string.IsNullOrWhiteSpace(txtUnitName.Text) ||
                string.IsNullOrWhiteSpace(txtAssessmentName.Text) ||
                cmbAssessmentType.SelectedItem == null ||
                dpDueDate.SelectedDate == null)
            {
                MessageBox.Show(
                    "Please complete all fields before adding assessment.",
                    "Missing Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // Get the selected assessment type from the dropdown
            ComboBoxItem selectedType =
                (ComboBoxItem)cmbAssessmentType.SelectedItem;

            // Create a new assessment using the entered information
            Assessment newAssessment = new Assessment
            {
                UnitName = txtUnitName.Text.Trim(),
                AssessmentName = txtAssessmentName.Text.Trim(),
                AssessmentType = selectedType.Content?.ToString() ??"",
                DueDate = dpDueDate.SelectedDate.Value
            };

            // Add the assessment to the shared list 
            assessmentManager.Assessments.Add(newAssessment);

            // Save all assessments to the text file
            assessmentManager.SaveAssessmentsToFile();

            // Confirm that the assessment was saved
            MessageBox.Show(
                "Assessment saved successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

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
