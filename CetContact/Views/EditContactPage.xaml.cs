using CetContact.Model;
using Microsoft.Maui.Controls;

namespace CetContact.Views
{
    public partial class EditContactPage : ContentPage
    {
        ContactInfo contactInfo;
        ContactRepository contactRepository;

        public EditContactPage()
        {
            InitializeComponent();
            contactRepository = new ContactRepository();
        }

        // "id" parametresi için QueryProperty özelliði
        public static readonly BindableProperty ContactIdProperty =
            BindableProperty.Create(nameof(ContactId), typeof(string), typeof(EditContactPage));

        public string ContactId
        {
            get => (string)GetValue(ContactIdProperty);
            set => SetValue(ContactIdProperty, value);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Int32.Parse yerine Convert.ToInt32 kullanýlabilir
            if (int.TryParse(ContactId, out int contactId))
            {
                contactInfo = await contactRepository.GetContactById(contactId);
                if (contactInfo != null)
                {
                    NameEntry.Text = contactInfo.Name;
                    PhoneEntry.Text = contactInfo.Phone;
                    EmailEntry.Text = contactInfo.Email;
                    AdressEntry.Text = contactInfo.Address;
                }
                else
                {
                    await DisplayAlert("Hata", "Kiþi Bulunamadý", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Hata", "Geçersiz kiþi ID", "Tamam");
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            contactInfo.Name = NameEntry.Text;
            contactInfo.Phone = PhoneEntry.Text;
            contactInfo.Address = AdressEntry.Text;
            contactInfo.Email = EmailEntry.Text;

            await contactRepository.Update(contactInfo);
            await Shell.Current.GoToAsync("..");
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            contactInfo.Name = NameEntry.Text;
            contactInfo.Phone = PhoneEntry.Text;
            contactInfo.Address = AdressEntry.Text;
            contactInfo.Email = EmailEntry.Text;

            bool userConfirmation = await DisplayAlert("Onay", "Kiþiyi silmek istediðinize emin misiniz?", "Evet", "Hayýr");

            if (userConfirmation)
            {
                await contactRepository.Delete(contactInfo);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
