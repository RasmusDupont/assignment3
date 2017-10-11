using System;
using Newtonsoft.Json;
namespace questionAserver
{
    public class Category
    {

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonProperty("cid")]
        public int Id
        {
            get;
            set;
        }
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

    }
}
