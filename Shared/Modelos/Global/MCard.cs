using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MCard
    {
        public string clickFunc { get; set; }
        public string imgSrc { get; set; }
        public string title { get; set; }
        public string label { get; set; }

        public MCard(string _cf, string isr, string ti, string la)
        {
            clickFunc = _cf;
            imgSrc = isr;
            title = ti;
            label = la;
        }
    }
}

