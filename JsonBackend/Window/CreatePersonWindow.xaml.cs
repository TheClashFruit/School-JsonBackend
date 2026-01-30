using System.Windows;
using JsonBackend.Model;
using JsonBackend.Util;

namespace JsonBackend.Window;

public partial class CreatePersonWindow : System.Windows.Window {
    public CreatePersonWindow() {
        InitializeComponent();
        
        CreateButton.Click += async (s, a) => {
            var name = NameTextBox.Text;
            var lastSeen = LastSeenTextBox.Text;
            var validCredit = int.TryParse(SocialCreditTextBox.Text, out int socialCredit);
            
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastSeen) || !validCredit) {
                MessageBox.Show("Please fill in all fields correctly.");
                return;
            }
            
            var person = new Person {
                Name = name,
                LastSeen = lastSeen,
                SocialCredit = socialCredit
            };
            
            try {
                await ApiUtil.CreatePerson(person);
                Close();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        };
    }
}