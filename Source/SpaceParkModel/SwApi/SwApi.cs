﻿using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkModel.SwApi
{
    public class SwApi
    {
        RestClient client;

        public SwApi()
        {
            client = new RestClient("https://swapi.dev/api/");
        }

        public async Task<List<T>> GetAllResources<T>(SwApiResource resource)
        {
            SwResource<T> response = await GetResourcePage<T>(resource);
            List<T> datas = response.Results;
            // once it goes into the while loop, it uses the GetResourcePage(string) method 
            while (response.Next != null)
            {
                response = await GetResourcePage<T>(response.Next);
                datas.AddRange(response.Results);
            }
            return datas;
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
            List<T> datas = response.Results;
            return datas;
        }

        public bool ValidateSwName(string name)
        {
            string trimmedName = name.Trim().ToLower();
            List<SwPeople> people = SearchResource<SwPeople>(SwApiResource.people, trimmedName).Result;
            if (people.Count != 1)
            {
                return false;
            }
            string personName = people[0].Name.ToLower();

            return trimmedName.Equals(personName);
        }

        public List<SwStarship> GetPersonStarships(string name)
        {
            List<SwStarship> starships = new();
            SwPeople person = SearchResource<SwPeople>(SwApiResource.people, name).Result.First();
            
            if (person.Starships.Count > 0)
            {
                foreach (var starship in person.Starships)
                {
                    starships.Add(GetResource<SwStarship>(starship).Result);
                }
            }

            return starships;
        }
    }
}