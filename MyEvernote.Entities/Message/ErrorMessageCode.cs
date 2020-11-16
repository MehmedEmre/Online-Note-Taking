using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities.Message
{
    public enum ErrorMessageCode
    {
        //Register
        UsernameAlreadyExists = 101,
        EmailAlreadyExists = 102,

        //Login
        UserisNotActive = 201,
        UserNameOrPassWrong = 202,
        
        //E-mail
        CheckYourEmail = 301,
        UserAlreadyActive = 302,

        //Guid
        ActivateIdDoesNotExists = 401,

        //User
        UserNotFound = 501,
        UserAlreadyExists = 502,

        //Update
        ErrorOccurredDuringUpdate = 501,

        //Add
        ErrorOccurredDuringInsert = 601,

        //Banned
        Banned = 701,

        //access restriction

        AccessRestriction = 801

    }
}
