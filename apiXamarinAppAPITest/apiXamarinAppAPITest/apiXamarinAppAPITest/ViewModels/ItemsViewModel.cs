using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using apiXamarinAppAPITest.Models;
using apiXamarinAppAPITest.Views;
using Microsoft.WindowsAzure.MobileServices;

namespace apiXamarinAppAPITest.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private MobileServiceClient client;

        public ItemsViewModel()
        {
            client = new MobileServiceClient("localhost:5000/api/people");


            client.SyncContext.Store.InitializeAsync();


            var table = client.GetSyncTable<Person>();

            var hueue = table.Where(x => x.firstName.Equals("jannes"));
        }
    }
}