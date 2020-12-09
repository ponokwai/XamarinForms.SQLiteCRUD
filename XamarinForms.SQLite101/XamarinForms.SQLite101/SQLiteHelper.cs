using SQLite;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinForms.SQLite101.Models;

namespace XamarinForms.SQLite101
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Person>().Wait();
        }

        public async Task<IList<Person>> GetItemsAsync()
        {
            return await db.Table<Person>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(Person person)
        {
            if(person.PersonID != 0)
            {
                return await db.UpdateAsync(person);
            }
            else
            {
                return await db.InsertAsync(person);
            }
        }

        public async Task<Person> GetItemAsync(int personId)
        {
            return await db.Table<Person>().Where(i => i.PersonID == personId).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteItemAsync(Person person)
        {
            return await db.DeleteAsync(person);
        }
    }
}