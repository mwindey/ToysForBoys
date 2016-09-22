﻿using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFToysForBoys.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterAccount(object sender, RoutedEventArgs e)
        {
           try
            {
                if (newPasswordBox.Password == verifyPasswordBox.Password)
                {
                    IAdminService admin = new AdminService();
                    admin.Insert(new Admin() { username = usernameBox.Text.ToString().Trim(), password = newPasswordBox.Password.ToString().Trim() });
                    MessageBox.Show("User added to the database.", "Registration", MessageBoxButton.OK, MessageBoxImage.Information);
                    View.LoginWindow eenView = new View.LoginWindow();
                    eenView.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Passwords do not match!", "Password Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}