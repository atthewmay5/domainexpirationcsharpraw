using System;
using System.Collections.Generic;
using System.Text;

namespace Expiring_Users
{
    class User
    {

		private String username;
		private String expiration;

		public User()
		{

		}


		public String getUsername()
		{
			return username;
		}
		public void setUsername(String username)
		{
			this.username = username;
		}
		public String getExpiration()
		{
			return expiration;
		}
		public void setExpiration(String expiration)
		{
			this.expiration = expiration;
		}

	public String toString()
		{
			return "User [username=" + username + ", expiration=" + expiration + "]";
		}

	}
}
