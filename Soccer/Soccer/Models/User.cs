
using System;
using System.Collections.Generic;



namespace Soccer.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserTypeId { get; set; }

        public string Picture { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public int FavoriteTeamId { get; set; }

        public int Points { get; set; }

        
        public Team FavoriteTeam { get; set; }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public DateTime TokenExpires { get; set; }


        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string FullPicture
        {
            get
            {
                if (string.IsNullOrEmpty(Picture))
                {
                    return "avatar_user.png";
                }

                return string.Format("http://soccerbackend.azurewebsites.net{0}", Picture.Substring(1));
            }
        }


    }
}
