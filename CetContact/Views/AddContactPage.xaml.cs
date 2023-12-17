using CetContact.Model;
using Microsoft.Maui.Controls;

public partial class AddContactPage : ContentPage
{
    ContactRepository contactRepository;

    public AddContactPage()
    {
        InitializeComponent();
        contactRepository = new ContactRepository();
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
        // Shell.Current.GoToAsync("//"+nameof(ContactsPage));
        this.Navigation.PopAsync(); // Windows'ta geri gitmek için kullanýlan yöntem
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            // Kullanýcýya uyarý gösterme veya iþlemi durdurma
            await DisplayAlert("Uyarý", "Ýsim girmek zorunludur.", "Tamam");
            return;
        }
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            // Kullanýcýya uyarý gösterme veya iþlemi durdurma
            await DisplayAlert("Uyarý", "Email girmek zorunludur.", "Tamam");
            return;
        }

        ContactInfo contact = new ContactInfo
        {
            Name = NameEntry.Text,
            Phone = PhoneEntry.Text,
            Address = AdressEntry.Text,
            Email = EmailEntry.Text,
        };
        await contactRepository.AddContact(contact);

        // Windows'ta geri gitmek için kullanýlan yöntem
        this.Navigation.PopAsync();
    }
}
