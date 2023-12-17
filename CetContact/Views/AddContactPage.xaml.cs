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
        this.Navigation.PopAsync(); // Windows'ta geri gitmek i�in kullan�lan y�ntem
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            // Kullan�c�ya uyar� g�sterme veya i�lemi durdurma
            await DisplayAlert("Uyar�", "�sim girmek zorunludur.", "Tamam");
            return;
        }
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            // Kullan�c�ya uyar� g�sterme veya i�lemi durdurma
            await DisplayAlert("Uyar�", "Email girmek zorunludur.", "Tamam");
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

        // Windows'ta geri gitmek i�in kullan�lan y�ntem
        this.Navigation.PopAsync();
    }
}
