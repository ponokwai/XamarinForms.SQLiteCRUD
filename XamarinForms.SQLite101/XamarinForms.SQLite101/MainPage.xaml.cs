using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.SQLite101.Models;
using System.Linq;
namespace XamarinForms.SQLite101
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var personList = await App.SQLiteDb.GetItemsAsync();
            if(personList != null)
            {
                lstPersons.ItemsSource = personList;
            }
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Person person = new Person
                {
                    Name = txtName.Text
                };
                await App.SQLiteDb.SaveItemAsync(person);
                txtName.Text = string.Empty;
                await DisplayAlert("Success", "Person added successfully", "OK");

                var personList = await App.SQLiteDb.GetItemsAsync();
                if (personList.Any())
                {
                    lstPersons.ItemsSource = personList;
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter name!", "OK");
            }
        }

        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPersonId.Text))
            {
                await DisplayAlert("Required", "Please enter a PersonID", "OK");
            }
            var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
            if(person != null)
            {
                await DisplayAlert("Success", $"Person Name : {person.Name}", "OK");
            }
            else
            {
                await DisplayAlert("Failed", "No record found", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                Person person = new Person()
                {
                    PersonID = Convert.ToInt32(txtPersonId.Text),
                    Name = txtName.Text
                };

                //Update Person  
                await App.SQLiteDb.SaveItemAsync(person);

                txtPersonId.Text = string.Empty;
                txtName.Text = string.Empty;
                await DisplayAlert("Success", "Person Updated Successfully", "OK");
                //Get All Persons  
                var personList = await App.SQLiteDb.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }

            }
            else
            {
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }

        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonId.Text))
            {
                //Get Person  
                var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
                if (person != null)
                {
                    //Delete Person  
                    await App.SQLiteDb.DeleteItemAsync(person);
                    txtPersonId.Text = string.Empty;
                    await DisplayAlert("Success", "Person Deleted", "OK");

                    //Get All Persons  
                    var personList = await App.SQLiteDb.GetItemsAsync();
                    if (personList != null)
                    {
                        lstPersons.ItemsSource = personList;
                    }
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter PersonID", "OK");
            }

        }
    }
}
