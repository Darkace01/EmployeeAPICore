using EmployeeAPI.MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.MVC.ApiHelpers
{
    public class EmployeeApi
    {
        string baseUrl = "https://localhost:44359/api/";

        public async Task<List<ViewEmployeeViewModel>> GetAllEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("employee");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<ViewEmployeeViewModel>>();
                return result;
            }
        }
        public async Task<ViewEmployeeViewModel> GetEmployeeById(int employeeId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"employee/{employeeId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<ViewEmployeeViewModel>();
                return result;
            }
        }
        public async Task CreateEmployee(CreateEmployeeViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("employee", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task UpdateEmployee(ViewEmployeeViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("employee", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task DeleteEmployee(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.DeleteAsync($"employee/{Id}");
                msg.EnsureSuccessStatusCode();
            }
        }
    }
}
