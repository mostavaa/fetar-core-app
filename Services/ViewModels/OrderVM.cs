using System;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class OrderVM
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public Guid GUID { get; set; }

    }
}
