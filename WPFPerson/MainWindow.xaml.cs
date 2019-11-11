using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPerson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> persons = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person person = ListView.SelectedItem as Person;
            foreach (var lev in GroupBoxGender.Children)
            {
                (lev as RadioButton).IsChecked = false;
            }

            CheckBoxHasDriversLicence.IsChecked = false;
            bool IsMale = false;
            string gender = "";            

            TxtBoxFirstname.Text = person.Firstname;
            TxtBoxLastname.Text = person.Lastname;
            DatePicker.SelectedDate = person.Birthdate;
            
            
                foreach (var lev in GroupBoxGender.Children)
                {
                    if (person.IsMale)
                    {
                        if ((lev as RadioButton).Content.Equals("Male"))
                        {
                            (lev as RadioButton).IsChecked = true;
                        }
                    }
                    else
                    {
                        if ((lev as RadioButton).Content.Equals("Female"))
                        {
                            (lev as RadioButton).IsChecked = true;
                        }
                    }                    
                    
                }
            

            if (person.HasDriversLicence)
            {
                CheckBoxHasDriversLicence.IsChecked = true;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GroupBoxGender.Children.Add(new RadioButton { Content = "Male" });
            GroupBoxGender.Children.Add(new RadioButton { Content = "Female" });
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool IsMale = false;
            bool DriversLicence = false;
            foreach (var lev in GroupBoxGender.Children)
            {
                
                if((lev as RadioButton).IsChecked ?? false)
                {
                    
                    if((lev as RadioButton).Content.ToString().Equals("Male")){
                        IsMale = true;
                    }
                    else
                    {
                        IsMale = false;
                    }
                }
            }

            if (CheckBoxHasDriversLicence.IsChecked ?? false)
            {
                DriversLicence = true;
            }

            Person person = new Person { Firstname = TxtBoxFirstname.Text, Lastname = TxtBoxLastname.Text, Birthdate = (DateTime)DatePicker.SelectedDate, IsMale = IsMale, HasDriversLicence = DriversLicence };
            ListView.Items.Add(person);
            persons.Add(person);            
        }

        /*public string AsCsvString(Person person)
        {                           
            return $"{person.Firstname};{person.Lastname};{person.Birthdate};{person.IsMale};{person.HasDriversLicence}";            
        }*/

        private void LoadCSV(object sender, RoutedEventArgs e)
        {            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                string[] allLines = File.ReadAllLines(openFileDialog.FileName);                           
                                
                foreach (var line in allLines)
                {                    
                    string[] parts = line.Split(Char.Parse(";"));                   
                    Person person = Parse(parts);                  
                    persons.Add(person);
                    ListView.Items.Add(person);
                    
                }   
                
            }
        }

        private Person Parse(string[] parts)
        {
            Person person = new Person { Firstname = parts[0], Lastname = parts[1], Birthdate = Convert.ToDateTime(parts[2]), IsMale = bool.Parse(parts[3]), HasDriversLicence = bool.Parse(parts[4]) };
            return person;            
        }

        private void SaveCSV(object sender, RoutedEventArgs e)
        {
            string text = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == true)
            {
                foreach (var person in persons)
                {                    
                    text = text + $"{person.Firstname};{person.Lastname};{person.Birthdate};{person.IsMale};{person.HasDriversLicence}" + "\n";
                }
                File.WriteAllText(saveFileDialog.FileName, text);
            }            
        }
    }
}
