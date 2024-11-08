namespace Premier.Models;

public enum Role {
	user, admin
}

public class User {
	public string Pseudo { get; set; }
	public string Password { get; set; }
	public int Id { get; set; }
	public Role Role { get; set; }
}

public class UserCreation {
	public string Pseudo {get; set;}
	public string Password {get; set;}
}

public class UserUpdate : UserInfo {
	public Role Role;
}
