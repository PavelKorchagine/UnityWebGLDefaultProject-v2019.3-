using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DBUserData
{
    public string Username { get; set; }
    public string Password { get; set; }

    public DBUserData() { }
    public DBUserData(string Username, string Password) {
        this.Username = Username;
        this.Password = Password;
    }

    public override string ToString()
    {
        return $"Username:{Username}, Password:{Password} ";
    }

}
