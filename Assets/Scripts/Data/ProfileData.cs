using System;
using SimpleJSON;

namespace Data
{
    [Serializable]
    public class ProfileData
    {
        public string name;
        public string name_avatar;
        public string email;
        public string phone;
        public ProfileData(JSONNode itemsData)
        {
            name =  itemsData["user"]["name"];
            name_avatar =  itemsData["user"]["name_avatar"];
            email = itemsData["user"]["email"];
            phone = itemsData["user"]["phone"];
        }
    }
}
