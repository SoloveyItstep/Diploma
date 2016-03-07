using Microsoft.AspNet.Mvc.Rendering;
using Store.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.ViewModels.Account
{
    public class LoginNewUserOrderViewModel
    {
        public LoginNewUserOrderViewModel(String language)
        {
            Language = language;
            UserName = "";
            Phone = "";
            if (language == "EN")
            {
                DeliveryList = new List<SelectListItem> {
                    new SelectListItem
                    {
                        Text = "Select Delivery option",
                        Value = "Delivery"
                    },
                    new SelectListItem
                    {
                        Text = "Pickup",
                        Value = "Pickup"
                    },
                    new SelectListItem
                    {
                        Text = "Courier",
                        Value = "Courier"
                    },
                    new SelectListItem
                    {
                        Text = "Nova Poshta",
                        Value = "Nova Poshta"
                    }
                };
                PaymentList = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Select Payment option",
                        Value = "Empty"
                    },
                    new SelectListItem
                    {
                        Text = "Cash",
                        Value = "Cash"
                    },
                    new SelectListItem
                    {
                        Text = "Cashless payment",
                        Value = "Cashless payment"
                    },
                    new SelectListItem
                    {
                        Text = "Transfer to card",
                        Value = "Transfer to card"
                    }
                };
            }
            else
            {
                DeliveryList = new List<SelectListItem> {
                    new SelectListItem
                    {
                        Text = "Выберете способ доставки",
                        Value = "Empty"
                    },
                    new SelectListItem
                    {
                        Text = "Самовывоз",
                        Value = "Pickup"
                    },
                    new SelectListItem
                    {
                        Text = "Курьером",
                        Value = "Courier"
                    },
                    new SelectListItem
                    {
                        Text = "Новой Почтой",
                        Value = "Nova Poshta"
                    }
                };

                PaymentList = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Выберете способ оплаты",
                        Value = "Empty"
                    },
                    new SelectListItem
                    {
                        Text = "Наличными",
                        Value = "Cash"
                    },
                    new SelectListItem
                    {
                        Text = "Безналичный",
                        Value = "Cashless payment"
                    },
                    new SelectListItem
                    {
                        Text = "Перевод на карту",
                        Value = "Transfer to card"
                    }
                };
            }
        }

        [RegularExpression(pattern: @"[a-zA-Zа-яА-Я0-9_\- ]{0,30}$")]
        [StringLength(maximumLength: 30)]
        [Display(Name = "User name")]
        public String UserName { get; set; }

        [Required]
        [RegularExpression(@"^\+?[0-9]{3,5}-?[0-9\-]+$")]
        public String Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IList<SelectListItem> DeliveryList { get; set; }

        [Display(Name = "Delivery")]
        public String Delivery { get; set; }

        public String City { get; set; }

        public String Address { get; set; }

        public IList<SelectListItem> PaymentList { get; set; }

        public String Payment { get; set; }

        public Dictionary<Apple, Int32> Apple { get; set; }

        [StringLength(maximumLength: 2)]
        public String Language { get; set; }

        public Decimal Currency { get; set; }
    }
}
