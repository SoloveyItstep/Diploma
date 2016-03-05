using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.Models.RegisterLogin
{
    public interface IRegisterLoginErrorsLanguage
    {
        String Registration(ICollection<ModelStateEntry> modelState, String language);
        String Login(ICollection<ModelStateEntry> modelState, String language);
    }
}
