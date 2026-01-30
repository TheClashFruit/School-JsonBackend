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

namespace JsonBackend;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private static HttpClient _client = new() {
        BaseAddress = new Uri("https://retoolapi.dev/prTuO5/ccp"),
    };

    private ObservableCollection<Person> _persons = new();
    
    public MainWindow() {
        InitializeComponent();
        
        PersonsDataGrid.ItemsSource = _persons;
        
        // Events
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e) {
        var persons = await _client.GetFromJsonAsync<List<Person>>("");
        
        if (persons != null)
            foreach (var person in persons)
                _persons.Add(person);
    }
}