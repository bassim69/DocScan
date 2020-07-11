using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocScan.Models;
using DocScan.ViewModels;
using DocScan.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocScan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        MasterPageViewModel _viewModel
        {
            get { return BindingContext as MasterPageViewModel; }
            set { BindingContext = value; }
        }

        public MasterPage()
        {
            InitializeComponent();
            _viewModel = new MasterPageViewModel();

            listView.ItemsSource = GetItemSource();
            listView.ItemSelected += OnItemSelected;

            Title = "MasterPage";
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        List<MasterPageItem> GetItemSource() {
            List<MasterPageItem> list = new List<MasterPageItem>();
            
            list.Insert(0, new MasterPageItem { Title = "HomePage", TargetType = new HomePage().GetType() });
            
            if (_viewModel.IsAllowed)
                list.Add(new MasterPageItem { Title = "Users", TargetType = new UsersListStatic().GetType() });

            return list;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;

            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                listView.SelectedItem = null;
                IsPresented = false;
            }
        }

        async Task Button_Profile_Clicked(object sender, EventArgs e)
        {
            User user = await _viewModel.GetUserById();

            (Application.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new Profile(user));
            IsPresented = false;
        }

        void Button_Logout_Clicked(object sender, EventArgs e)
        {
            Settings.AuthenticationToken = "";
            Settings.CurrentUserRole = "";
            Settings.UserId = "";
            Settings.Password = "";

            Application.Current.MainPage = new LoginPage();
        }
    }
}
