using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatProject.Repository
{
    public class ProductSpecParams
    {
        private const int MaxSize = 10;
        public int PageIndex { get; set; } = 1;
        private int pageSize = 5;
        public int PageSize 
        {
            get
            {
                return pageSize;
            }
            set 
            { 
                if(value > MaxSize)
                    pageSize = MaxSize;
                else
                    pageSize = value;
            } 
        } 
        public string? Sort {get; set;}

        public int? BrandId {get; set;}

        public int? TypeId { get; set; }
        private string? searchByName;
        public string? SearchByName 
        {
            get
            {
                return searchByName;
            }
            set 
            {
                searchByName = value?.ToLower();
            } 
        }
    }
}
