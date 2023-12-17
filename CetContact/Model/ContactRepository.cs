using SQLite;

namespace CetContact.Model
{
    public class ContactRepository
    {
        private SQLiteAsyncConnection database;
        private string databaseName = "contacts2.db3";

        public ContactRepository()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ContactInfo>(CreateFlags.AllImplicit | CreateFlags.AutoIncPK).GetAwaiter().GetResult();
        }

        public async Task<List<ContactInfo>> GetAllContacts()
        {
            return await database.Table<ContactInfo>().ToListAsync();
        }

        public async Task AddContact(ContactInfo contact)
        {
            await database.InsertAsync(contact);
        }

        public async Task<ContactInfo> GetContactById(int Id)
        {
            var contact = await database.Table<ContactInfo>().Where(c => c.Id == Id).FirstOrDefaultAsync();
            return contact;
        }

        public async Task Update(ContactInfo contact)
        {
            await database.UpdateAsync(contact);
        }

        public async Task Delete(ContactInfo contact)
        {
            await database.DeleteAsync(contact);
        }
    }
}
