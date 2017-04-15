using Xamarin.Forms;
using System.Linq;

namespace MyContacts
{
    public class App : Application
    {
        public static ContactRepository ContactRepo { get; private set; }

        public App(string dbPath)
        {
            //set database path first, then retrieve main page
            ContactRepo = new ContactRepository(dbPath);

            MainPage = new NavigationPage(new AllContacts());
        }
    }
}
