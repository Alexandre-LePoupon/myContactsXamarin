using MyContacts.Models;
using Plugin.Messaging;
using System;
using Xamarin.Forms;

namespace MyContacts
{
    public partial class ContactDetails : ContentPage
    {
        private Contact theContact;

		public ContactDetails(Contact contact)
        {
			BindingContext = contact;
            theContact = contact;
            InitializeComponent();
        }

        public async void OnModifButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            await App.ContactRepo.ModifContactAsync(theContact);

            statusMessage.Text = App.ContactRepo.StatusMessage;
        }

        public void OnDeleteButtonClicked(object sender, EventArgs args)
        {
            statusMessage.Text = "";

            App.ContactRepo.DeleteContactAsync(theContact);
            statusMessage.Text = App.ContactRepo.StatusMessage;
        }

        public void OnCallButtonClicked(object sender, EventArgs args)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            //var number = "0" + phone.Text;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(phone.Text);
        }

        public void OnSmsButtonClicked(object sender, EventArgs args)
        {
            var phoneDialer = CrossMessaging.Current.SmsMessenger;
            //var number = "0" + phone.Text;
            if (phoneDialer.CanSendSms)
                phoneDialer.SendSms(phone.Text);
        }

        public void OnEmailButtonClicked(object sender, EventArgs args)
        {
            var mailto = "mailto:" + email.Text;
            Device.OpenUri(new Uri(mailto));
        }
    }
}
