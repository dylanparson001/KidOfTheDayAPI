﻿namespace KidOfTheDayAPI.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Role { get; set; }
    public object Token { get; set; }
    
}