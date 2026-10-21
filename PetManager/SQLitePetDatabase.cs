using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager
{
    internal class SQLitePetDatabase : IPetDatabase
    {
        string databasePath =
            Path.Combine(FileSystem.Current.AppDataDirectory, "pets.db3");

        SQLite.SQLiteOpenFlags flags =
            SQLite.SQLiteOpenFlags.ReadWrite
            | SQLite.SQLiteOpenFlags.Create;

        SQLiteAsyncConnection database;

        public SQLitePetDatabase()
        {
            database = new SQLiteAsyncConnection(databasePath,flags);
            database.CreateTableAsync<Pet>().Wait();

        }

        public async Task CreatePetAsync(Pet pet)
        {
            await database.InsertAsync(pet);
        }

        public async Task DeletePetAsync(Pet pet)
        {
            await database.DeleteAsync(pet);
        }

        public async Task<Pet> GetPetAsync(int id)
        {
            return await database
                .Table<Pet>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Pet>> GetPetsAsync()
        {
            return await database.Table<Pet>().ToListAsync();
        }

        public async Task UpdatePetAsync(Pet pet)
        {
            await database.UpdateAsync(pet);
        }
    }
}
