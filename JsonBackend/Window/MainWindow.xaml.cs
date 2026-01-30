using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
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
using JsonBackend.Model;
using JsonBackend.Util;

namespace JsonBackend.Window;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : System.Windows.Window {
    private ObservableCollection<Person> _persons = new();
    
    public MainWindow() {
        InitializeComponent();
        
        PersonsDataGrid.ItemsSource = _persons;
        
        // Events
        Loaded += async (s, e) => {
            try {
                var persons = await ApiUtil.GetPersons();
                foreach (var person in persons) _persons.Add(person);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        };

        RefreshButton.Click += async (s, e) => {
            try {
                _persons.Clear();
                var persons = await ApiUtil.GetPersons();
                foreach (var person in persons) _persons.Add(person);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        };
        
        AddButton.Click += async (s, e) => {
            var window = new CreatePersonWindow {
                Owner = this
            };
            
            window.ShowDialog();
        };
        
        DeleteButton.Click += (s, e) => {
            try {
                if (PersonsDataGrid.SelectedItem != null)
                    ApiUtil.DeletePerson(((Person)PersonsDataGrid.CurrentItem).Id);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        };
        
        
    }
}