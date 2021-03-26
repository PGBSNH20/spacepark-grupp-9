using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkModel.SwApi
{
    public class SwApi
    {
        private readonly RestClient client;

        public SwApi()
        {
            client = new RestClient("https://swapi.dev/api/");
        }

        public async Task<List<T>> GetAllResources<T>(SwApiResource resource)
        {
            SwResource<T> response = await GetResourcePage<T>(resource);
            List<T> data = response.Results;
            // once it goes into the while loop, it uses the GetResourcePage(string) method 
            while (response.Next != null)
            {
                response = await GetResourcePage<T>(response.Next);
                data.AddRange(response.Results);
            }
            return data;
        }

        private async Task<SwResource<T>> GetResourcePage<T>(SwApiResource resource)
        {
            string resourceString = resource.ToString();
            var request = new RestRequest($"{resourceString}/", DataFormat.Json);
            return await client.GetAsync<SwResource<T>>(request);
        }

        private async Task<SwResource<T>> GetResourcePage<T>(string resource)
        {
            var request = new RestRequest(resource, DataFormat.Json);
            return await client.GetAsync<SwResource<T>>(request);
        }

        private async Task<T> GetResource<T>(string resource)
        {
            var request = new RestRequest(resource, DataFormat.Json);
            return await client.GetAsync<T>(request);
        }

        public async Task<List<T>> SearchResource<T>(SwApiResource resource, string searchTerm)
        {
            var request = new RestRequest($"{resource}/?search={searchTerm}", DataFormat.Json);
            var response = await client.GetAsync<SwResource<T>>(request);
            List<T> data = response.Results;
            return data;
        }

        public async Task<bool> ValidateSwName(string name)
        {
            string trimmedName = name.Trim().ToLower();
            List<SwPeople> people = await SearchResource<SwPeople>(SwApiResource.people, trimmedName);
            if (people.Count != 1)
            {
                return false;
            }
            string personName = people[0].Name.ToLower();

            return trimmedName == personName;
        }

        public async Task<List<SwStarship>> GetPersonStarships(string name)
        {
            List<SwPeople> people = await SearchResource<SwPeople>(SwApiResource.people, name);
            SwPeople person = people.First();
            
            List<SwStarship> starships = new();
            if (person.Starships.Count > 0)
            {
                foreach (var starship in person.Starships)
                {
                    starships.Add(await GetResource<SwStarship>(starship));
                }
            }

            return starships;
        }
    }
}