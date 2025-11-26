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
        public Password CheckStrength(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            int strength = result.Score;
            Password passwordEntity = new Password();
            passwordEntity.Pass = password;
            passwordEntity.Strength = strength;
            return passwordEntity;
        }
    }
}
