using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using MyContacts.Models;

namespace MyContacts
{
    public partial class AllContacts : ContentPage
    {
        public AllContacts()
        {
            InitializeComponent();

            statusMessage.Text = "";
            getContact();
        }

        async void getContact()
        {
            ObservableCollection<Contact> contact =
                new ObservableCollection<Contact>(await App.ContactRepo.GetAllContactAsync());
            contactList.ItemsSource = contact;
        }
        
        async void OnAdd(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ContactAdd());
        }
        
        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Contact tappedContact = (Contact) e.Item;
            await this.Navigation.PushAsync(new ContactDetails(tappedContact));
        }
        
        void OnRefreshing(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            getContact();
            lv.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            getContact();
        }

        async void OnFilter(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Chose filter", "Cancel", "Reset", "Favorite", "Male", "Female");
            if(action == "Favorite")
            {
                ObservableCollection<Contact> contact =
                new ObservableCollection<Contact>(await App.ContactRepo.GetAllContactAsync());

                ObservableCollection<Contact> favoriteContact = new ObservableCollection<Contact>();
                
                foreach(var tmp in contact)
                {
                    if(tmp.Favorite == true)
                    {
                        favoriteContact.Add(tmp);
                    }
                }

                contactList.ItemsSource = favoriteContact;
            }

            if (action == "Male")
            {
                ObservableCollection<Contact> contact =
                new ObservableCollection<Contact>(await App.ContactRepo.GetAllContactAsync());

                ObservableCollection<Contact> maleContact = new ObservableCollection<Contact>();

                foreach (var tmp in contact)
                {
                    if (tmp.Gender == 0)
                    {
                        maleContact.Add(tmp);
                    }
                }

                contactList.ItemsSource = maleContact;
            }

            if (action == "Female")
            {
                ObservableCollection<Contact> contact =
                new ObservableCollection<Contact>(await App.ContactRepo.GetAllContactAsync());

                ObservableCollection<Contact> femaleContact = new ObservableCollection<Contact>();

                foreach (var tmp in contact)
                {
                    if (tmp.Gender == 1)
                    {
                        femaleContact.Add(tmp);
                    }
                }

                contactList.ItemsSource = femaleContact;
            }

            if (action == "Reset")
            {
                getContact();
            }
        }
    }
}

