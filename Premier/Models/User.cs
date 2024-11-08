namespace Premier.Models;

public class User {
	public string Pseudo { get; set; }
	public string Password { get; set; }
	public int Id { get; set; }

	public enum Role {
	    User, Admin
	}


}

public class UserCreation {
	public string Pseudo {get; set;}
	public string Password {get; set;}
}

public class UserUpdate {
	public string Pseudo {get; set;}
	public string Password {get; set;}
}
