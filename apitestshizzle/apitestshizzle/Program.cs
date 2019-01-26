using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace apitestshizzle
{
    class Program
    {
        private MobileServiceClient client;
        public List<Person> Persons { get; }

        public Program()
        {
            client = new MobileServiceClient( "http://localhost:5000/api");
            var store = new MobileServiceSQLiteStore("wijkagent.db");
            store.DefineTable<Person>();
            client.SyncContext.InitializeAsync(store);
            //            client.SyncContext.InitializeAsync(new )
            //            Persons = GetPersons().Result;
        }

//        private async Task InitLocalStoreAsynce()
//        {
            
//            return await 

//        }
//        private async Task SyncAsync()
//        {
//            await client.SyncContext.PushAsync();
            
//        }
        private async Task<List<Person>> GetPersons()
        {
            return await client.GetSyncTable<Person>().ToListAsync();
        }

        static void Main(string[] args)
        {
            var p = new Program();

            foreach (var pPerson in p.Persons)
            {
                Console.WriteLine(pPerson.firstName + " " + pPerson.lastName);
            }

            Console.ReadKey();
        }

    }
}
