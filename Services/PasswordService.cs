using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;


namespace Services
{
    public class PasswordService : IPasswordService
    {
        public Password checkStrength(string password)
        {
            var Result = Zxcvbn.Core.EvaluatePassword(password);
            int Strength = Result.Score;
            Password passwordEntity = new Password();
            passwordEntity.Pass = password;
            passwordEntity.Strength = Strength;
            return passwordEntity;
        }
    }
}
