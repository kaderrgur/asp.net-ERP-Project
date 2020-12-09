using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Class
{
    public class Context
    {
        private static erp_KaderGUREntities baglanti;

        public static erp_KaderGUREntities Baglanti
        {

            get
            {
                if (baglanti == null)
                    baglanti = new erp_KaderGUREntities();
                return baglanti;
            }
            set { baglanti = value; }
        }
    }
}