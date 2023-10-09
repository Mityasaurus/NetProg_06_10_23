using Newtonsoft.Json.Linq;
using RickAndMorty_API_CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty_API_CORE.Domain.ProviderJson
{
    public sealed class JsonProvider
    {
        public static List<Character> FromJsonToCharacterList(string json)
        {
            List<Character> characters = new List<Character>();
            JObject keyValuePairs = JObject.Parse(json);
            IList<JToken> results = keyValuePairs["results"].Children().ToList();
            foreach (JToken token in results)
            {

                //Character character = token.ToObject<Character>();

                //int c = 0;
                //foreach (var child in token.Children())
                //{
                //    if(c == 7)
                //    {
                //        string s = child.First.First.Last.ToString();
                //        class_params.Add(s);
                //    }
                //    else
                //    {
                //        class_params.Add(child.Last.ToString());
                //    }
                //    c++;
                //}
                Character item = new Character();

                item.Id = token["id"].ToString();
                item.Name = token["name"].ToString();
                item.Status = token["status"].ToString();
                item.Species = token["species"].ToString();
                item.Gender = token["gender"].ToString();
                item.Location = token["location"]["name"].ToString();
                item.ImageURL = token["image"].ToString();

                characters.Add(item);
            }
            return characters;
        }

        public static int GetPagesMaxNumberFromJson(string json)
        {
            int result = 0;
            JObject keyValuePairs = JObject.Parse(json);
            var results = keyValuePairs["info"]["pages"].ToString();

            result = int.Parse(results);
            return result;
        }
    }
}
