using System;
using System.Collections.Generic;
using System.Linq;
using MyContacts.Models;
using SQLite;
using System.Threading.Tasks;

namespace MyContacts
{
	public class ContactRepository
	{
		private readonly SQLiteAsyncConnection conn;

		public string StatusMessage { get; set; }

		public ContactRepository(string dbPath)
		{
			conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Contact>().Wait();
        }

		public async Task AddNewContactAsync(string firstName, string lastName, String phone, string email, DateTime birthday, int gender, bool favorite)
		{
		    try
			{
				//basic validation to ensure a name was entered
				if (string.IsNullOrEmpty(firstName))
					throw new Exception("Valid name required");
                
                //insert a new contact into the Contact table
                var result = await conn.InsertAsync(
                    new Contact {
                        FirstName = firstName,
                        LastName = lastName,
                        Phone = phone,
                        Email = email,
                        Birthday = birthday,
                        Gender = gender,
                        Favorite = favorite
                    }).ConfigureAwait(continueOnCapturedContext: false);

				StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, firstName);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to add {0}. Error: {1}", firstName, ex.Message);
			}
		}

        public async Task ModifContactAsync(Contact contact)
        {
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(contact.FirstName))
                    throw new Exception("Valid name required");

                //insert a new contact into the Contact table
                var result = await conn.UpdateAsync(contact).ConfigureAwait(continueOnCapturedContext: false);

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, contact.FirstName);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", contact.FirstName, ex.Message);
            }
        }

        public Task<List<Contact>> GetAllContactAsync()
		{
            //return a list of contact saved to the Contact table in the database
            return conn.Table<Contact>().ToListAsync();
		}

        public void ResetBdd()
        {
            //reset Contact table
            conn.ExecuteAsync("DELETE FROM Contact");
        }

        public void DeleteContactAsync(Contact contact)
        {
            conn.DeleteAsync(contact);
        }
    }
}