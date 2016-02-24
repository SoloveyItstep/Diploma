using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding;

namespace AppleStore.Models.RegisterLogin
{
    public class RegisterLoginErorsLanguage : IRegisterLoginErrorsLanguage
    {
        public string Login(ICollection<ModelStateEntry> modelState, String language)
        {
            String respons = "";
            foreach (var model in modelState)
            {
                foreach (var error in model.Errors)
                {
                    respons += error.ErrorMessage + "<br/>";
                }
            }
            return respons;
        }

        public string Registration(ICollection<ModelStateEntry> modelState, String language)
        {
            String respons = "";
            foreach (var model in modelState)
            {
                foreach (var error in model.Errors)
                {
                    respons += RegisterMessage(error.ErrorMessage,language) + "<br/>";
                }
            }
            return respons;
        }

        private String RegisterMessage(String message, String language)
        {
            String returnMessage = String.Empty;
            if (language == "EN") {
                if (message == "Требуется поле User name.")
                    returnMessage = "- 'User name' field is required";
                else if (message == "Требуется поле City.")
                    returnMessage = "- 'City' field is required";
                else if (message == "Требуется поле Address.")
                    returnMessage = "- 'Address' field is required";
                else if (message == "Требуется поле Email.")
                    returnMessage = "- 'Email' field is required";
                else if (message == "Требуется поле Password.")
                    returnMessage = "- 'Password' field is required";
                else if (message == "Требуется поле Phone.")
                    returnMessage = "- 'Phone' field is required";
                else if (message == "Поле User name должно соответствовать регулярному выражению \"[a-zA-Z0-9_\\- ]{2,15}$\".")
                    returnMessage = "- 'User name' field shoudn have only letters, digits and '_- ' simbols and max length is 15";
                else if (message == "Поле User name должно быть строкой с минимальной длиной 2 и максимальной 15.")
                    returnMessage = "- 'User name' field min length is 2 and max length is 15.";
                else if (message == "Поле City должно соответствовать регулярному выражению \"[a-zA-Z0-9_\\- ]{2,15}$\".")
                    returnMessage = "- 'City' field shoudn have only letters, digits and '_- ' simbols and max length is 15";
                else if (message == "Поле City должно быть строкой с минимальной длиной 2 и максимальной 15.")
                    returnMessage = "- 'City' field min length is 2 and max length is 15.";
                else if (message == "Поле Phone должно соответствовать регулярному выражению \"^\\+?[0-9]{3,5}-?[0-9](-[0-9]+)+$\".")
                    returnMessage = "- 'Phone' field could have '+' at begining and it should have digits or deshes.";
                else if (message == "Поле Address должно соответствовать регулярному выражению \"[^<>!{}]+\".")
                    returnMessage = "- 'Address' field shouldn't have '<>!{}' simbols";
                else if (message == "Поле Address должно быть строкой с минимальной длиной 5 и максимальной 40.")
                    returnMessage = "- 'Address' field min length is 5 and max length is 40";
                else if (message == "Поле Email не содержит допустимый адрес электронной почты.")
                    returnMessage = "- 'Email' field should have email address";
               
            }
            else
            {

            }
            if (returnMessage == String.Empty)
                returnMessage = "- "+message;
            return returnMessage;
        }
    }
}
