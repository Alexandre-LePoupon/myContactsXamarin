using MyContacts.Models;
using System;
using System.Collections.ObjectModel;

namespace MyContacts
{
    public partial class ContactAdd
    {
        public ContactAdd()
        {
            InitializeComponent();
        }

        public async void OnNewButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            await App.ContactRepo.AddNewContactAsync(firstName.Text, lastName.Text, phone.Text, email.Text, birthday.Date, genderPicker.SelectedIndex, favorite.IsToggled);
            statusMessage.Text = App.ContactRepo.StatusMessage;
        }

        public void OnDeleteButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            App.ContactRepo.ResetBdd();
            statusMessage.Text = App.ContactRepo.StatusMessage;
        }
    }
}
