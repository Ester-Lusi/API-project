using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using Entities;


namespace Services
{
    public class PasswordService :IPasswordService
    {
        public Password GetStrength(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            Password pass = new() { Pass = password, Strength = result.Score };
            return pass;
        }
    }
}
