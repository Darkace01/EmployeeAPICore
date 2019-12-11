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
    public class EmployeeTaskApi
    {
        string baseUrl = "https://localhost:44359/api/";

        public async Task<List<ViewEmployeeTaskViewModel>> GetAllEmployeeTasks()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("employeetask");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<ViewEmployeeTaskViewModel>>();
                return result;
            }
        }
        public async Task<ViewEmployeeTaskViewModel> GetEmployeeTaskById(int taskId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"employeetask/{taskId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<ViewEmployeeTaskViewModel>();
                return result;
            }
        }
        public async Task<ViewEmployeeTaskViewModel> GetEmployeeTaskByEmployeeId(int employeeId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"employeetask/byEmployeeId/{employeeId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<ViewEmployeeTaskViewModel>();
                return result;
            }
        }
        public async Task CreateEmployeeTask(CreateEmployeeTaskViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("employeetask", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }

        public async Task UpdateEmployeeTask(ViewEmployeeTaskViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PutAsync("employeetask", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task DeleteEmployeeTask(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.DeleteAsync($"employeetask/{Id}");
                msg.EnsureSuccessStatusCode();
            }
        }
    }
}
