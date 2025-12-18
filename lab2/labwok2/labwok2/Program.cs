bool ValidateUser(string username, string password, int age, string email)
{
    if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20)
        return false;
    if (string.IsNullOrEmpty(password) || password.Length < 6 || !password.Any(char.IsDigit))
        return false;
    if (age < 13 || age > 120)
        return false;
    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.EndsWith(".edu"))
        return false;
    return true;
}
