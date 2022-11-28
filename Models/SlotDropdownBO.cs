using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class something
    {
        public static List<GlobalClass> GetSlotList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "6 AM to 9 AM",
                Color = "#2ebaee"
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "9 AM to 12 PM",
                Color = "#f05123"
            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "12 PM to 3 PM",
                Color = "#1b5732"
            });

            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "3 PM to 6 PM",
                Color = "#d50000"
            });
            Lst.Add(new GlobalClass
            {
                Id = 5,
                Text = "6 PM to 9 PM",
                Color = "#d50000"
            });
            return Lst;
        }
    }
}